using Shops.Entities;
using Shops.Tools;

namespace Shops.Services;

public class ShopService : IShopService
{
    private readonly List<Shop> _allShops;
    private readonly List<Customer> _allCustomers;
    private int _shopIdCounter = 0;
    private int _customerIdCounter = 0;

    public ShopService()
    {
        _allCustomers = new List<Customer>();
        _allShops = new List<Shop>();
    }

    public Product? FindProductInShop(Shop shop, Product product)
        => shop.Products.FirstOrDefault(other => other.Name.Equals(product.Name));

    public bool CheckShopExist(string address)
        => _allShops.Any(other => other.Address.Equals(address));

    public Shop AddShop(string name, string address)
    {
        if (CheckShopExist(address))
            throw new InvalidShopNameException("shop with such address exists");
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidShopNameException("wrong format of shop name");

        _shopIdCounter++;
        var shop = new Shop(name, _shopIdCounter, address);
        _allShops.Add(shop);

        return shop;
    }

    public Customer AddCustomer(string name, decimal money)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidCustomerNameException("wrong format of customer name");
        if (money < 0)
            throw new ShopException("money should be >0");

        var customer = new Customer(name, money, _customerIdCounter++);
        _allCustomers.Add(customer);

        return customer;
    }

    public Product AddProductToShop(Shop shop, Product product)
    {
        Product? other = FindProductInShop(shop, product);
        if (other is not null)
        {
            other.IncreaseQuantity(product.Quantity);
            if (other.Cost != product.Cost)
                other.ChangePrice(Math.Min(other.Cost, product.Cost));

            return other;
        }

        shop.AddProduct(product);

        return product;
    }

    public Shop FindShopWithCheapestCostOfProduct(Product product)
    {
        var shops = _allShops.FindAll(shop => shop.FindProduct(product).Name.Equals(product.Name)).ToList();
        Shop shopWithCheapestProduct = shops.OrderBy(shop => shop.FindProduct(product).Cost).First();

        return shopWithCheapestProduct ?? throw new ShopException("no such shop, which have this product");
    }

    public Shop FindShopWithCheapestListOfProducts(List<Product> productsToBuy)
    {
        ArgumentNullException.ThrowIfNull(productsToBuy);
        if (productsToBuy.Count == 0)
            throw new ShopException("list u want to find is empty");

        decimal minSum = decimal.MaxValue;
        var shops = _allShops.Where(shop => shop.Products.Any(productsToBuy.Contains)).ToList();
        if (shops.Count == 0)
            throw new ShopException("no such shops with that list of products");

        Shop? cheapestShop = null;
        foreach (Shop shop in shops)
        {
            var productsInShop = shop.Products.Where(productsToBuy.Contains).ToList();
            decimal productSum = productsInShop.Sum(product => product.Cost);
            if (productSum <= minSum)
            {
                minSum = productSum;
                cheapestShop = shop;
            }
        }

        return cheapestShop ?? throw new ShopException("no shop was found");
    }

    public void ChangeProductCost(Product product, decimal newCost)
    {
        if (newCost < 0)
            throw new InvalidProductCostException("cost can't be < 0!");
        ArgumentNullException.ThrowIfNull(product);

        product.ChangePrice(newCost);
    }

    public void CustomerBuyProductsInShop(Shop shop, Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentNullException.ThrowIfNull(shop);
        if (!CheckShopExist(shop.Address))
            throw new InvalidShopNameException("no such shop exist!");
        if (!shop.IsEnoughProducts(customer))
            throw new ShopException("Customer can't buy products in this shop!");

        foreach (Product product in customer.ToBuyList)
        {
            customer.BuyProduct(product);
        }
    }
}