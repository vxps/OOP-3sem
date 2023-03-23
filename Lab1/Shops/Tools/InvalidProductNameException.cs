namespace Shops.Tools;

public class InvalidProductNameException : ShopException
{
    public InvalidProductNameException() { }

    public InvalidProductNameException(string message)
        : base(message) { }

    public InvalidProductNameException(string message, Exception inner)
        : base(message, inner) { }
}