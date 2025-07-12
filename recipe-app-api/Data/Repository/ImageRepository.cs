using Microsoft.EntityFrameworkCore;
using recipe_app_api.Data.Entities;
using recipe_app_api.Interfaces;
namespace recipe_app_api.Data.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly RecipeDbContext _dbContext;

        public ImageRepository(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RecipeImageEntity imageEntity)
        {
            _dbContext.RecipeImages.Add(imageEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(RecipeImageEntity imageEntity)
        {
            _dbContext.RecipeImages.Remove(imageEntity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<RecipeImageEntity?> GetAsync(int recipeId)
        {
            return await _dbContext.RecipeImages.FirstOrDefaultAsync(x => x.RecipeId == recipeId);
        }
    }
}
