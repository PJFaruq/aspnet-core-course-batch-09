using ECommerceApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(StoreController.Index), "Store");
        }

    }
}
