using Microsoft.AspNetCore.Mvc;
using recipe_app_api.Interfaces;
using recipe_app_api.Models;

namespace recipe_app_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly ILogger<RecipeController> _logger;
        
        public RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger)
        {
            _recipeService = recipeService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRecipeAsync([FromForm] CreateRecipeDto recipeDto)
        {
            _logger.LogInformation("INFO Starting creation of new recipe");

            var createdRecipeId = await _recipeService.CreateRecipeAsync(recipeDto);

            _logger.LogInformation("INFO Successfully created recipe with id: {Id}", createdRecipeId);

            return CreatedAtAction(nameof(GetRecipeById), new { id = createdRecipeId }, null);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            _logger.LogInformation("INFO Starting deleting recipe with id: {Id}", id);

            await _recipeService.DeleteRecipeAsync(id);

            _logger.LogInformation("INFO Successfully deleted recipe with id: {Id}", id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, [FromForm] UpdateRecipeDto recipeDto)
        {
            _logger.LogInformation("INFO Starting update for recipe with id: {Id}", id);

            if (id != recipeDto.Id)
            {
                _logger.LogWarning("WARNING ID mismatch: URL id {Id} does not match body id {BodyId}", id, recipeDto.Id);
                return BadRequest($"ID in URL ({id}) does not match ID in body ({recipeDto.Id}).");
            }

            await _recipeService.UpdateRecipeAsync(recipeDto);

            _logger.LogInformation("INFO Successfully updated recipe with id: {Id}", id);

            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipeById(int id)
        {
            _logger.LogInformation("INFO Starting fetching recipe by id:" + id);

            var recipe = await _recipeService.GetRecipeById(id);

            return Ok(recipe);
           
        }

        [HttpGet]
        public async Task<ActionResult<List<RecipeDto>>> GetRecipes()
        {
            var recipes = await _recipeService.GetAllRecipes();

            return Ok(recipes);
        }
    }
}
