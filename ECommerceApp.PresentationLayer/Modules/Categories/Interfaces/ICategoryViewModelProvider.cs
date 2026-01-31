using ECommerceApp.PresentationLayer.Modules.Categories.ViewModels;

namespace ECommerceApp.PresentationLayer.Modules.Categories.Interfaces
{
    public interface ICategoryViewModelProvider
    {
        Task<(bool Success, string? ErrorMessage)> CreateCategoryAsync(CategoryCreateViewModel categoryCreateViewModel);
    }
}
