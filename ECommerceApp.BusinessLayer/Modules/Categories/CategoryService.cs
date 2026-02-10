using ECommerceApp.BusinessLayer.Modules.Categories.Interface;
using ECommerceApp.DataAccessLayer.Modules.Categories.Interfaces;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Categories
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)

        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {

            return await _categoryRepository.AddCategory(category);
            //category.CreatedDate = DateTime.Now;
            //Category category1 = _context.Categories.FirstOrDefault(m => m.Name == category.Name);
        }



    }
}
