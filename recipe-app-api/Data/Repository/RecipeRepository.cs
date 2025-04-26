using recipe_app_api.Data.Entities;
using recipe_app_api.Interfaces;
using recipe_app_api.Models;

namespace recipe_app_api.Data.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext _dbContext;

        public RecipeRepository(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateRecipeAsync(CreateRecipeDto recipeDto)
        {

            var recipe = new RecipeEntity
            {
                Title = recipeDto.Title,
                Category = recipeDto.Category,
                CookingTime = recipeDto.CookingTime,
                Servings = recipeDto.Servings,
                Ingredients = new List<IngredientEntity>(),
                Steps = new List<StepEntity>()
            };

            _dbContext.Recipes.Add(recipe);
            await _dbContext.SaveChangesAsync();

            foreach (var ingredient in recipeDto.Ingredients)
            {
                recipe.Ingredients.Add(new IngredientEntity
                {
                    RecipeId = recipe.Id,
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity
                });
            }

            int stepNumber = 1;

            foreach (var step in recipeDto.Steps)
            {
                recipe.Steps.Add(new StepEntity
                {
                    RecipeId = recipe.Id,
                    StepNumber = stepNumber,
                    Instruction = step
                });

                stepNumber++;
            }

            await _dbContext.SaveChangesAsync();


        }
    }
}
