using ECommerceApp.PresentationLayer.Modules.Orders.ViewModels;

namespace ECommerceApp.PresentationLayer.Modules.Orders.Interfaces
{
    public interface ICheckoutViewModelProvider
    {
        Task<CheckoutViewModel?> GetCheckoutViewModel(string userId);
        Task<OrderConfirmationViewModel?> PlaceOrderAsync(CheckoutViewModel model, string userId);
    }
}
