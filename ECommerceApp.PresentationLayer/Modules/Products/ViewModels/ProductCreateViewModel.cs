using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.PresentationLayer.Modules.Products.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description must be up to 500 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be zero or greater")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "SKU is required")]
        [StringLength(50, ErrorMessage = "SKU must be up to 50 characters")]
        [Display(Name = "SKU")]
        public string SKU { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
