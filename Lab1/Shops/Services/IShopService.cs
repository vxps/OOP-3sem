using Shops.Entities;

namespace Shops.Services;

public interface IShopService
{
    Shop AddShop(string name, string address);
    Customer AddCustomer(string name, decimal money);
    Product AddProductToShop(Shop shop, Product product);
    void ChangeProductCost(Product product, decimal newCost);
    Shop FindShopWithCheapestCostOfProduct(Product product);
    void CustomerBuyProductsInShop(Shop shop, Customer customer);
    Shop FindShopWithCheapestListOfProducts(List<Product> productsToBuy);
}