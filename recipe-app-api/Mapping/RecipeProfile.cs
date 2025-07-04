using AutoMapper;
using recipe_app_api.Data.Entities;
using recipe_app_api.Models;

namespace recipe_app_api.Mapping
{

    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<IngredientEntity, IngredientsDto>();

            CreateMap<StepEntity, StepDto>();

            CreateMap<RecipeEntity, RecipeDto>()
                .ForMember(dest => dest.Steps,
                    opt => opt.MapFrom(src => src.Steps))
                .ForMember(dest => dest.Ingredients,
                    opt => opt.MapFrom(src => src.Ingredients));
        }
    }

}

