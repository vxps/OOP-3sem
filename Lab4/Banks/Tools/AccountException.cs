namespace Banks.Tools;

public class AccountException : Exception
{
    public AccountException(string message)
        : base(message) { }

    public static AccountException InvalidMoneyException()
    {
        throw new AccountException("money can't be <0");
    }

    public static AccountException InvalidAccountFindException()
    {
        throw new AccountException("no such account found");
    }

    public static AccountException InvalidWithdrawException()
    {
        throw new AccountException("you try to withdraw money > than u have");
    }

    public static AccountException InvalidAccountMoneyException()
    {
        throw new AccountException("not enough money in account");
    }

    public static AccountException InvalidLimitException()
    {
        throw new AccountException("u can' transfer or withdraw money > than Limit(not verified account)");
    }
}