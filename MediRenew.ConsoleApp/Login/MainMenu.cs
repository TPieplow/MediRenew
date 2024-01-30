using MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;
using Spectre.Console;
using Table = Spectre.Console.Table;

namespace MediRenew.ConsoleApp.Login;

public class MainMenu(LoginService loginService, RegistrationHandler registrationHandler)
{
    private readonly LoginService _loginService = loginService;
    private readonly RegistrationHandler _registrationHandler = registrationHandler;

    public async Task<bool> ShowMenuAsync()
    {
        bool loggedIn = true;

        while (loggedIn)
        {
            Console.Clear();
            Header.StaticHeader();

            var menu = new[]
            {
                "[Yellow]1. Log in[/]",
                "[Yellow]2. Create new user[/]"
            };

            var table = new Table();

            table.AddColumn(new TableColumn("[Yellow]Menu[/]").Centered());

            foreach ( var item in menu )
            {
                table.AddRow(item);
            }
            
            AnsiConsole.Write(
                table.Centered()
                .BorderStyle(Color.HotPink));

            var choice = AnsiConsole.Prompt(
                new TextPrompt<string>("[Yellow]Choose an option: [/]")
                .PromptStyle(Color.Yellow)
                .InvalidChoiceMessage("Invalid choice. Please enter 1 or 2")
                .Validate(choice => choice == "1" || choice == "2"));

            switch (choice)
            {
                case "1":
                    return await _loginService.Login();

                case "2":
                    await _registrationHandler.RegisterAsync();
                    break;
            }
        }
        return false;
    }
}
