using MediRenew.ConsoleApp.Utils;

namespace Infrastructure.Utils;

public class Cancel
{
    public static string AddOrAbort(string prompt)
    {
        Console.WriteLine(prompt);
        string input = Console.ReadLine()?.Trim()!;

        if (input.Equals("cancel", StringComparison.CurrentCultureIgnoreCase))
        {
            DisplayMessage.Message("Operation aborted");
            return null;
        }
        return input;
    }
}
