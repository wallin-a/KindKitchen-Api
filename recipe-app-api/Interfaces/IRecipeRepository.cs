using recipe_app_api.Models;

namespace recipe_app_api.Interfaces;

public interface IRecipeRepository
{
    Task CreateRecipeAsync(CreateRecipeDto recipeDto);

    Task<RecipeDto> GetRecipeById(int id);
    Task<List<RecipeDto>> GetRecipes();
    Task DeleteRecipe(int id);
    Task UpdateRecipe(RecipeDto recipeDto);

}
