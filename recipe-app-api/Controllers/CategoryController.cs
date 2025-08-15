
using Microsoft.AspNetCore.Mvc;
using recipe_app_api.Interfaces;
using recipe_app_api.Models;

namespace recipe_app_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService service, ILogger<CategoryController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategoryAsync(string categoryName)
        {
            _logger.LogInformation("INFO Starting creation of new category");

            var creadedCategoryId = await _service.CreateAsync(categoryName);

            _logger.LogInformation("INFO Successfully created recipe with id: {Id}", creadedCategoryId);

            return CreatedAtAction(nameof(GetCategoryById), new { id = creadedCategoryId }, null);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            _logger.LogInformation("INFO Starting deleting category with id: {Id}", id);

            await _service.DeleteAsync(id);

            _logger.LogInformation("INFO Successfully deleted category with id: {Id}", id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            _logger.LogInformation("INFO Starting update for category with id: {Id}", id);

            if (id != categoryDto.Id)
            {
                _logger.LogWarning("WARNING ID mismatch: URL id {Id} does not match body id {BodyId}", id, categoryDto.Id);
                return BadRequest($"ID in URL ({id}) does not match ID in body ({categoryDto.Id}).");
            }

            await _service.UpdateAsync(categoryDto);

            _logger.LogInformation("INFO Successfully updated category with id: {Id}", id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            _logger.LogInformation("INFO Starting fetching category by id:" + id);

            var category = await _service.GetById(id);

            return Ok(category);
        }

        [HttpGet("{id}/with-recipes")]
        public async Task<ActionResult<CategoryWithRecipesDto>> GetCategoryWithRecipes(int id)
        {
            _logger.LogInformation("INFO Starting fetching category by id:" + id);

            var category = await _service.GetWithRecipes(id);

            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _service.GetAllAsync();
            return Ok(categories);
        }
    }
}
