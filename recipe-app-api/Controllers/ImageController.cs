using Microsoft.AspNetCore.Mvc;
using recipe_app_api.Interfaces;

namespace recipe_app_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        private readonly ILogger<ImageController> _logger;

        public ImageController(IImageService imageService, ILogger<ImageController> logger)
        {
            _imageService = imageService;
            _logger = logger;
        }

        [HttpGet("{recipeId}/image")]
        public async Task<IActionResult> GetRecipeImage(int recipeId)
        {
            _logger.LogInformation("INFO Starting fetching image for recipe with id:" + recipeId);

            var sasUrl = await _imageService.GetImageAsync(recipeId);

            _logger.LogInformation("INFO SAS URL: " + sasUrl);

            return Redirect(sasUrl);
        }
    }
}
