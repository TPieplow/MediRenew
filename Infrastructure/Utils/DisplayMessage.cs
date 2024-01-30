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
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
