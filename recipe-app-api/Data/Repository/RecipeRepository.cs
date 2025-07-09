using AutoMapper;
using Microsoft.EntityFrameworkCore;
using recipe_app_api.Data.Entities;
using recipe_app_api.Interfaces;

namespace recipe_app_api.Data.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext _dbContext;

        public RecipeRepository(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RecipeEntity recipe)
        {
            _dbContext.Recipes.Add(recipe);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(RecipeEntity recipe)
        {
            _dbContext.Recipes.Remove(recipe);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
       
        public async Task<List<RecipeEntity>> GetAllAsync()
        {
            var recipes = await _dbContext.Recipes
                .Include(r => r.Steps)
                .Include(r => r.Ingredients)
                .ToListAsync();

            return recipes;
        }

        public async Task<RecipeEntity?> GetByIdAsync(int id)
        {
            return await _dbContext.Recipes
                .Include(r => r.Steps)
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
