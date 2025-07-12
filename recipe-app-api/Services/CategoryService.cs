using AutoMapper;
using Microsoft.EntityFrameworkCore;
using recipe_app_api.Data.Entities;
using recipe_app_api.Data.Repository;
using recipe_app_api.Exceptions;
using recipe_app_api.Interfaces;
using recipe_app_api.Models;

namespace recipe_app_api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public CategoryService(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> CreateAsync(string categoryName)
        {
            var newCategory = new CategoryEntity { Name = categoryName };

            await _repository.AddAsync(newCategory);
            return newCategory.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Category with id: {id} could not be found");

            await _repository.DeleteAsync(category);
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var category = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Category with id: {id} could not be found");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateAsync(CategoryDto categoryDto)
        {
            var category = await _repository.GetByIdAsync(categoryDto.Id)
                ?? throw new NotFoundException($"Category with id: {categoryDto.Id} could not be found");

            category.Name = categoryDto.Name;
            await _repository.SaveChangesAsync();
        }

        public  async Task<List<CategoryDto>> GetCategoriesByIdsAsync(List<int> categoryIds){
            var categories = await _repository.GetCategoryEntitiesByListOfIds(categoryIds)
                ?? throw new NotFoundException($"Categories could not be found");

            return _mapper.Map<List<CategoryDto>>(categories);

        }
    }
}
