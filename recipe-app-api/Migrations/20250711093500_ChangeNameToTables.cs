using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace recipe_app_api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipyImages_Recipes_RecipeId",
                table: "RecipyImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipyImages",
                table: "RecipyImages");

            migrationBuilder.RenameTable(
                name: "RecipyImages",
                newName: "RecipeImages");

            migrationBuilder.RenameIndex(
                name: "IX_RecipyImages_RecipeId",
                table: "RecipeImages",
                newName: "IX_RecipeImages_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeImages",
                table: "RecipeImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeImages_Recipes_RecipeId",
                table: "RecipeImages",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeImages_Recipes_RecipeId",
                table: "RecipeImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeImages",
                table: "RecipeImages");

            migrationBuilder.RenameTable(
                name: "RecipeImages",
                newName: "RecipyImages");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeImages_RecipeId",
                table: "RecipyImages",
                newName: "IX_RecipyImages_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipyImages",
                table: "RecipyImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipyImages_Recipes_RecipeId",
                table: "RecipyImages",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
