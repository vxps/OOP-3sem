namespace Shops.Tools;

public class InvalidCustomerMoneyException : Exception
{
    public InvalidCustomerMoneyException() { }

    public InvalidCustomerMoneyException(string message)
        : base(message) { }

    public InvalidCustomerMoneyException(string message, Exception inner)
        : base(message, inner) { }
}