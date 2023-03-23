namespace Shops.Tools;

public class InvalidProductCostException : ShopException
{
    public InvalidProductCostException() { }

    public InvalidProductCostException(string message)
        : base(message) { }

    public InvalidProductCostException(string message, Exception inner)
        : base(message, inner) { }
}