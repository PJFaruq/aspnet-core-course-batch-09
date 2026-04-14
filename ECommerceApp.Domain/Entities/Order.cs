namespace ECommerceApp.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }           // Primary Key
        public string ApplicationUserId { get; set; }   // Foreign Key to Customer
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string ShipAddress { get; set; }
        public Payment Payment { get; set; }

        // Relationship: Order can have many OrderItems
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
