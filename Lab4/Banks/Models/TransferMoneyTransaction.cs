using Banks.AccountTypes;
using Banks.Tools;

namespace Banks.Models;

public class TransferMoneyTransaction : Transaction
{
    public TransferMoneyTransaction(Account sender, Account recipient, decimal money, DateTime time)
        : base(sender, money, time)
    {
        ArgumentNullException.ThrowIfNull(recipient);

        Recipient = recipient;
    }

    public Account Recipient { get; }

    public void Transfer(decimal money)
    {
        ArgumentNullException.ThrowIfNull(money);
        if (money < MinMoney)
            throw TransactionException.InvalidMoneyException();

        Sender.WithdrawMoney(money);
        Recipient.UpdateMoney(money);
    }

    public override void CancelTransaction()
    {
        if (IsCancelled)
            throw TransactionException.InvalidCancellingException();

        Sender.UpdateMoney(Money);
        Recipient.WithdrawMoney(Money);
        IsCancelled = true;
    }
}