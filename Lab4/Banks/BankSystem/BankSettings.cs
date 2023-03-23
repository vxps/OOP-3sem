using Banks.Tools;

namespace Banks.BankSystem;

public class BankSettings
{
    private const int MinPercent = 0;
    private const int MinMoneyLimit = 0;
    private List<decimal> _depositPercentages;
    public BankSettings(
        decimal debitPercent,
        List<decimal> depositPercentages,
        decimal creditLimit,
        decimal creditCommission,
        decimal notVerifiedLimit)
    {
        ArgumentNullException.ThrowIfNull(debitPercent);
        ArgumentNullException.ThrowIfNull(depositPercentages);
        ArgumentNullException.ThrowIfNull(creditLimit);
        ArgumentNullException.ThrowIfNull(creditCommission);
        ArgumentNullException.ThrowIfNull(NotVerifiedLimit);
        if (CheckSettingsOnLimits(debitPercent, depositPercentages, creditLimit, creditCommission, notVerifiedLimit))
        {
            throw BankException.InvalidPercentException();
        }

        depositPercentages.Sort();
        if (depositPercentages[0] >= depositPercentages[1]
            || depositPercentages[1] >= depositPercentages[2])
            throw BankException.InvalidDepositPercentException();

        DebitPercent = debitPercent;
        _depositPercentages = depositPercentages;
        CreditLimit = creditLimit;
        CreditCommission = creditCommission;
        NotVerifiedLimit = notVerifiedLimit;
    }

    public decimal DebitPercent { get; private set; }
    public IReadOnlyList<decimal> DepositPercentages => _depositPercentages;
    public decimal CreditLimit { get; private set; }
    public decimal CreditCommission { get; private set; }
    public decimal NotVerifiedLimit { get; }

    public decimal ChangeDebitPercent(decimal percent)
    {
        ArgumentNullException.ThrowIfNull(percent);
        if (percent < MinPercent)
            throw BankException.InvalidPercentException();

        DebitPercent = percent;
        return percent;
    }

    public List<decimal> ChangeDepositPercentages(List<decimal> percents)
    {
        ArgumentNullException.ThrowIfNull(percents);
        if (percents.Any(x => x < MinPercent))
            throw BankException.InvalidPercentException();

        _depositPercentages = percents;
        return percents;
    }

    public decimal ChangeCreditCommission(decimal money)
    {
        ArgumentNullException.ThrowIfNull(money);
        if (money < MinPercent)
            throw BankException.InvalidPercentException();

        CreditCommission = money;
        return money;
    }

    private bool CheckSettingsOnLimits(decimal debitPercent, List<decimal> depositPercentages, decimal creditLimit, decimal creditCommission, decimal notVerifiedLimit)
        => debitPercent < MinPercent ||
           depositPercentages.Any(percent => percent < MinPercent) ||
           creditLimit < MinMoneyLimit ||
           creditCommission < MinMoneyLimit ||
           NotVerifiedLimit < MinMoneyLimit;
}