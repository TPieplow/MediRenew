namespace Infrastructure.Utils;

public class TryConvert
{
    public static (bool success, int result, string errorMessage) TryConvertStringToInt(string input)
    {
        if (int.TryParse(input, out int result))
        {
            return (true, result, null)!;
        }
        else
        {
            return (false, 0, "Invalid input. Please enter a valid integer");
        }
    }

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
