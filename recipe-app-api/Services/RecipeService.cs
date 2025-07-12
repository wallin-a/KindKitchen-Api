using AutoMapper;
using recipe_app_api.Data.Entities;
using recipe_app_api.Data.Repository;
using recipe_app_api.Exceptions;
using recipe_app_api.Interfaces;
using recipe_app_api.Models;

namespace recipe_app_api.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly BlobService _blobService;
        private readonly IImageRepository _imageRepository;

        public RecipeService(IRecipeRepository recipeRepository, ICategoryRepository categoryRepository, IMapper mapper, BlobService blobService, IImageRepository imageRepository)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _blobService = blobService;
            _imageRepository = imageRepository;
        }

        public async Task<int> CreateRecipeAsync(CreateRecipeDto recipeDto)
        {
            var recipe = new RecipeEntity
            {
                Title = recipeDto.Title,
                Categories = new List<CategoryEntity>(),
                Description = recipeDto.Description,
                CookingTime = recipeDto.CookingTime,
                Servings = recipeDto.Servings,
                Ingredients = new List<IngredientEntity>(),
                Steps = new List<StepEntity>()
            };

            var categories = await _categoryRepository.GetCategoryEntitiesByListOfIds(recipeDto.CategoryIds);
            recipe.Categories = categories;

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

            if (recipeDto.ImageFile != null)
            {
                // Generate unique blob filename
                var fileName = $"recipe_{recipe.Id}_{Guid.NewGuid()}_{recipeDto.ImageFile.FileName}";

                // Upload to Azure Blob Storage
                await _blobService.UploadFileAsync(recipeDto.ImageFile, fileName);

                // Save blob reference in RecipeImageEntity
                var recipeImage = new RecipeImageEntity
                {
                    RecipeId = recipe.Id,
                    BlobName = fileName
                };

                await _imageRepository.AddAsync(recipeImage);
            }

            return recipe.Id;
        }

        public async Task<string> GetImageAsync(int id)
        {
            var recipeImageEntity = await _imageRepository.GetAsync(id);

            if (recipeImageEntity == null)
                    throw new NotFoundException($"Image with id {id} not found.");

            var sasUrl = _blobService.GetSasUrl(recipeImageEntity.BlobName, TimeSpan.FromMinutes(30));

            return sasUrl;
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

        public async Task UpdateRecipeAsync(UpdateRecipeDto recipeDto)
        {
            var recipe = await _recipeRepository.GetByIdAsync(recipeDto.Id)
                         ?? throw new NotFoundException($"Recipe with Id {recipeDto.Id} not found.");

            recipe.Title = recipeDto.Title;
            recipe.Description = recipeDto.Description;
            recipe.CookingTime = recipeDto.CookingTime;
            recipe.Servings = recipeDto.Servings;

            recipe.Categories.Clear();
            var categories = await _categoryRepository.GetCategoryEntitiesByListOfIds(recipeDto.CategoryIds);
            recipe.Categories = categories;

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
                    Instruction = step
                });
            }

            await _recipeRepository.SaveChangesAsync();
        }
    }
}

