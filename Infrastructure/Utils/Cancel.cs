using MediRenew.ConsoleApp.Utils;

namespace Infrastructure.Utils;

public class Cancel
{

    /// <summary>
    /// Takes a string prompt from the user, if prompt = "cancel" the operation cancels. 
    /// </summary>
    /// <param name="prompt">The prompt from the user</param>
    /// <returns>Returns null if prompt is "cancel", else the input</returns>
    public static string AddOrAbort(string prompt)
    {
        Console.WriteLine(prompt);
        string input = Console.ReadLine()?.Trim()!;

        if (input.Equals("cancel", StringComparison.CurrentCultureIgnoreCase) || String.IsNullOrWhiteSpace(input))
        {
            DisplayMessage.Message("Operation aborted");
            return null!;
        }
        return input;
    }
}