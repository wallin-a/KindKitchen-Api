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
        public async Task<ActionResult> CreateRecipeAsync([FromForm] CreateRecipeDto recipeDto)
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
    }
}
