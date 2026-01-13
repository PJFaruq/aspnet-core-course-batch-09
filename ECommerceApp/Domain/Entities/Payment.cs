namespace ECommerceApp.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }           // Primary Key
        public int OrderId { get; set; }      // Foreign Key to Order
        public decimal Amount { get; set; }
        public string PayMethod { get; set; }
        public DateTime PayDate { get; set; }
        public string TransId { get; set; }

        // Relationship: Payment belongs to one Order
        public Order Order { get; set; }
    }
}
