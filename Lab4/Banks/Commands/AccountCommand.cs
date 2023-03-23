using Banks.AccountTypes;
using Banks.BankSystem;
using Banks.ClientSystem;
using Banks.Tools;

namespace Banks.Commands;

public class AccountCommand : Command
{
    public override void Execute()
    {
        Client client;
        decimal money;
        Console.WriteLine("write bank name for account \n");
        Bank bank = CentralBank.GetInstance().FindBankByName(Console.ReadLine() !);
        Console.WriteLine("write clients data for account to find owner of an account \n" +
                          "'passport' to find client by passport(verified client) \n" +
                          "'id' to find client by id(not verified client) \n");
        switch (Console.ReadLine() !.ToLower())
        {
            case "passport":
                client = bank.FindClientByPassport(Console.ReadLine() !);
                break;
            case "id":
                client = bank.FindClientById(Guid.Parse(Console.ReadLine() !));
                break;
            default:
                throw new ArgumentException("no such command");
        }

        Console.WriteLine("write what type of account u want to \n" +
                          "1. 'debit' for debit account and then write starting money\n" +
                          "2. 'deposit' for deposit account, then starting money and then add account duration\n" +
                          "3. 'credit' for credit account  \n");
        switch (Console.ReadLine() !.ToLower())
        {
            case "debit":
                money = Convert.ToDecimal(Console.ReadLine());
                var debitAccount = new DebitAccount(money, client, bank.Settings.DebitPercent);
                bank.AddAccount(debitAccount);
                Console.WriteLine(
                    $"Debit account for {client.Name + ' ' + client.Surname}: successfully registered \n" +
                    $"with balance {debitAccount.AccountMoney}\n");
                break;
            case "credit":
                bank.AddAccount(new CreditAccount(
                    bank.Settings.CreditLimit,
                    client,
                    bank.Settings.CreditCommission));
                Console.WriteLine(
                    $"Credit account for {client.Name + ' ' + client.Surname}: successfully registered \n" +
                    $"with balance {bank.Settings.CreditLimit}\n");
                break;
            case "deposit":
                money = Convert.ToDecimal(Console.ReadLine());
                var dateTime = Convert.ToDateTime(Console.ReadLine());
                bank.AddAccount(new DepositAccount(money, client, bank.ChooseDepositPercentToAccount(money), dateTime));
                Console.WriteLine(
                    $"Deposit account for {client.Name + ' ' + client.Surname}: successfully registered \n" +
                    $"with balance {money}\n");
                break;
            default:
                throw new ArgumentException("no such account or command \n");
        }
    }
}