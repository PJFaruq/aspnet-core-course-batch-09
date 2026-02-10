using ECommerceApp.DataAccessLayer.Modules.Categories.Interfaces;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.DataAccessLayer.Modules.Categories
{
    public class CategoryRepository : ICategoryRepository
    {

        Task<bool> ICategoryRepository.AddCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
