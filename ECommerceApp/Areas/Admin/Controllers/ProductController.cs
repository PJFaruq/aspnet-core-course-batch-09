using ECommerceApp.BusinessLayer.Exceptions;
using ECommerceApp.PresentationLayer.Modules.Categories.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Products.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Products.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductViewModelProvider _productViewModelProvider;
        private readonly ICategoryViewModelProvider _categoryViewModelProvider;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductViewModelProvider productViewModelProvider, ICategoryViewModelProvider categoryViewModelProvider, IWebHostEnvironment env)
        {
            _productViewModelProvider = productViewModelProvider;
            _categoryViewModelProvider = categoryViewModelProvider;
            _env = env;
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
        public async Task<IActionResult> Create(ProductCreateViewModel product, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(product);
            }

            product.ImagePath = await SaveProductImageAsync(imageFile);

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

        private async Task<string?> SaveProductImageAsync(IFormFile? imageFile, string? oldPath = null)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return oldPath;
            }

            var allowed = new[] { ".jpg", ".jpeg", "png" };
            var ext = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !allowed.Contains(ext))
            {
                return oldPath;
            }

            var dir = Path.Combine(_env.WebRootPath, "Images", "products");
            Directory.CreateDirectory(dir);
            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(dir, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            if (!string.IsNullOrEmpty(oldPath))
            {
                var oldFull = Path.Combine(_env.WebRootPath, oldPath.TrimStart('/'));
                if (System.IO.File.Exists(oldFull))
                {
                    System.IO.File.Delete(oldFull);
                }
            }

            return "/images/products/" + fileName;
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
        public async Task<IActionResult> Edit(int id, ProductEditViewModel viewModel, IFormFile? imageFile)
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

            viewModel.ImagePath = await SaveProductImageAsync(imageFile, viewModel.ImagePath);

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
