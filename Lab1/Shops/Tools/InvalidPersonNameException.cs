namespace Shops.Tools;

public class InvalidPersonNameException : ShopException
{
    public InvalidPersonNameException() { }

    public InvalidPersonNameException(string message)
        : base(message) { }

    public InvalidPersonNameException(string message, Exception inner)
        : base(message, inner) { }
}