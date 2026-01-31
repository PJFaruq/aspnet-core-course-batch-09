using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Categories.Interface
{
    public interface ICategroyService
    {
        Task<bool> CreateCategoryAsync(Category category);
    }
}
