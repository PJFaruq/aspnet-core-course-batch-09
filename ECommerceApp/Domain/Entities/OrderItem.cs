namespace ECommerceApp.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }           // Primary Key
        public int OrderId { get; set; }      // Foreign Key to Order
        public int ProductId { get; set; }    // Foreign Key to Product
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }

        // Relationships
        public Order Order { get; set; }      // OrderItem belongs to one Order
        public Product Product { get; set; }  // OrderItem refers to one Product
    }
}
