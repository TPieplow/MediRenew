namespace Infrastructure.Utils;

public class TryConvert
{
    /// <summary>
    /// A tuple-method, using TryParse, converts a string to int.
    /// </summary>
    /// <param name="input">The input to convert from a string to a integer</param>
    /// <returns>returning a success bool, the result and a potential error-message (or null)</returns>
    public static (bool success, int result, string errorMessage) TryConvertStringToInt(string input)
    {
        if (int.TryParse(input, out int result))
        {
            return (true, result, null)!;
        }
        else
        {
            return (false, 0, "\nExiting menu...");
        }
    }

    /// <summary>
    /// Sets an int property using Action<>, taking the userinput in consideration. Works like a "func" but it doesnt return anyting but rather assigns a value.
    /// </summary>
    /// <param name="setPropertyAction">The action to set the interger</param>
    /// <param name="promptMessage">The message displayed to the user</param>
    public static void SetPropertyWithConversion(Action<int> setPropertyAction, string promptMessage)
    {
        string userInput = Cancel.AddOrAbort(promptMessage);
        var (success, result, errorMessage) = TryConvertStringToInt(userInput);

        if (success)
        {
            setPropertyAction(result);
        }
        else
        {
            Console.WriteLine(errorMessage);
            Console.ReadKey();
        }
    }
}
