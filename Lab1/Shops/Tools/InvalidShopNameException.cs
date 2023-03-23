namespace Shops.Tools;

public class InvalidShopNameException : Exception
{
    public InvalidShopNameException() { }

    public InvalidShopNameException(string message)
        : base(message) { }

    public InvalidShopNameException(string message, Exception inner)
        : base(message, inner) { }
}