using ECommerceApp.Domain.Entities;

namespace ECommerceApp.DataAccessLayer.Modules.Categories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> AddCategory(Category category);
    }
}
