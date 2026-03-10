using ECommerceApp.PresentationLayer.Modules.Carts.ViewModels;

namespace ECommerceApp.PresentationLayer.Modules.Carts.Interfaces
{
    public interface ICartViewModelProvider
    {
        CartViewModel GetCartViewModel();
        void AddItem(int productId, string productName, decimal unitPrice, int quantity = 1);
        void UpdateQuantity(int productId, int quantity);
        void RemoveItem(int productId);
    }
}
