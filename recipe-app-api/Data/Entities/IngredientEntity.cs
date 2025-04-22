namespace recipe_app_api.Data.Entities
{
    public class IngredientEntity
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public required string Name { get; set; }
        public required string Quantity { get; set; } // e.g. "1 cup", "2 tbsp"
    }
}
