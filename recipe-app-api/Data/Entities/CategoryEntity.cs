namespace recipe_app_api.Data.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<RecipeEntity> Recipes { get; set; } = new();
    }
}
