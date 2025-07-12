namespace recipe_app_api.Data.Entities
{
    public class RecipeEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public TimeSpan CookingTime { get; set; }
        public int Servings { get; set; }
        public required List<CategoryEntity> Categories { get; set; } = new();
        public required List<IngredientEntity> Ingredients { get; set; } = new();
        public required List<StepEntity> Steps { get; set; } = new();
    }
}
