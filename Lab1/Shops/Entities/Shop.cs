using Shops.Tools;

namespace Shops.Entities;

public class Shop
{
    private readonly List<Product> _products;
    public Shop(string name, int id, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidShopNameException("wrong format of shop name");
        if (string.IsNullOrWhiteSpace(address))
            throw new ShopException("wrong format of address");

        Name = name;
        Id = id;
        Address = address;
        _products = new List<Product>();
    }

    public IReadOnlyList<Product> Products => _products;
    public string Name { get; }
    public int Id { get; }
    public string Address { get; }

    public void AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        _products.Add(product);
    }

    public Product FindProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        return _products.FirstOrDefault(other => other.Equals(product)) ??
               throw new InvalidProductNameException("no such product!");
    }

    public bool IsEnoughProducts(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        return customer.ToBuyList.All(other =>
            other.Name == FindProduct(other).Name && other.Quantity <= FindProduct(other).Quantity);
    }
}