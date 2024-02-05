using Spectre.Console;

namespace MediRenew.ConsoleApp.Utils
{
    public class DisplayMessage
    {
        /// <summary>
        /// Displays a custom message to the user, includes a Console.ReadKey();.
        /// </summary>
        /// <param name="message">The message to be shown</param>
        public static void Message(string message)
        {
            AnsiConsole.Write(new Markup($"\n[Red]{message}[/]"));
            AnsiConsole.Write(new Markup("\n[Green]Press any key to continue...[/]"));
            Console.ReadKey();
        }
    }
}
