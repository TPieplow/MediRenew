using Spectre.Console;
namespace MediRenew.ConsoleApp.Login;

public class LoginService
{
    public bool Login()
    {
        int loginAttempts = 0;
        const int maxAttempts = 3;

        Console.Clear();
        Header.StaticHeader();

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

            var username = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Username:[/] ")
            .PromptStyle(Color.Yellow));

            var password = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Password:[/] ")
            .PromptStyle(Color.Yellow)
                .Secret());

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
        return username == "1" && password == "1";
    }
}
