namespace recipe_app_api.Data.Entities
{
    public class RecipeImageEntity
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string BlobName { get; set; } = string.Empty;
        public RecipeEntity Recipe { get; set; } = null!;
    }
}
