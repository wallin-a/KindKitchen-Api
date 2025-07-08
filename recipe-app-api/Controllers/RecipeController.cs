using Microsoft.AspNetCore.Mvc;
using recipe_app_api.Exceptions;
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
                _logger.LogError(ex, "ERROR while creating recipe");
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRecipe(int id) 
        {
            _logger.LogInformation($"INFO Starting deleting recipe with id: {id}");

            try
            {
                await _recipeRepository.DeleteRecipe(id);
                return NoContent();
            } catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return NotFound();
            } catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Unexpected error while deleting recipe with id {Id}", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRecipe([FromBody] RecipeDto recipe)
        {
            _logger.LogInformation($"INFO Starting deleting recipe with id: {recipe.Id}");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogWarning("Validation failed: {Errors}", string.Join(", ", errors));
                return BadRequest(ModelState);
            }

            try
            {
                await _recipeRepository.UpdateRecipe(recipe);
                return NoContent();
            } catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return NotFound();
            } catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Unexpected error while updating recipe with id {Id}", recipe.Id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipeById(int id)
        {
            _logger.LogInformation("INFO Starting fetching recipe by id:" + id);

            var recipe = await _recipeRepository.GetRecipeById(id);

            if (recipe == null)
            {
                _logger.LogError("ERROR Could not fetch recipe by id:" + id);

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
