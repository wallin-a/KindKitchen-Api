using recipe_app_api.Models;

namespace recipe_app_api.Interfaces
{
    public interface IRecipeService
    {
        Task<int> CreateRecipeAsync(CreateRecipeDto recipeDto);
        Task<RecipeDto> GetRecipeById(int id);
        Task<List<RecipeDto>> GetAllRecipes();
        Task DeleteRecipeAsync(int id);
        Task UpdateRecipeAsync(RecipeDto recipeDto);
    }
}
