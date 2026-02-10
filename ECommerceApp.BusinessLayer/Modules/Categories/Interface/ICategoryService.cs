using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Categories.Interface
{
    public interface ICategoryService
    {
        Task<bool> CreateCategoryAsync(Category category);
    }
}
