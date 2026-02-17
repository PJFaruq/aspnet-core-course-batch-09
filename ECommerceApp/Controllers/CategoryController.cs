using ECommerceApp.BusinessLayer.Exceptions;
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

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryViewModelProvider.GetAllAsync();
            return View(categories);
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
                return View(category);
            }

            try
            {
                await _categoryViewModelProvider.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidUserInputException ex)
            {

                ModelState.AddModelError(nameof(CategoryCreateViewModel.Name), ex.Message);
                return View(category);
            }


        }

        public async Task<IActionResult> Edit(int categoryId)
        {
            var viewModel = await _categoryViewModelProvider.GetByIdAsync(categoryId);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);


        }
    }
}
