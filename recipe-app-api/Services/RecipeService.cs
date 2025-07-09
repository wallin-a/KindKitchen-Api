using AutoMapper;
using recipe_app_api.Data.Entities;
using recipe_app_api.Exceptions;
using recipe_app_api.Interfaces;
using recipe_app_api.Models;

namespace recipe_app_api.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateRecipeAsync(CreateRecipeDto recipeDto)
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

            foreach (var ingredient in recipeDto.Ingredients)
            {
                recipe.Ingredients.Add(new IngredientEntity
                {
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity
                });
            }

            int stepNumber = 1;
            foreach (var step in recipeDto.Steps)
            {
                recipe.Steps.Add(new StepEntity
                {
                    StepNumber = stepNumber++,
                    Instruction = step
                });
            }

            await _recipeRepository.AddAsync(recipe);

            return recipe.Id;
        }
        public async Task<RecipeDto> GetRecipeById(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id)
                         ?? throw new NotFoundException($"Recipe with Id {id} not found.");


            RecipeDto RecipeDto = _mapper.Map<RecipeDto>(recipe);
            return RecipeDto;
        }

        public async Task<List<RecipeDto>> GetAllRecipes()
        {
            var recipes = await _recipeRepository.GetAllAsync();

            return _mapper.Map<List<RecipeDto>>(recipes);
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id)
                         ?? throw new NotFoundException($"Recipe with Id {id} not found.");

            await _recipeRepository.DeleteAsync(recipe);
        }

        public async Task UpdateRecipeAsync(RecipeDto recipeDto)
        {
            var recipe = await _recipeRepository.GetByIdAsync(recipeDto.Id)
                         ?? throw new NotFoundException($"Recipe with Id {recipeDto.Id} not found.");

            recipe.Title = recipeDto.Title;
            recipe.Category = recipeDto.Category;
            recipe.Description = recipeDto.Description;
            recipe.CookingTime = recipeDto.CookingTime;
            recipe.Servings = recipeDto.Servings;
            recipe.ImageUrl = recipeDto.ImageUrl;

            recipe.Ingredients.Clear();
            foreach (var ingredient in recipeDto.Ingredients)
            {
                recipe.Ingredients.Add(new IngredientEntity
                {
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity,
                    RecipeId = recipe.Id
                });
            }

            recipe.Steps.Clear();
            int stepNumber = 1;
            foreach (var step in recipeDto.Steps)
            {
                recipe.Steps.Add(new StepEntity
                {
                    RecipeId = recipe.Id,
                    StepNumber = stepNumber++,
                    Instruction = step.Instruction
                });
            }

            await _recipeRepository.SaveChangesAsync();
        }
    }
}

