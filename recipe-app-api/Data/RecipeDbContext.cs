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
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<RecipeImageEntity> RecipeImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecipeEntity>()
                .HasMany(r => r.Steps)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipeEntity>()
                .HasMany(r => r.Ingredients)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipeEntity>()
                .HasMany(r => r.Categories)
                .WithMany(c => c.Recipes);

        }
    }
}
