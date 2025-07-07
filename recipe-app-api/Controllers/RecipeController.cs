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

        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRecipeAsync(CreateRecipeDto recipeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _recipeRepository.CreateRecipeAsync(recipeDto);
            }
            catch (Exception ex)
            {

            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipeById(int id)
        {
            
            var recipe = await _recipeRepository.GetRecipeById(id);

            if (recipe == null)
            {
                return NotFound($"Could not find recipe with id: {id}");
            }
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
