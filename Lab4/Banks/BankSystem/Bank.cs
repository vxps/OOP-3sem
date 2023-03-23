using Banks.AccountTypes;
using Banks.ClientSystem;
using Banks.Observer;
using Banks.Tools;

namespace Banks.BankSystem;

public class Bank : IObservable
{
    private const int MinMoney = 0;
    private List<Account> _allAccounts;
    private List<Client> _allClients;
    private List<IObserver> _observers;
    public Bank(string name, BankSettings settings)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw BankException.InvalidBankNameException();
        ArgumentNullException.ThrowIfNull(settings);

        Name = name;
        Settings = settings;
        Id = Guid.NewGuid();
        _allAccounts = new List<Account>();
        _allClients = new List<Client>();
        _observers = new List<IObserver>();
    }

    public IReadOnlyList<Account> AllAccounts => _allAccounts;
    public IReadOnlyList<Client> AllClients => _allClients;
    public string Name { get; }
    public BankSettings Settings { get; }
    public Guid Id { get; }

    public bool CheckClientExist(Client client)
        => _allClients.Exists(cl => cl.Passport == client.Passport);

    public Client FindClientByPassport(string passport)
        => _allClients.FirstOrDefault(cl => cl.Passport == passport)
           ?? throw ClientException.InvalidClientNameFindException();

    public Client FindClientById(Guid id)
        => _allClients.FirstOrDefault(cl => cl.Id == id)
           ?? throw ClientException.InvalidClientNameFindException();

    public Account FindAccountByOwner(Client client)
        => _allAccounts.FirstOrDefault(x => x.AccountClient.Passport == client.Passport)
           ?? throw AccountException.InvalidAccountFindException();

    public Client AddClient(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        if (CheckClientExist(client))
            throw BankException.InvalidClientExistenceException();

        _allClients.Add(client);
        return client;
    }

    public Account AddAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        if (!account.IsVerified)
            account.SetVerificationLimit(Settings.NotVerifiedLimit);
        _allAccounts.Add(account);
        return account;
    }

    public decimal ChooseDepositPercentToAccount(decimal money)
    {
        ArgumentNullException.ThrowIfNull(money);
        if (money < MinMoney)
            throw AccountException.InvalidMoneyException();

        decimal percent = MinMoney;
        if (money < Settings.DepositPercentages[0])
            percent = Settings.DepositPercentages[0];
        if (money < Settings.DepositPercentages[1])
            percent = Settings.DepositPercentages[1];
        if (money > Settings.DepositPercentages[2])
            percent = Settings.DepositPercentages[2];

        return percent;
    }

    public BankSettings ChangeDebitSettings(decimal percent)
    {
        ArgumentNullException.ThrowIfNull(percent);

        Settings.ChangeDebitPercent(percent);
        NotifyObservers("debit percent", percent);
        return Settings;
    }

    public BankSettings ChangeDepositSettings(List<decimal> percents)
    {
        ArgumentNullException.ThrowIfNull(percents);

        Settings.ChangeDepositPercentages(percents);
        foreach (decimal percent in percents) NotifyObservers("deposit percent", percent);
        return Settings;
    }

    public BankSettings ChangeCreditSettings(decimal commission)
    {
        ArgumentNullException.ThrowIfNull(commission);

        Settings.ChangeCreditCommission(commission);
        NotifyObservers("credit commission", commission);
        return Settings;
    }

    public void AddObserver(IObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);

        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);

        _observers.Remove(observer);
    }

    public void NotifyObservers(string setting, decimal newLimit)
        => _observers.ForEach(x => x.Update($"Banks setting: {setting} - updated to {newLimit}"));
}