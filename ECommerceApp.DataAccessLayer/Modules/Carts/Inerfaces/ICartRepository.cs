using ECommerceApp.Domain.Entities;

namespace ECommerceApp.DataAccessLayer.Modules.Carts.Inerfaces
{
    public interface ICartRepository
    {
        Cart GetCart();
        void SaveCart(Cart cart);
        void ClearCart();
    }
}
