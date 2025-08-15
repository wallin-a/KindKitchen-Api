using AutoMapper;
using recipe_app_api.Data.Entities;
using recipe_app_api.Models;
namespace recipe_app_api.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() { 
        
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CategoryEntity, CategoryWithRecipesDto>()
                .ForMember(dest => dest.Recipes,
                    opt => opt.MapFrom(src => src.Recipes));
        }
    }
}
