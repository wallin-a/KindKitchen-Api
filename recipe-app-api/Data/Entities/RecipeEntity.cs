namespace recipe_app_api.Data.Entities
{
    public class RecipeEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Category { get; set; }
        public int CookingTime { get; set; } // minutes
        public int Servings { get; set; }
        public string? ImageUrl { get; set; }

        public required List<IngredientEntity> Ingredients { get; set; } = new();
        public required List<StepEntity> Steps { get; set; } = new();
    }
}
