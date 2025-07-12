using System.ComponentModel.DataAnnotations;

namespace recipe_app_api.Models
{
    public class CreateRecipeDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public List<int> CategoryIds { get; set; } = new();

        public TimeSpan CookingTime { get; set; }

        [Range(1, 50, ErrorMessage = "Servings must be between 1 and 50.")]
        public int Servings { get; set; }

        [Required]
        public List<string> Steps { get; set; } = new();

        [Required]
        public List<IngredientsDto> Ingredients { get; set; } = new();
        public IFormFile? ImageFile { get; set; }
    }
}
