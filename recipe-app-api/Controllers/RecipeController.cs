using Microsoft.AspNetCore.Mvc;
using recipe_app_api.Interfaces;
using recipe_app_api.Models;

namespace recipe_app_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(IRecipeRepository recipeRepository, ILogger<RecipeController> logger)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRecipeAsync([FromBody] CreateRecipeDto recipeDto)
        {
            _logger.LogInformation("INFO Starting creating new recipe");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogWarning("Validation failed: {Errors}", string.Join(", ", errors));
                return BadRequest(ModelState);
            }
            try
            {
                await _recipeRepository.CreateRecipeAsync(recipeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating recipe");
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipeById(int id)
        {
            _logger.LogInformation("INFO Starting fetching recipe by id:" + id);

            var recipe = await _recipeRepository.GetRecipeById(id);

            if (recipe == null)
            {
                _logger.LogError("Error Could not fetch recipe by id:" + id);

                return NotFound($"Could not find recipe with id: {id}");
            }

            _logger.LogInformation($"INFO found recipe: {recipe.Id}, {recipe.Title}");

            return Ok(recipe);
           
        }

        [HttpGet]
        public async Task<ActionResult<List<RecipeDto>>> GetRecipes()
        {
            var recipes = await _recipeRepository.GetRecipes();

            if (recipes == null)
                return NotFound("Could not finf recipes");

            return Ok(recipes);
        }
    }
}
