using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.Hellpers;
using Souq.core.Dtos;
using Souq.core.Entities.Product;
using Souq.core.Interfaces;
using Souq.core.Shared;

namespace Souq.Api.Controllers
{

    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery]GetAllProductsParams getAllProductsParams)
        {
            try
            {
                var Products = await _unitOfWork.ProductRepo.GetAllAsync(getAllProductsParams);
                if (Products is null) return BadRequest(new ResponseApi(400));
               int TotalCountOfProducts = await _unitOfWork.ProductRepo.GetCountAsync();
                return Ok(new Pagination<ProductDto>(getAllProductsParams.PageNumber ,getAllProductsParams.PageSize ,TotalCountOfProducts , Products));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Product product = await _unitOfWork.ProductRepo.GetByIdAsync(id , x=>x.Category , x=>x.Images);
                if (product == null) return BadRequest(new ResponseApi(400));
                ProductDto result = _mapper.Map<ProductDto>(product);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add(AddProductDto addProductDto)
        {
            try
            {
                await _unitOfWork.ProductRepo.AddAsync(addProductDto);
                return Ok(new ResponseApi(200,"Product added successfully"));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
        {
            try
            {
                await _unitOfWork.ProductRepo.UpdateAsync(updateProductDto);
                return Ok(new ResponseApi(200, "Product Updated successfully"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                Product productFromDb = await _unitOfWork.ProductRepo.GetByIdAsync(Id);
                if (productFromDb == null)
                {
                    return NotFound();
                }

               await _unitOfWork.ProductRepo.DeleteAsync(productFromDb);
                return Ok(new ResponseApi(200, "Produtc Deleted Successfully"));
            }
            catch (Exception ex)
            {

               return BadRequest(new ResponseApi(400 , ex.Message));
            }
        }
    }
}
