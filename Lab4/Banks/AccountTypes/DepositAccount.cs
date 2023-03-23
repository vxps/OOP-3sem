using Banks.ClientSystem;
using Banks.Tools;

namespace Banks.AccountTypes;

public class DepositAccount : Account
{
    public DepositAccount(decimal money, Client client, decimal percent, DateTime duration)
        : base(money, client)
    {
        ArgumentNullException.ThrowIfNull(percent);
        if (percent < MinPercent)
            throw new ArgumentException("percent can't be < 0");

        Percent = percent;
        Duration = duration;
        IsDeposit = true;
    }

    public decimal Percent { get; }
    public DateTime Duration { get; }
    public bool IsDeposit { get; }

    public override void UpdateMoney(decimal money)
    {
        ArgumentNullException.ThrowIfNull(money);
        if (money < MinMoney)
            throw AccountException.InvalidMoneyException();

        AccountMoney += money;
    }

    internal override void WithdrawMoney(decimal money)
    {
        throw new ArgumentException("u can't withdraw money from deposit account before time passed");
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