using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }  //Primary key
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }

        //Relationship: One category can have many Products
        //public List<Product> Products { get; set; }
    }
}
