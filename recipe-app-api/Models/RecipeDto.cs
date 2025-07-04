namespace recipe_app_api.Models
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Category { get; set; } = string.Empty;

        public int CookingTime { get; set; }

        public int Servings { get; set; }

        public string? ImageUrl { get; set; }

        public List<StepDto> Steps { get; set; } = new();

        public List<IngredientsDto> Ingredients { get; set; } = new();
    }
}
