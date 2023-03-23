using Banks.BankSystem;
using Banks.ClientSystem;

namespace Banks.Commands;

public class InfoCommand : Command
{
    public override void Execute()
    {
        Console.WriteLine("Banks Info: \n");
        foreach (Bank bank in CentralBank.GetInstance().AllBanks)
        {
            Console.WriteLine($"bank name: {bank.Name} \n" +
                              $"bank id: {bank.Id} \n" +
                              $"bank settings: \n" +
                              $"1. debit percent {bank.Settings.DebitPercent}% \n" +
                              $"2.1. deposit first percent {bank.Settings.DepositPercentages[0]}% \n" +
                              $"2.2. deposit second percent {bank.Settings.DepositPercentages[1]}% \n" +
                              $"2.3. deposit third percent {bank.Settings.DebitPercent}% \n" +
                              $"3.1 credit limit {bank.Settings.CreditLimit} \n" +
                              $"3.2 credit commission {bank.Settings.CreditCommission} \n" +
                              $"4. limit for not verified accounts {bank.Settings.NotVerifiedLimit} \n");
            Console.WriteLine($"clients in bank {bank.Name} \n");
            foreach (Client bankClient in bank.AllClients)
            {
                Console.WriteLine($"client name: {bankClient.Name} \n" +
                                  $"client surname: {bankClient.Surname} \n" +
                                  $"client id: {bankClient.Id} \n" +
                                  $"client address: {bankClient.Address} \n" +
                                  $"client passport: {bankClient.Passport} \n");
            }
        }
    }
}