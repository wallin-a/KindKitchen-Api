using recipe_app_api.Data.Entities;

namespace recipe_app_api.Interfaces
{
    public interface IImageRepository
    {
        Task AddAsync(RecipeImageEntity imageDto);
        Task DeleteAsync(RecipeImageEntity imageEntity);
        Task<RecipeImageEntity> GetAsync(int id);
    }
}
