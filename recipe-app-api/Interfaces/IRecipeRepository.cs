using recipe_app_api.Data.Entities;
using recipe_app_api.Models;

namespace recipe_app_api.Interfaces;

public interface IRecipeRepository
{
    Task AddAsync(RecipeEntity recipe);
    Task<RecipeEntity?> GetByIdAsync(int id);
    Task SaveChangesAsync();
    Task<List<RecipeEntity>> GetAllAsync();
    Task DeleteAsync(RecipeEntity recipe);
}
