namespace recipe_app_api.Models
{
    public class CategoryWithRecipesDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<RecipeDto> Recipes { get; set; } = new List<RecipeDto>();
    }
}
