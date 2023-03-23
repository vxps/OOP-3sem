using Banks.BankSystem;
using Banks.ClientSystem;
using Banks.Tools;

namespace Banks.AccountTypes;

public class DebitAccount : Account
{
    public DebitAccount(decimal money, Client client, decimal percent)
        : base(money, client)
    {
        ArgumentNullException.ThrowIfNull(percent);
        if (percent < MinPercent)
            throw new ArgumentException("percent can't be < 0");

        Percent = percent;
    }

    public decimal Percent { get; }

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
            throw AccountException.InvalidAccountMoneyException();
        if (money > VerificationLimit)
            throw AccountException.InvalidLimitException();

        AccountMoney -= money;
    }

    internal override decimal CalculateMoney(int days)
    {
        ArgumentNullException.ThrowIfNull(days);
        if (days < MinDaysToCalculate)
            throw new ArgumentException("days can't be < 0");

        for (int i = 0; i < days; i++)
        {
            CalculatedMoney += CalculatedMoney + (Percent / YearDays);
        }

        return CalculatedMoney;
    }
}