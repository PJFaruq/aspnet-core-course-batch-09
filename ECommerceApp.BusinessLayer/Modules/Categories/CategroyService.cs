using ECommerceApp.BusinessLayer.Modules.Categories.Interface;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Categories
{
    public class CategroyService : ICategroyService
    {
        public Task<bool> CreateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
            //category.CreatedDate = DateTime.Now;
            //Category category1 = _context.Categories.FirstOrDefault(m => m.Name == category.Name);
        }
    }
}
