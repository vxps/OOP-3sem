using Banks.AccountTypes;
using Banks.BankSystem;
using Banks.ClientSystem;
using Banks.Models;
using Banks.Tools;
using Xunit;

namespace Banks.Test;

public class BankServiceTest
{
    private CentralBank _centralBank = CentralBank.GetInstance();

    [Fact]
    public void AddBankAndAddClientToBank_CheckCreated()
    {
        var bankSettings = new BankSettings(1, new List<decimal>() { 2, 3, 4 }, 10000, 1000, 10000);
        Bank bank = _centralBank.RegisterBank("sber", bankSettings);
        var clientBuilder = new ClientBuilder();
        clientBuilder.AddName("name");
        clientBuilder.AddSurname("surname");
        clientBuilder.AddPassport("6000102030");
        clientBuilder.AddAddress("some street");
        Client client = clientBuilder.MakeClient();
        bank.AddClient(client);
        Assert.Equal(client, bank.AllClients[0]);
    }

    [Fact]
    public void CreateClient_ThrowException()
    {
        var bankSettings = new BankSettings(1, new List<decimal>() { 2, 3, 4 }, 10000, 1000, 10000);
        Bank bank = _centralBank.RegisterBank("sber", bankSettings);
        Assert.Throws<ClientException>(() =>
        {
            var clientBuilder = new ClientBuilder();
            clientBuilder.AddSurname("surname");
            clientBuilder.AddPassport("6000102030");
            clientBuilder.AddAddress("some street");
            Client client = clientBuilder.MakeClient();
        });
    }

    [Fact]
    public void CreateAccountForClientAndAddToBank_CheckCreateAccount()
    {
        var bankSettings = new BankSettings(1, new List<decimal> { 2, 3, 4 }, 10000, 1000, 10000);
        Bank bank = _centralBank.RegisterBank("sber", bankSettings);
        var clientBuilder = new ClientBuilder();
        clientBuilder.AddName("name");
        clientBuilder.AddSurname("surname");
        clientBuilder.AddPassport("6000102030");
        clientBuilder.AddAddress("some street");
        Client client = clientBuilder.MakeClient();
        bank.AddClient(client);

        Account acc = new DebitAccount(10000, client, bank.Settings.DebitPercent);
        bank.AddAccount(acc);
        Assert.Equal(acc, bank.AllAccounts[0]);
    }

    [Fact]
    public void UpdateMoneyAndWithdrawMoney_CheckCorrectMoneyOnAccount()
    {
        var bankSettings = new BankSettings(1, new List<decimal> { 2, 3, 4 }, 10000, 1000, 10000);
        Bank bank = _centralBank.RegisterBank("sber", bankSettings);
        var clientBuilder = new ClientBuilder();
        clientBuilder.AddName("name");
        clientBuilder.AddSurname("surname");
        clientBuilder.AddPassport("6000102030");
        clientBuilder.AddAddress("some street");
        Client client = clientBuilder.MakeClient();
        bank.AddClient(client);

        Account acc = new DebitAccount(10000, client, bank.Settings.DebitPercent);
        bank.AddAccount(acc);
        var transaction1 = new UpdateMoneyTransaction(acc, 1000, DateTime.Now);
        _centralBank.RegisterTransaction(transaction1);
        acc.UpdateMoney(transaction1.Money);
        var transaction2 = new WithdrawMoneyTransaction(acc, 5000, DateTime.Now);
        _centralBank.RegisterTransaction(transaction2);
        transaction2.Withdraw(5000);
        Assert.Equal(6000, acc.AccountMoney);
    }

    [Fact]
    public void CreateTransactionWithNotVerifiedAccount_ThrowException()
    {
        var bankSettings = new BankSettings(1, new List<decimal> { 2, 3, 4 }, 10000, 1000, 5000);
        Bank bank = _centralBank.RegisterBank("sber", bankSettings);
        var clientBuilder = new ClientBuilder();
        clientBuilder.AddName("name");
        clientBuilder.AddSurname("surname");
        Client client = clientBuilder.MakeClient();
        bank.AddClient(client);
        Account acc = new DebitAccount(10000, client, bank.Settings.DebitPercent);
        bank.AddAccount(acc);
        Assert.Throws<AccountException>(() =>
        {
            var transaction = new WithdrawMoneyTransaction(acc, 6000, DateTime.Now);
            _centralBank.RegisterTransaction(transaction);
            transaction.Withdraw(transaction.Money);
        });
    }

    [Fact]
    public void TransferMoneyFromBankToBank_CheckTransaction()
    {
        var bankSettings = new BankSettings(1, new List<decimal> { 2, 3, 4 }, 10000, 1000, 10000);
        Bank bank = _centralBank.RegisterBank("sber", bankSettings);
        var clientBuilder = new ClientBuilder();
        clientBuilder.AddName("name");
        clientBuilder.AddSurname("surname");
        clientBuilder.AddPassport("6000102030");
        clientBuilder.AddAddress("some street");
        Client client = clientBuilder.MakeClient();
        bank.AddClient(client);

        Bank bank2 = _centralBank.RegisterBank("tinek", bankSettings);
        var clientBuilder2 = new ClientBuilder();
        clientBuilder2.AddName("k");
        clientBuilder2.AddSurname("m");
        clientBuilder2.AddPassport("6000100000");
        clientBuilder2.AddAddress("some street");
        Client client2 = clientBuilder.MakeClient();
        bank2.AddClient(client2);

        Account acc1 = new DebitAccount(10000, client, bank.Settings.DebitPercent);
        bank.AddAccount(acc1);
        Account acc2 = new DebitAccount(5000, client2, bank2.Settings.DebitPercent);
        bank2.AddAccount(acc2);
        var transaction = new TransferMoneyTransaction(acc1, acc2, 2000, DateTime.Now);
        _centralBank.RegisterTransaction(transaction);
        transaction.Transfer(transaction.Money);
        Assert.Equal(8000, acc1.AccountMoney);
        Assert.Equal(7000, acc2.AccountMoney);
    }

    [Fact]
    public void CancelTransaction_CheckCorrect()
    {
        var bankSettings = new BankSettings(1, new List<decimal> { 2, 3, 4 }, 10000, 1000, 10000);
        Bank bank = _centralBank.RegisterBank("sber", bankSettings);
        var clientBuilder = new ClientBuilder();
        clientBuilder.AddName("name");
        clientBuilder.AddSurname("surname");
        clientBuilder.AddPassport("6000102030");
        clientBuilder.AddAddress("some street");
        Client client = clientBuilder.MakeClient();
        bank.AddClient(client);

        Account acc = new DebitAccount(10000, client, bank.Settings.DebitPercent);
        bank.AddAccount(acc);
        var transaction = new WithdrawMoneyTransaction(acc, 5000, DateTime.Now);
        _centralBank.RegisterTransaction(transaction);
        transaction.Withdraw(transaction.Money);
        _centralBank.CancelTransaction(transaction.Id);
        Assert.Equal(10000, acc.AccountMoney);
    }

    [Fact]
    public void NotifyObservers_CheckCorrectNotification()
    {
        var bankSettings = new BankSettings(1, new List<decimal> { 2, 3, 4 }, 10000, 1000, 10000);
        Bank bank = _centralBank.RegisterBank("sber", bankSettings);
        var clientBuilder = new ClientBuilder();
        clientBuilder.AddName("name");
        clientBuilder.AddSurname("surname");
        clientBuilder.AddPassport("6000102030");
        clientBuilder.AddAddress("some street");
        Client client = clientBuilder.MakeClient();
        bank.AddClient(client);
        bank.AddObserver(client);

        bank.ChangeDebitSettings(10);
        Assert.Equal($"Banks setting: debit percent - updated to 10", client.Notifications[0]);
    }
}