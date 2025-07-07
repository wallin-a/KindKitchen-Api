using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using recipe_app_api.Data.Entities;
using recipe_app_api.Interfaces;
using recipe_app_api.Models;

namespace recipe_app_api.Data.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext _dbContext;
        private readonly IMapper _mapper;

        public RecipeRepository(RecipeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateRecipeAsync(CreateRecipeDto recipeDto)
        {

            var recipe = new RecipeEntity
            {
                Title = recipeDto.Title,
                Category = recipeDto.Category,
                Description = recipeDto.Description,
                CookingTime = recipeDto.CookingTime,
                Servings = recipeDto.Servings,
                ImageUrl = recipeDto.ImageUrl,
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

        public async Task<RecipeDto> GetRecipeById(int id)
        {
            var recipe = await _dbContext.Recipes
                .Include(r => r.Steps)
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id) ?? throw new Exception($"Recipe with Id {id} not found.");

            RecipeDto RecipeDto = _mapper.Map<RecipeDto>(recipe);
            return RecipeDto;
        }

        public async Task<List<RecipeDto>> GetRecipes()
        {
            var recipes = await _dbContext.Recipes
                .Include(r => r.Steps)
                .Include(r => r.Ingredients)
                .ProjectTo<RecipeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return recipes;
        }
    }
}
