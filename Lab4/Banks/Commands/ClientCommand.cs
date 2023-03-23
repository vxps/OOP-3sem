using Banks.BankSystem;
using Banks.ClientSystem;
using Banks.Tools;

namespace Banks.Commands;

public class ClientCommand : Command
{
    public override void Execute()
    {
        Console.WriteLine("enter name and surname \n");
        string clientName = Console.ReadLine() !;
        string clientSurname = Console.ReadLine() !;
        var clientBuilder = new ClientBuilder();
        clientBuilder.AddName(clientName);
        clientBuilder.AddSurname(clientSurname);
        Console.WriteLine($"your name: {clientName} \n" +
                          "do u want to change it? \n");
        if (Console.ReadLine() !.ToLower() == "yes")
        {
            clientName = Console.ReadLine() !;
            clientBuilder.AddName(clientName);
        }

        Console.WriteLine($"your surname: {clientSurname} \n" +
                          "do u want to change it? \n");
        if (Console.ReadLine() !.ToLower() == "yes")
        {
            clientSurname = Console.ReadLine() !;
            clientBuilder.AddSurname(clientSurname);
        }

        Console.WriteLine("if u want to add passport write 'yes' then write passport, to skip press enter \n");
        if (Console.ReadLine() !.ToLower() == "yes")
        {
            string passport = Console.ReadLine() !;
            if (Convert.ToDecimal(passport) < ClientBuilder.MinPassport)
                throw new ArgumentException("wrong passport data");

            clientBuilder.AddPassport(passport);
        }

        Console.WriteLine("if u want to add address write 'yes' then write address, to skip press enter \n ");
        if (Console.ReadLine() !.ToLower() == "yes")
        {
            string address = Console.ReadLine() !;
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("wrong address");

            clientBuilder.AddAddress(address);
        }

        Client client = clientBuilder.MakeClient();
        Console.WriteLine("then type bank name to add client \n");
        Bank bank1 = CentralBank.GetInstance().FindBankByName(Console.ReadLine() !);
        bank1.AddClient(client);
        Console.WriteLine($"Client: {client.Name + ' ' + client.Surname} successfully registered \n");
    }
}