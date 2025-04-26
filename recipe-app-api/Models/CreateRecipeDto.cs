using System.ComponentModel.DataAnnotations;

namespace recipe_app_api.Models
{
    public class CreateRecipeDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;

        [Range(1, 600, ErrorMessage = "Cooking time must be between 1 and 600 minutes.")]
        public int CookingTime { get; set; }

        [Range(1, 50, ErrorMessage = "Servings must be between 1 and 50.")]
        public int Servings { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public List<string> Steps { get; set; } = new();

        [Required]
        public List<IngredientsDto> Ingredients { get; set; } = new();
    }
}
