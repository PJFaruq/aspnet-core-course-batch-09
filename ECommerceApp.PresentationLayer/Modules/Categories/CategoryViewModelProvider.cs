using ECommerceApp.BusinessLayer.Modules.Categories.Interface;
using ECommerceApp.Domain.Entities;
using ECommerceApp.PresentationLayer.Modules.Categories.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Categories.ViewModels;

namespace ECommerceApp.PresentationLayer.Modules.Categories
{
    public class CategoryViewModelProvider : ICategoryViewModelProvider
    {
        public readonly ICategoryService _categoryService;

        public CategoryViewModelProvider(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<(bool Success, string? ErrorMessage)> CreateCategoryAsync(CategoryCreateViewModel categoryCreateViewModel)
        {

            Category category = new Category();
            category.Name = categoryCreateViewModel.Name;
            category.Description = categoryCreateViewModel.Description;

            bool isCreated = await _categoryService.CreateCategoryAsync(category);
            return (isCreated, null);

        }
    }
}
