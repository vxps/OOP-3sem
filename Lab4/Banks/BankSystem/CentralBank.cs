using Banks.AccountTypes;
using Banks.ClientSystem;
using Banks.Models;
using Banks.Tools;

namespace Banks.BankSystem;

public class CentralBank
{
    private const int MinDays = 0;
    private const int MinMoney = 0;
    private static CentralBank? _instance;
    private List<Bank> _allBanks;
    private List<Transaction> _allTransactions;

    public CentralBank()
    {
        _allBanks = new List<Bank>();
        _allTransactions = new List<Transaction>();
    }

    public IReadOnlyList<Bank> AllBanks => _allBanks;
    public IReadOnlyList<Transaction> AllTransactions => _allTransactions;

    public static CentralBank GetInstance()
        => _instance ??= new CentralBank();

    public Transaction FindTransaction(Guid id)
        => _allTransactions.FirstOrDefault(x => x.Id == id)
           ?? throw TransactionException.InvalidTransactionFindException();

    public Bank FindBankById(Guid id)
        => _allBanks.FirstOrDefault(x => x.Id == id)
           ?? throw BankException.InvalidBankFindException();

    public Bank FindBankByName(string name)
        => _allBanks.FirstOrDefault(x => x.Name == name)
           ?? throw BankException.InvalidBankFindException();
    public Bank RegisterBank(string name, BankSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        if (string.IsNullOrWhiteSpace(name))
            throw BankException.InvalidBankNameException();

        var bank = new Bank(name, settings);
        _allBanks.Add(bank);
        return bank;
    }

    public Transaction RegisterTransaction(Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        _allTransactions.Add(transaction);
        return transaction;
    }

    public Transaction TransferMoney(decimal money, Account sender, Account recipient)
    {
        ArgumentNullException.ThrowIfNull(money);
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(recipient);
        if (money < MinMoney)
            throw BankException.InvalidMoneyException();
        if (money > sender.VerificationLimit)
            throw AccountException.InvalidLimitException();

        var transferTransaction = new TransferMoneyTransaction(sender, recipient, money, DateTime.Now);
        RegisterTransaction(transferTransaction);
        sender.WithdrawMoney(money);
        recipient.UpdateMoney(money);
        return transferTransaction;
    }

    public void CancelTransaction(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);

        Transaction transaction = FindTransaction(id)
                                  ?? throw new ArgumentException("no such transaction");

        transaction.CancelTransaction();
    }

    public void SimulateDays(Account account, int days)
    {
        ArgumentNullException.ThrowIfNull(days);
        ArgumentNullException.ThrowIfNull(account);
        if (days < MinDays)
            throw BankException.InvalidDaysException();

        account.CalculateMoney(days);
    }
}