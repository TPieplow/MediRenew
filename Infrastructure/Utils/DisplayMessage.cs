﻿namespace MediRenew.ConsoleApp.Utils
{
    public class DisplayMessage
    {
        public static void Message(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
