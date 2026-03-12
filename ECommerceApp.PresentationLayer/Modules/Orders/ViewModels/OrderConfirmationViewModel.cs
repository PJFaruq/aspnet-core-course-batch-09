namespace ECommerceApp.PresentationLayer.Modules.Orders.ViewModels
{
    public class OrderConfirmationViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
