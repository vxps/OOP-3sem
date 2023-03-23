namespace Banks.Tools;

public class TransactionException : Exception
{
    public TransactionException(string message)
        : base(message) { }

    public static TransactionException InvalidMoneyException()
    {
        throw new TransactionException("money can't be <0");
    }

    public static TransactionException InvalidCancellingException()
    {
        throw new TransactionException("transaction is already cancelled");
    }

    public static TransactionException InvalidTransactionFindException()
    {
        throw new TransactionException("no such transaction found");
    }
}