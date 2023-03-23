using Banks.AccountTypes;
using Banks.BankSystem;
using Banks.ClientSystem;
using Banks.Models;

namespace Banks.Commands;

public class TransactionCommand : Command
{
    public override void Execute()
    {
        Console.WriteLine("write bank name of a sender's bank \n");
        Bank bank = CentralBank.GetInstance().FindBankByName(Console.ReadLine() !);
        Console.WriteLine("write senders data for account to find owner of an account \n" +
                          "'passport' to find client by passport(verified client) \n" +
                          "'id' to find client by id(not verified client) \n");
        Client client;
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

        Console.WriteLine("write type of transaction \n" +
                                          "1. 'Update' money \n" +
                                          "2. 'Withdraw' money \n" +
                                          "3. 'Transfer' money from account to account \n" +
                                          "4. 'Cancel' transaction \n");
        switch (Console.ReadLine() !.ToLower())
        {
            case "update":
                Console.WriteLine("write money transaction \n");
                decimal money = Convert.ToDecimal(Console.ReadLine());
                Account sender = bank.FindAccountByOwner(client);
                var transaction = new UpdateMoneyTransaction(sender, money, DateTime.Now);
                CentralBank.GetInstance().RegisterTransaction(transaction);
                sender.UpdateMoney(money);
                Console.WriteLine($"Update Transaction: {sender.AccountClient.Name + ' ' + sender.AccountClient.Surname}" +
                                  $" updated money from {sender.AccountMoney - money} to {sender.AccountMoney} \n");
                break;
            case "withdraw":
                Console.WriteLine("write money transaction \n");
                money = Convert.ToDecimal(Console.ReadLine());
                Account sender2 = bank.FindAccountByOwner(client);
                var transaction2 = new WithdrawMoneyTransaction(sender2, money, DateTime.Now);
                CentralBank.GetInstance().RegisterTransaction(transaction2);
                sender2.WithdrawMoney(money);
                Console.WriteLine($"Withdraw Transaction: {sender2.AccountClient.Name + ' ' + sender2.AccountClient.Surname}" +
                                  $" withdraw money from {sender2.AccountMoney + money} to {sender2.AccountMoney} \n");
                break;
            case "transfer":
                Console.WriteLine("write money transaction \n");
                money = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Write Bank name of a recipients bank \n");
                Bank recBank = CentralBank.GetInstance().FindBankByName(Console.ReadLine() !);
                Console.WriteLine("write recipients data for account to find owner of an account \n" +
                                  "'passport' to find client by passport(verified client) \n" +
                                  "'id' to find client by id(not verified client) \n");
                Client recipient;
                switch (Console.ReadLine() !.ToLower())
                {
                    case "passport":
                        recipient = recBank.FindClientByPassport(Console.ReadLine() !);
                        break;
                    case "id":
                        recipient = recBank.FindClientById(Guid.Parse(Console.ReadLine() !));
                        break;
                    default:
                        throw new ArgumentException("no such command");
                }

                Account recipientAccount = recBank.FindAccountByOwner(recipient);
                Account sender3 = bank.FindAccountByOwner(client);
                var transferTransaction = new TransferMoneyTransaction(sender3, recipientAccount, money, DateTime.Now);
                CentralBank.GetInstance().RegisterTransaction(transferTransaction);
                CentralBank.GetInstance().TransferMoney(money, sender3, recipientAccount);
                Console.WriteLine($"Transfer Transaction: {sender3.AccountClient.Name + ' ' + sender3.AccountClient.Surname}" +
                                  $" transfer {money} to {recipient.Name + ' ' + recipient.Surname} \n");
                break;
            case "cancel":
                Console.WriteLine("write Id of a transaction u wan' to cancel \n");
                var id = Guid.Parse(Console.ReadLine() !);
                CentralBank.GetInstance().CancelTransaction(id);
                Console.WriteLine(
                    "Cancel Transaction: transaction successfully cancelled \n");
                break;
            default:
                throw new ArgumentException("no such command");
        }
    }
}