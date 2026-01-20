using ECommerceApp.Data;
using ECommerceApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ECommerceDbContext _context;

        public CategoryController(ECommerceDbContext context)
        {
            _context = context;
        }

        //GET: Category/Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Category category)
        {
            category.CreatedDate = DateTime.Now;

            //Server side validation
            if (!ModelState.IsValid)
            {
                return View();
            }

            Category category1 = _context.Categories.FirstOrDefault(m => m.Name == category.Name);

            if (category1 == null)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
