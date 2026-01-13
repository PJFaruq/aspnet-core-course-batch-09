namespace ECommerceApp.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }           // Primary Key
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        //Relationship: Product belongs to one Category
        public Category Category { get; set; }

        //Relationship: One product has one Inventory
        public Inventory Inventory { get; set; }

        public int CategoryId { get; set; }  //Foreign key


        // Relationship: Product can appear in many OrderItems
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


        // Relationship: Product can appear in many ShoppingCart items
        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

    }
}
