using ECommerceApp.BusinessLayer.Modules.Carts.Interfaces;
using ECommerceApp.BusinessLayer.Modules.Orders.Interfaces;
using ECommerceApp.DataAccessLayer.Identity;
using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ECommerceApp.PresentationLayer.Modules.Orders
{
    public class CheckoutViewModelProvider : ICheckoutViewModelProvider
    {

        private readonly ICartViewModelProvider _cartViewModelProvider;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CheckoutViewModelProvider(
                ICartViewModelProvider cartViewModelProvider,
                ICartService cartService,
                IOrderService orderService,
                UserManager<ApplicationUser> userManager)
        {
            _cartViewModelProvider = cartViewModelProvider;
            _cartService = cartService;
            _orderService = orderService;
            _userManager = userManager;
        }

        public async Task<CheckoutViewModel?> GetCheckoutViewModel(string userId)
        {
            var cartVm = _cartViewModelProvider.GetCartViewModel();
            if (cartVm.Items.Count == 0) { return null; }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) { return null; }

            return new CheckoutViewModel
            {
                Cart = cartVm,
                Email = user.Email,
                FullName = user.FullName,
                ShipAddress = user.ShippingAddress
            };
        }

        public async Task<OrderConfirmationViewModel?> PlaceOrderAsync(CheckoutViewModel model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) { return null; }

            user.ShippingAddress = model.ShipAddress;
            await _userManager.UpdateAsync(user);

            var order = await _orderService.PlaceOrderAsync(
                userId,
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
