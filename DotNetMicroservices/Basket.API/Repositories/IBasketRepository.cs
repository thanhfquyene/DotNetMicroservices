using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<Cart> GetBasket(string userName);
        Task<Cart> UpdateBasket(Cart cart);
        Task DeleteBasket(string userName);
     }
}
