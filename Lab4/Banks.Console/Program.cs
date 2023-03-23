using Banks.Commands;

namespace Banks;

public static class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("---------OPTIONS:---------\n" +
                                  "--bank to create bank \n" +
                                  "--account to create account and add him to bank \n" +
                                  "--client to create client \n" +
                                  "--transaction to create transaction \n" +
                                  "--info to get all info about all system \n" +
                                  "--exit to exit programme \n");
                Console.WriteLine("If u want to cancel choosing option and restart press UNDO \n");
                switch (Console.ReadLine())
                {
                    case "--bank":
                        var command = new BankCommand();
                        command.Execute();
                        break;
                    case "--client":
                        var command2 = new ClientCommand();
                        command2.Execute();
                        break;
                    case "--account":
                        var command3 = new AccountCommand();
                        command3.Execute();
                        break;
                    case "--transaction":
                        var command4 = new TransactionCommand();
                        command4.Execute();
                        break;
                    case "--info":
                        var command5 = new InfoCommand();
                        command5.Execute();
                        break;
                    case "--exit":
                        Environment.Exit(1);
                        break;
                    default:
                        throw new ArgumentException("no such command");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
