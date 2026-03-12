using ECommerceApp.PresentationLayer.Modules.Orders.ViewModels;

namespace ECommerceApp.PresentationLayer.Modules.Orders.Interfaces
{
    public interface ICheckoutViewModelProvider
    {
        CheckoutViewModel? GetCheckoutViewModel();
        Task<OrderConfirmationViewModel?> PlaceOrderAsync(CheckoutViewModel model);
    }
}
