using System.Drawing;
using Shops.Tools;

namespace Shops.Entities;

public class Product
{
    private const int MinProductCost = 0;
    private const int MinQuantity = 0;
    public Product(string name, decimal cost, int quantity = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidProductNameException("wrong format of product name");
        ArgumentNullException.ThrowIfNull(cost);
        if (cost < MinProductCost)
            throw new InvalidProductCostException("cost can't be < 0");

        Name = name;
        Cost = cost;
        Quantity = quantity;
    }

    public string Name { get; }
    public decimal Cost { get; private set; }
    public int Quantity { get; private set; }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < MinProductCost)
            throw new InvalidCustomerMoneyException("money can't be < 0");

        this.Cost = newPrice;
    }

    public void IncreaseQuantity(int x)
    {
        if (x < MinQuantity)
            throw new ShopException("quantity of product < 0");

        Quantity += x;
    }

    public bool Equals(Product? other)
        => other is not null && other.Name == this.Name;
}