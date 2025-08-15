using Microsoft.EntityFrameworkCore;
using recipe_app_api.Data.Entities;
using recipe_app_api.Models;

namespace recipe_app_api.Data.Repository
{
    public interface ICategoryRepository
    {
        Task AddAsync(CategoryEntity categoryEntity);
        Task<CategoryEntity?> GetByIdAsync(int id);
        Task<CategoryEntity?> GetWithRecipesAsync(int id);
        Task SaveChangesAsync();
        Task<List<CategoryEntity>> GetAllAsync();
        Task DeleteAsync(CategoryEntity categoryEntity);
        Task<List<CategoryEntity>> GetCategoryEntitiesByListOfIds(List<int> categoryIds);
    }
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RecipeDbContext _dbContext;

        public CategoryRepository(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(CategoryEntity categoryEntity)
        {
            _dbContext.Categories.Add(categoryEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CategoryEntity categoryEntity)
        {
            _dbContext.Categories.Remove(categoryEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CategoryEntity>> GetAllAsync()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<CategoryEntity?> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public  async Task<List<CategoryEntity>> GetCategoryEntitiesByListOfIds(List<int> categoryIds)
        {
            var categories = await _dbContext.Categories
                                    .Where(c => categoryIds.Contains(c.Id))
                                    .ToListAsync();
            return categories;
        }

        public async Task<CategoryEntity?> GetWithRecipesAsync(int id)
        {
            var categoryWithRecipes = await _dbContext.Categories
                .Include(c => c.Recipes)
                .FirstOrDefaultAsync(c => c.Id == id);

            return categoryWithRecipes;
        }
    }
}
