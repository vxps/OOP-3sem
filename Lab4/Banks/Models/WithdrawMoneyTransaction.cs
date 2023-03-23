using Banks.AccountTypes;
using Banks.Tools;

namespace Banks.Models;

public class WithdrawMoneyTransaction : Transaction
{
    public WithdrawMoneyTransaction(Account account, decimal money, DateTime time)
        : base(account, money, time) { }

    public void Withdraw(decimal withdrawingMoney)
    {
        ArgumentNullException.ThrowIfNull(withdrawingMoney);
        if (withdrawingMoney < MinMoney)
            throw TransactionException.InvalidMoneyException();

        Sender.WithdrawMoney(withdrawingMoney);
    }

    public override void CancelTransaction()
    {
        if (IsCancelled)
            throw TransactionException.InvalidCancellingException();

        Sender.UpdateMoney(Money);
        IsCancelled = true;
    }
}