using ECommerceApp.PresentationLayer.Modules.Categories.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Categories.ViewModels;

namespace ECommerceApp.PresentationLayer.Modules.Categories
{
    public class CategoryViewModelProvider : ICategoryViewModelProvider
    {
        public Task<(bool Success, string? ErrorMessage)> CreateCategoryAsync(CategoryCreateViewModel categoryCreateViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
