namespace ECommerceApp.Domain.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }           // Primary Key
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }

        public int CustomerId { get; set; }   // Foreign Key to Customer
        public int ProductId { get; set; }    // Foreign Key to Product

        // Relationships
        public Customer Customer { get; set; }    // Cart belongs to one Customer
        public Product Product { get; set; }      // Cart item refers to one Product

    }
}
