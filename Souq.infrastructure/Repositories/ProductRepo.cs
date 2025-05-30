﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Souq.core.Dtos;
using Souq.core.Entities.Product;
using Souq.core.Interfaces;
using Souq.core.Services;
using Souq.core.Shared;
using Souq.infrastructure.Data;

namespace Souq.infrastructure.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly IMapper _mapper;
        private readonly IImageServiceManagement _imagesService;
        public ProductRepo(AppDbContext context, IMapper mapper, IImageServiceManagement imagesService) : base(context)
        {
            _mapper = mapper;
            _imagesService = imagesService;
        }

        public async Task<ReturnProductDto> GetAllAsync(GetAllProductsParams getAllProductsParams)
        {
            IQueryable<Product> query = _context.Products.Include(p => p.Category).Include(p => p.Images).AsNoTracking();


            //Searching By Words
            if (!string.IsNullOrEmpty(getAllProductsParams.SearchingWord))
            {
                string[] ListOfSearchingWords = getAllProductsParams.SearchingWord.Split(" ");

                query = query.Where(x => ListOfSearchingWords.All(w =>
                    x.Name.ToLower().Contains(w.ToLower())
                    ||
                    x.Description.ToLower().Contains(w.ToLower())
               ));

            }

            // Filter By Category
            if (getAllProductsParams.CategoryId != null)
            {
                query = query.Where(x => x.CategoryId == getAllProductsParams.CategoryId);
            }

            ReturnProductDto returnProductDto = new ReturnProductDto();
            returnProductDto.TotalCount = query.Count();

            //Sorting 
            query = getAllProductsParams.Sort switch
            {
                "priceAse" => query.OrderBy(x => x.NewPrice),
                "priceDesc" => query.OrderByDescending(x => x.NewPrice),
                _ => query.OrderByDescending(x => x.Name),
            };

            // Pagination 
            query = query.Skip(getAllProductsParams.PageSize * (getAllProductsParams.PageNumber - 1)).Take(getAllProductsParams.PageSize);
            returnProductDto.Products = _mapper.Map<List<ProductDto>>(query.ToList());
            return (returnProductDto);
        }

        public async Task<bool> AddAsync(AddProductDto addProductDto)
        {
            if (addProductDto == null) return false;
            Product product = _mapper.Map<Product>(addProductDto);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            List<string> imagesPaths = await _imagesService.AddImagesAsync(addProductDto.Images, addProductDto.Name);
            List<Image> images = imagesPaths.Select(path => new Image()
            {
                ImageName = path,
                ProductId = product.Id,
            }).ToList();

            await _context.Images.AddRangeAsync(images);
            await _context.SaveChangesAsync();

            return true;
        }



        public async Task<bool> UpdateAsync(UpdateProductDto updateProductDto)
        {
            if (updateProductDto is null) return false;

            using var transaction = await _context.Database.BeginTransactionAsync();

            Product? productFromDB = await _context.Products.Include(x => x.Category).Include(x => x.Images)
                .FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == updateProductDto.Id);

            if (productFromDB == null) return false;

            _mapper.Map(updateProductDto, productFromDB);

            List<Image> ImageslistFromDb = await _context.Images.Where(x => x.ProductId == productFromDB.Id).ToListAsync();

            foreach (Image image in ImageslistFromDb)
            {
                _imagesService.DeleteImage(image.ImageName);
            }

            _context.Images.RemoveRange(ImageslistFromDb);

            if (updateProductDto.Images != null)
            {
                List<string> newImagesPaths = await _imagesService.AddImagesAsync(updateProductDto.Images, updateProductDto.Name);

                List<Image> images = newImagesPaths.Select(path => new Image()
                {
                    ImageName = path,
                    ProductId = updateProductDto.Id,
                }).ToList();

                _context.Images.AddRange(images);
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }

        public async Task DeleteAsync(Product product)
        {
            List<Image> productImagesFromDb = await _context.Images.Where(i => i.ProductId == product.Id).ToListAsync();
            foreach (Image image in productImagesFromDb)
            {
                _imagesService.DeleteImage(image.ImageName);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
