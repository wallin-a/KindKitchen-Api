using recipe_app_api.Models;

namespace recipe_app_api.Interfaces
{
    public interface ICategoryService
    {
        Task<int> CreateAsync(string categoryName);
        Task<CategoryDto> GetById(int id);
        Task<List<CategoryDto>> GetAllAsync();
        Task DeleteAsync(int id);
        Task UpdateAsync(CategoryDto categoryDto);
        Task<List<CategoryDto>> GetCategoriesByIdsAsync(List<int> categoryIds);
        Task<CategoryWithRecipesDto> GetWithRecipes(int id);
    }
}
