using Banks.ClientSystem;
using Banks.Tools;

namespace Banks.AccountTypes;

public class CreditAccount : Account
{
    public CreditAccount(decimal money, Client client, decimal commission)
        : base(money, client)
    {
        ArgumentNullException.ThrowIfNull(commission);
        if (commission < MinMoney)
            throw AccountException.InvalidMoneyException();

        Commission = commission;
    }

    public decimal Commission { get; }

    public override void UpdateMoney(decimal money)
    {
        ArgumentNullException.ThrowIfNull(money);
        if (money < MinMoney)
            throw AccountException.InvalidMoneyException();

        AccountMoney += money;
    }

    internal override void WithdrawMoney(decimal money)
    {
        ArgumentNullException.ThrowIfNull(money);
        if (money < MinMoney)
            throw AccountException.InvalidMoneyException();
        if (money > AccountMoney)
            throw AccountException.InvalidWithdrawException();
        if (money > VerificationLimit)
            throw AccountException.InvalidLimitException();

        if (AccountMoney < MinMoney)
            AccountMoney -= money * Commission;
        AccountMoney -= money;
    }

    internal override decimal CalculateMoney(int days)
        => CalculatedMoney;
}