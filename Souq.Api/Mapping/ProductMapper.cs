using AutoMapper;
using Souq.core.Dtos;
using Souq.core.Entities.Product;

namespace Souq.Api.Mapping
{
    public class ProductMapper:Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDto>().ForMember(x => x.CategoryName , op => op.MapFrom(src => src.Category.Name)).ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
            CreateMap<AddProductDto , Product>().ForMember(x=>x.Images,op=>op.Ignore()).ReverseMap();
            CreateMap<UpdateProductDto , Product>().ForMember(x=>x.Images,op=>op.Ignore()).ReverseMap();
        }
    }
}
