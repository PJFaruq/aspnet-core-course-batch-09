using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartViewModelProvider _cartViewModelProvider;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartViewModelProvider cartViewModelProvider, ILogger<CartController> logger)
        {
            _cartViewModelProvider = cartViewModelProvider;
            _logger = logger;
        }
        public IActionResult Index()
        {
            //you can access session from controller like this
            //HttpContext.Session.SetString("Cart", "data");
            //HttpContext.Session.GetString("Cart");

            //Loggin purpose
            //_logger.LogTrace("Index page is called");
            _logger.LogInformation("Cart Page accessed");


            var viewModel = _cartViewModelProvider.GetCartViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int productId, string productName, decimal unitPrice, int quantity = 1)
        {
            //Logging 
            // _logger.LogDebug("Add to Cart: product name {ProductName} , product id {ProductId}", productName, productId);

            try
            {
                if (quantity < 1)
                    quantity = 1;
                _cartViewModelProvider.AddItem(productId, productName, unitPrice, quantity);
                TempData["CartMessage"] = $"Added {productName} to cart.";
            }
            catch (Exception e)
            {

                _logger.LogError("Add to cart failed with product name {ProductName}", productName);
            }


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
