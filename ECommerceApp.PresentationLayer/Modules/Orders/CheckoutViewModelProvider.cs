using ECommerceApp.BusinessLayer.Modules.Carts.Interfaces;
using ECommerceApp.BusinessLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.ViewModels;

namespace ECommerceApp.PresentationLayer.Modules.Orders
{
    public class CheckoutViewModelProvider : ICheckoutViewModelProvider
    {

        private readonly ICartViewModelProvider _cartViewModelProvider;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CheckoutViewModelProvider(
                ICartViewModelProvider cartViewModelProvider,
                ICartService cartService,
                IOrderService orderService)
        {
            _cartViewModelProvider = cartViewModelProvider;
            _cartService = cartService;
            _orderService = orderService;
        }

        public CheckoutViewModel? GetCheckoutViewModel()
        {
            var cartVm = _cartViewModelProvider.GetCartViewModel();
            if (cartVm.Items.Count == 0) { return null; }
            return new CheckoutViewModel { Cart = cartVm };
        }

        public async Task<OrderConfirmationViewModel?> PlaceOrderAsync(CheckoutViewModel model)
        {
            var order = await _orderService.PlaceOrderAsync(
                model.FirstName,
                model.LastName,
                model.Email,
                model.Phone ?? "",
                model.ShipAddress);

            return new OrderConfirmationViewModel
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status ?? "Pending"
            };
        }
    }
}
