using ShoppingCart.Model;

namespace ShoppingCart.Repository
{
    public interface IRepositoryShippingRate:Repository<ShippingRate,int>
    {
        ShippingRate findByCountry(string country);
    }
}