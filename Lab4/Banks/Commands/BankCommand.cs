using Banks.BankSystem;
using Banks.Tools;

namespace Banks.Commands;

public class BankCommand : Command
{
    public override void Execute()
    {
        Console.WriteLine("enter bank name \n");
        string name = Console.ReadLine() !;
        Console.WriteLine("enter bank setting \n" +
                          "1. debit percent \n" +
                          "2.1 deposit first percent \n" +
                          "2.2. deposit second percent \n" +
                          "2.3. deposit third percent \n" +
                          "3.1. credit limit \n" +
                          "3.2. credit commission \n" +
                          "4. limit for not verified accounts \n");
        decimal debitPercent = Convert.ToDecimal(Console.ReadLine());
        decimal firstDeposit = Convert.ToDecimal(Console.ReadLine());
        decimal secondDeposit = Convert.ToDecimal(Console.ReadLine());
        decimal thirdDeposit = Convert.ToDecimal(Console.ReadLine());
        decimal creditLimit = Convert.ToDecimal(Console.ReadLine());
        decimal creditCommission = Convert.ToDecimal(Console.ReadLine());
        decimal limit = Convert.ToDecimal(Console.ReadLine());

        var settings = new BankSettings(
            debitPercent,
            new List<decimal>() { firstDeposit, secondDeposit, thirdDeposit },
            creditLimit,
            creditCommission,
            limit);
        CentralBank.GetInstance().RegisterBank(name, settings);
        Console.WriteLine($"Bank: {name} successfully registered \n");
    }
}