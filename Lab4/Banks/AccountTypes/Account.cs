using Banks.ClientSystem;
using Banks.Tools;

namespace Banks.AccountTypes;

public abstract class Account
{
    protected const int MinMoney = 0;
    protected const int MinPercent = 0;
    protected const int MinDaysToCalculate = 0;
    protected const int YearDays = 365;
    protected Account(decimal money, Client client)
    {
        ArgumentNullException.ThrowIfNull(money);
        if (money < MinMoney)
            throw AccountException.InvalidMoneyException();
        ArgumentNullException.ThrowIfNull(client);

        AccountClient = client;
        AccountMoney = money;
        CalculatedMoney = money;
        Id = Guid.NewGuid();
        IsVerified = client.CheckVerification();
        VerificationLimit = AccountMoney;
    }

    public Client AccountClient { get; }
    public decimal AccountMoney { get; protected set; }
    public decimal CalculatedMoney { get; protected set; }
    public Guid Id { get; }
    public bool IsVerified { get; }
    public decimal VerificationLimit { get; private set; }
    public void SetVerificationLimit(decimal limit) => VerificationLimit = limit;
    public abstract void UpdateMoney(decimal money);
    internal abstract void WithdrawMoney(decimal money);
    internal abstract decimal CalculateMoney(int days);
}