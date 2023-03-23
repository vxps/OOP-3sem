using Banks.AccountTypes;
using Banks.Tools;

namespace Banks.Models;

public class UpdateMoneyTransaction : Transaction
{
    public UpdateMoneyTransaction(Account sender, decimal money, DateTime time)
        : base(sender, money, time) { }

    public void Update(decimal updatingMoney)
    {
        ArgumentNullException.ThrowIfNull(updatingMoney);
        if (updatingMoney < MinMoney)
            throw TransactionException.InvalidMoneyException();

        Sender.UpdateMoney(updatingMoney);
    }

    public override void CancelTransaction()
    {
        if (IsCancelled)
            throw TransactionException.InvalidCancellingException();

        Sender.WithdrawMoney(Money);
        IsCancelled = true;
    }
}