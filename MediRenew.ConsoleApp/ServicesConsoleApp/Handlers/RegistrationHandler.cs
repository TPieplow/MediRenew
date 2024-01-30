using Business.Interfaces;
using Business.Services;
using Spectre.Console;
using static Infrastructure.Utils.ResultEnums;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers
{
    public class RegistrationHandler(IAuthenticationService authenticationService)
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<bool> RegisterAsync()
        {
            var username = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]Enter new username: [/]")
                .PromptStyle(Color.Yellow));

            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]Enter new password: [/]")
                .PromptStyle(Color.Yellow)
                .Secret());

            var registrationResult = await _authenticationService.CreateUserAndLoginAsync(username, password);

            if (registrationResult == Result.Success)
            {
                Console.Clear();
                AnsiConsole.Write(new Rule("/t[yellow]User registered![/]"));
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.Clear();
                AnsiConsole.Write(new Rule("/t[yellow]User registration failed, user already exists.[/]"));
                Console.ReadKey();
                return false;
            }

        }
    }
}
