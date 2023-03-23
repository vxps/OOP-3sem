namespace Shops.Tools;

public class InvalidCustomerNameException : Exception
{
    public InvalidCustomerNameException() { }

    public InvalidCustomerNameException(string message)
        : base(message) { }

    public InvalidCustomerNameException(string message, Exception inner)
        : base(message, inner) { }
}