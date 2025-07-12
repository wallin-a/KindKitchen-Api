using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace recipe_app_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRecipeEntityAndAddCategoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Recipes");

            migrationBuilder.AddColumn<TimeSpan>(
                 name: "CookingTimeTemp",
                 table: "Recipes",
                 nullable: false,
                 defaultValue: new TimeSpan(0));

            migrationBuilder.Sql(
    @"
    UPDATE Recipes
    SET CookingTimeTemp = CAST(DATEADD(MINUTE, CookingTime, 0) AS time)
    ");
            migrationBuilder.DropColumn(
    name: "CookingTime",
    table: "Recipes");

            migrationBuilder.RenameColumn(
    name: "CookingTimeTemp",
    table: "Recipes",
    newName: "CookingTime");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipyImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    BlobName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipyImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipyImages_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryEntityRecipeEntity",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    RecipesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntityRecipeEntity", x => new { x.CategoriesId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_CategoryEntityRecipeEntity_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryEntityRecipeEntity_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntityRecipeEntity_RecipesId",
                table: "CategoryEntityRecipeEntity",
                column: "RecipesId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipyImages_RecipeId",
                table: "RecipyImages",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryEntityRecipeEntity");

            migrationBuilder.DropTable(
                name: "RecipyImages");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "CookingTime",
                table: "Recipes",
                type: "int",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
