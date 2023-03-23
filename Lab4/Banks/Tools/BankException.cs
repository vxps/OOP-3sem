namespace Banks.Tools;

public class BankException : Exception
{
    public BankException(string message)
        : base(message) { }

    public static BankException InvalidBankNameException()
    {
        throw new BankException("bank name can't be null or empty");
    }

    public static BankException InvalidBankFindException()
    {
        throw new BankException("no such bank found");
    }

    public static BankException InvalidPercentException()
    {
        throw new BankException("percents can't be < 0");
    }

    public static BankException InvalidDaysException()
    {
        throw new BankException("days can't be < 0");
    }

    public static BankException InvalidMoneyException()
    {
        throw new BankException("money can't be < 0");
    }

    public static BankException InvalidDepositPercentException()
    {
        throw new BankException("deposit percents be different and increase");
    }

    public static BankException InvalidClientExistenceException()
    {
        throw new BankException("such client exist in bank");
    }
}