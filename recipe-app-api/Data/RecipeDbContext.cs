using Microsoft.EntityFrameworkCore;
using recipe_app_api.Data.Entities;

namespace recipe_app_api.Data
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
        : base(options) { }
        public RecipeDbContext()
        {
            
        }

        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<IngredientEntity> Ingredients { get; set; }
        public DbSet<StepEntity> Steps { get; set; }
    }
}
