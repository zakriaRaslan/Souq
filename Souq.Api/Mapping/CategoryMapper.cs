using AutoMapper;
using Souq.core.Dtos;
using Souq.core.Entities.Product;

namespace Souq.Api.Mapping
{
    public class CategoryMapper:Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryDto , Category>().ReverseMap();
            CreateMap<UpdateCategoryDto , Category>().ReverseMap();
        }
    }
}
