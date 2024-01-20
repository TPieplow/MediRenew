namespace MediRenew.ConsoleApp.Login;

public class LoginService
{
    public bool Login()
    {
        Console.Clear();
        string username, password;
        int loginAttempts = 0;
        const int maxAttempts = 3;

        Console.Clear();
        Console.WriteLine("\tWelcome to MediRenew, a Hospital Database Handler! ");
        Console.Write("\t------------------------------------------------------\n");
        Console.Write("\n\n\tLogin credentials:\n");
        Console.Write("\tUsername: Hans@domain.com\n");
        Console.Write("\tPassword: Bytmig123\n");
 
        do
        {
            Console.Write("\n\tUsername: ");
            username = Console.ReadLine()!;

            Console.Write("\n\tPassword");
            password = Console.ReadLine()!;
            if (IsValidLogin(username, password))
            {
                Console.Clear();
                Console.WriteLine("\tLogged in successfully!\n\n");
                return true;
            }
            else

                loginAttempts++;
            if (loginAttempts < maxAttempts)
            {
                Console.WriteLine("\tInvalid credentials. Please try again.");
            }
            else
            {
                Console.WriteLine("\tYou have entered wrong credentials more than 3 times. Please wait 10 minutes before trying again");
                Thread.Sleep(10 * 60 * 1000);
                loginAttempts = 0;
            }

        } while (loginAttempts < maxAttempts);
        Console.WriteLine("\tExiting application due to multiple failed login attempts");
        return false;
    }

    private static bool IsValidLogin(string username, string password)
    {
        return username == "Hans@domain.com" && password == "Bytmig123";
    }
}
