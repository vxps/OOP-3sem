using System.Reflection;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Xunit;

namespace Shops.Test;

public class ShopServiceTest
{
    private IShopService _shopService = new ShopService();

    [Fact]
    public void AddProductToShop_ShopHasProduct()
    {
        Shop shop = _shopService.AddShop("abc", "12");
        Product product = _shopService.AddProductToShop(shop, new Product("something", 10, 1));
        Assert.Contains(product, shop.Products);
    }

    [Fact]
    public void AddProductToShop_ChangeProductCost()
    {
        Shop shop = _shopService.AddShop("abab", "01");
        Product product = _shopService.AddProductToShop(shop, new Product("anything", 20, 3));
        _shopService.ChangeProductCost(product, 12);
        Assert.Contains(product, shop.Products);
        Assert.Equal(12, product.Cost);
        Assert.Equal(12, shop.FindProduct(product).Cost);
    }

    [Fact]
    public void FindShopWithCheapestProduct_NotEnoughProductOrDontHaveProduct()
    {
        Shop shop1 = _shopService.AddShop("first", "1");
        Product product = _shopService.AddProductToShop(shop1, new Product("c#", 10, 1));
        Shop shop2 = _shopService.AddShop("second", "2");
        _shopService.AddProductToShop(shop2, new Product("c#", 12, 1));
        Shop shop3 = _shopService.AddShop("third", "3");
        _shopService.AddProductToShop(shop3, new Product("c#", 9, 1));
        Shop shop4 = _shopService.AddShop("fourth", "4");
        _shopService.AddProductToShop(shop4, new Product("c#", 4, 1));
        Assert.Equal(shop4, _shopService.FindShopWithCheapestCostOfProduct(product));
        Assert.Throws<InvalidProductNameException>(() =>
        {
            var product2 = new Product("product", 4, 4);
            _shopService.FindShopWithCheapestCostOfProduct(product2);
        });
    }

    [Fact]
    public void CustomerBuyProduct_CheckEnoughMoneyToBuy()
    {
        Customer customer = _shopService.AddCustomer("somebody", 100);
        Shop shop1 = _shopService.AddShop("shop1", "address1");
        Product pr1 = _shopService.AddProductToShop(shop1, new Product("pencil", 10, 2));
        Product pr2 = _shopService.AddProductToShop(shop1, new Product("pen", 20, 3));
        customer.AddProduct(pr1);
        customer.AddProduct(pr2);
        _shopService.CustomerBuyProductsInShop(shop1, customer);
        Assert.Equal(20, customer.Money);

        Assert.Throws<InvalidCustomerMoneyException>(() =>
        {
            Customer customer2 = _shopService.AddCustomer("somebody2", 30);
            Product p3 = _shopService.AddProductToShop(shop1, new Product("ben", 30, 2));
            customer2.AddProduct(p3);
            _shopService.CustomerBuyProductsInShop(shop1, customer2);
        });
    }

    [Fact]
    public void CustomerBuyListOfProductsInShop()
    {
        Customer customer = _shopService.AddCustomer("zaz", 100);
        Shop shop1 = _shopService.AddShop("s1", "1");
        Shop shop2 = _shopService.AddShop("s1", "2");
        Product p1 = _shopService.AddProductToShop(shop1, new Product("book", 10, 3));
        Product p2 = _shopService.AddProductToShop(shop1, new Product("book2", 20, 1));
        p1.ChangePrice(5);
        p2.ChangePrice(10);
        _shopService.AddProductToShop(shop2, p1);
        _shopService.AddProductToShop(shop2, p2);
        var products = new List<Product>();
        products.Add(p1);
        products.Add(p2);

        Assert.Equal(shop2, _shopService.FindShopWithCheapestListOfProducts(products));
    }
}