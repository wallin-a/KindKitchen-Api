using recipe_app_api.Data.Entities;

namespace recipe_app_api.Data.Repository
{
    public interface IRecipeImageRepository
    {
        //Task AddAsyn(RecipeImageEntity recipeImage);
    }
    public class RecipeImageRepository : IRecipeImageRepository
    {
        private readonly RecipeDbContext _dbContext;

        public RecipeImageRepository(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public Task AddAsyn(RecipeImageEntity recipeImage)
        //{
        //    _dbContext..Add(recipe);
        //    await _dbContext.SaveChangesAsync();
        //}
    }
}
