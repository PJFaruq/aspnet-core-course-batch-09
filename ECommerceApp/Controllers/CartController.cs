using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartViewModelProvider _cartViewModelProvider;

        public CartController(ICartViewModelProvider cartViewModelProvider)
        {
            _cartViewModelProvider = cartViewModelProvider;
        }
        public IActionResult Index()
        {
            //you can access session from controller like this
            //HttpContext.Session.SetString("Cart", "data");
            //HttpContext.Session.GetString("Cart");

            var viewModel = _cartViewModelProvider.GetCartViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int productId, string productName, decimal unitPrice, int quantity = 1)
        {
            if (quantity < 1)
                quantity = 1;
            _cartViewModelProvider.AddItem(productId, productName, unitPrice, quantity);
            TempData["CartMessage"] = $"Added {productName} to cart.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int productId, int quantity)
        {
            _cartViewModelProvider.UpdateQuantity(productId, quantity);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            _cartViewModelProvider.RemoveItem(productId);
            return RedirectToAction(nameof(Index));
        }
    }
}
