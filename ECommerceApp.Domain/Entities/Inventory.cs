namespace ECommerceApp.Domain.Entities
{
    public class Inventory
    {
        public int Id { get; set; }           // Primary Key
        public int ProductId { get; set; }    // Foreign Key to Product
        public int StockQty { get; set; }
        public int ReorderLvl { get; set; }
        public DateTime LastUpdated { get; set; }

        // Relationship: Inventory belongs to one Product
        public Product Product { get; set; }
    }
}
