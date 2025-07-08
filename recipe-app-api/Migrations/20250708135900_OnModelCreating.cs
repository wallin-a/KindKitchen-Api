using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace recipe_app_api.Migrations
{
    /// <inheritdoc />
    public partial class OnModelCreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_RecipeEntityId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Recipes_RecipeEntityId",
                table: "Steps");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_RecipeEntityId",
                table: "Ingredients",
                column: "RecipeEntityId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Recipes_RecipeEntityId",
                table: "Steps",
                column: "RecipeEntityId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_RecipeEntityId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Recipes_RecipeEntityId",
                table: "Steps");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_RecipeEntityId",
                table: "Ingredients",
                column: "RecipeEntityId",
                principalTable: "Recipes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Recipes_RecipeEntityId",
                table: "Steps",
                column: "RecipeEntityId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
