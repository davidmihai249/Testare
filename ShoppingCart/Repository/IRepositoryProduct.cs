using ShoppingCart.Model;

namespace ShoppingCart
{
    public interface IRepositoryProduct:Repository<Product,int>
    {
        Product findByProductName(string name);
    }
}