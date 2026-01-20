using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain.Entities
{
    public class Category
    {

        public int Id { get; set; }  //Primary key

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 to 50 character")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description name is required")]
        [StringLength(50, ErrorMessage = "Name must be up to 500 characters")]
        [Display(Name = "Category Description")]
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        //Relationship: One category can have many Products
        //public List<Product> Products { get; set; }
    }
}
