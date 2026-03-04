using ECommerceApp.BusinessLayer.Exceptions;
using ECommerceApp.PresentationLayer.Modules.Categories.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Products.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Products.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductViewModelProvider _productViewModelProvider;
        private readonly ICategoryViewModelProvider _categoryViewModelProvider;

        public ProductController(IProductViewModelProvider productViewModelProvider, ICategoryViewModelProvider categoryViewModelProvider)
        {
            _productViewModelProvider = productViewModelProvider;
            _categoryViewModelProvider = categoryViewModelProvider;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productViewModelProvider.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryViewModelProvider.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel product)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(product);
            }

            try
            {
                await _productViewModelProvider.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidUserInputException ex)
            {
                ModelState.AddModelError(nameof(ProductCreateViewModel.SKU), ex.Message);
                var categories = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(product);
            }
        }

        public async Task<IActionResult> Edit(int productId)
        {
            var viewModel = await _productViewModelProvider.GetByIdAsync(productId);
            if (viewModel == null)
            {
                return NotFound();
            }

            var categories = await _categoryViewModelProvider.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var categories = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(viewModel);
            }

            try
            {
                await _productViewModelProvider.UpdateAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidUserInputException ex)
            {
                ModelState.AddModelError(nameof(ProductEditViewModel.SKU), ex.Message);
                var categories = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(viewModel);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await _productViewModelProvider.GetByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _productViewModelProvider.GetByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            bool isDeleted = await _productViewModelProvider.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
