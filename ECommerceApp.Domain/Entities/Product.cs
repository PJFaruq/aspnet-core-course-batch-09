using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }           // Primary Key

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        public string SKU { get; set; }

        [StringLength(500)]
        public string? ImagePath { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        //Relationship: Product belongs to one Category

        public Category Category { get; set; }

        //Relationship: One product has one Inventory
        public Inventory? Inventory { get; set; }

        public int CategoryId { get; set; }  //Foreign key


        // Relationship: Product can appear in many OrderItems
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


        // Relationship: Product can appear in many ShoppingCart items
        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

    }
}
