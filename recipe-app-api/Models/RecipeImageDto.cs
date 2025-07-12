using recipe_app_api.Data.Entities;

namespace recipe_app_api.Models
{
    public class RecipeImageDto
    {
        public int RecipeId { get; set; }
        public string BlobName { get; set; } = string.Empty;
    }
}
