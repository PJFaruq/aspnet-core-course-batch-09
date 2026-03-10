using ECommerceApp.DataAccessLayer.Modules.Carts.Inerfaces;
using ECommerceApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace ECommerceApp.DataAccessLayer.Modules.Carts
{
    public class SessionCartRepository : ICartRepository
    {
        private const string CartSessionKey = "Cart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionCartRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public Cart GetCart()
        {
            if (Session.TryGetValue(CartSessionKey, out byte[]? bytes) && bytes != null && bytes.Length > 0)
            {
                var json = Encoding.UTF8.GetString(bytes);
                var cart = JsonSerializer.Deserialize<Cart>(json);
                return cart;
            }

            return new Cart();
        }

        public void SaveCart(Cart cart)  //int, string, byte[]
        {

            var json = JsonSerializer.Serialize(cart);
            var encoded = Encoding.UTF8.GetBytes(json);
            Session.Set(CartSessionKey, encoded);
        }
    }
}
