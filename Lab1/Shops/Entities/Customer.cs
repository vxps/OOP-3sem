using Shops.Tools;

namespace Shops.Entities;

public class Customer
{
    private const int MinMoney = 0;
    private readonly List<Product> _products;
    public Customer(string name, decimal money, int id)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidPersonNameException("wrong person name");
        ArgumentNullException.ThrowIfNull(money);
        if (money < MinMoney)
            throw new InvalidCustomerMoneyException("money can't be <0");

        Name = name;
        Money = money;
        Id = id;
        _products = new List<Product>();
    }

    public string Name { get; }
    public decimal Money { get; private set; }
    public int Id { get; }
    public IReadOnlyList<Product> ToBuyList => _products;

    public Product? FindProductInCustomerList(Product product)
        => _products.FirstOrDefault(other => other.Name.Equals(product.Name));

    public Product AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        Product? other = FindProductInCustomerList(product);
        if (other is not null)
        {
            other.IncreaseQuantity(product.Quantity);
            if (other.Cost != product.Cost)
                other.ChangePrice(Math.Min(other.Cost, product.Cost));

            return other;
        }

        _products.Add(product);

        return product;
    }

    public void BuyProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (product.Cost * product.Quantity > Money)
            throw new InvalidCustomerMoneyException("customer don't have enough money to buy such product");

        Money -= product.Cost * product.Quantity;
    }
}