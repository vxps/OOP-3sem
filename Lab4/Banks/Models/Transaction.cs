using Banks.AccountTypes;
using Banks.Tools;

namespace Banks.Models;

public abstract class Transaction
{
    protected const int MinMoney = 0;
    public Transaction(Account sender, decimal money, DateTime transactionTime)
    {
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(money);
        ArgumentNullException.ThrowIfNull(transactionTime);
        if (money < MinMoney)
            throw TransactionException.InvalidMoneyException();

        Sender = sender;
        Money = money;
        TransactionTime = transactionTime;
        Id = Guid.NewGuid();
        IsCancelled = false;
    }

    public Account Sender { get; }
    public decimal Money { get; }
    public DateTime TransactionTime { get; }
    public Guid Id { get; }
    public bool IsCancelled { get; protected set; }
    public abstract void CancelTransaction();
}