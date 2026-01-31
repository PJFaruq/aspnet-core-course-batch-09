using ECommerceApp.PresentationLayer.Modules.Categories.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Categories.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryViewModelProvider _categoryViewModelProvider;

        public CategoryController(ICategoryViewModelProvider categoryViewModelProvider)
        {
            _categoryViewModelProvider = categoryViewModelProvider;
        }

        //GET: Category/Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel category)
        {


            //Server side validation
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _categoryViewModelProvider.CreateCategoryAsync(category);

            if (!result.Success)
            {
                category.ErrorMessage = result.ErrorMessage;
            }


            return View(category);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
