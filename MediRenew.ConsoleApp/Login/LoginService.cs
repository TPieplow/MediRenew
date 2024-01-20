using Spectre.Console;
namespace MediRenew.ConsoleApp.Login;

public class LoginService
{
    public bool Login()
    {
        Console.Clear();
        string username, password;
        int loginAttempts = 0;
        const int maxAttempts = 3;

        Console.Clear();

        AnsiConsole.Write(new FigletText("MediRenew").Centered().Color(Color.Yellow));

        var rule = new Rule();
        rule.RuleStyle("Hotpink");
        AnsiConsole.Write(rule);

        AnsiConsole.Write(new Rule("\n[Yellow]The Number One - Hospital Database Handler![/]").RuleStyle(Color.HotPink));

        rule = new Rule();
        rule.RuleStyle("Hotpink");
        AnsiConsole.Write(rule);

        AnsiConsole.Write(
        new Table()
        .BorderStyle(Color.HotPink)
            .AddColumn(new TableColumn("[Yellow]Login Credentials[/]").Centered())
            .AddRow("[Yellow]Username: Hans@domain.com[/]")
            .AddRow("[Yellow]Password: Bytmig123![/]").Centered());

        do
        {
            AnsiConsole.Write(
            new Table()
                .BorderStyle(Color.HotPink)
                .AddColumn(new TableColumn("Login").Centered())
                .Centered());

            AnsiConsole.Write(new Rule("\n[yellow]Username:[/]").LeftJustified());
            username = Console.ReadLine()!;

            AnsiConsole.Write(new Rule("\n[yellow]Password:[/]").LeftJustified());
            password = Console.ReadLine()!;
            if (IsValidLogin(username, password))
            {
                Console.Clear();
                AnsiConsole.Write(new Rule("\t[yellow]Logged in successfully![/]\n\n").LeftJustified());
                return true;
            }
            else
            {
                loginAttempts++;
                if (loginAttempts < maxAttempts)
                {
                    AnsiConsole.Write(new Rule("\t[yellow]Invalid credentials. Please try again.[/]").LeftJustified());
                }
                else
                {
                    AnsiConsole.Write(new Rule("\tYou have entered wrong credentials more than 3 times. Please wait 10 minutes before trying again").LeftJustified());
                    Thread.Sleep(10 * 60 * 1000);
                    loginAttempts = 0;
                }
            }

        } while (loginAttempts < maxAttempts);
        AnsiConsole.Write(new Rule("\tExiting application due to multiple failed login attempts").LeftJustified());
        return false;
    }

    private static bool IsValidLogin(string username, string password)
    {
        return username == "Hans@domain.com" && password == "Bytmig123!";
    }
}
