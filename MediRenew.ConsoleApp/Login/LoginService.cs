namespace MediRenew.ConsoleApp.Login;

public class LoginService
{
    public static bool Login()
    {
        Console.Clear();
        string username, password;
        int loginAttempts = 0;
        const int maxAttempts = 3;

        Console.Write("\n\nLogin :\n");
        Console.Write("Username: Hans@domain.com\n");
        Console.Write("Password: Bytmig123\n");
        Console.Write("------------------------------------------------------\n");
        do
        {
            Console.WriteLine("Username: ");
            username = Console.ReadLine()!;

            Console.WriteLine("Password");
            password = Console.ReadLine()!;
            if (IsValidLogin(username, password))
            {
                Console.Clear();
                Console.WriteLine("Logged in successfully!\n\n");
                return true;
            }
            else

                loginAttempts++;
            if (loginAttempts < maxAttempts)
            {
                Console.WriteLine("Invalid credentials. Please try again.");
            }
            else
            {
                Console.WriteLine("You have entered wrong credentials more than 3 times. Please wait 10 minutes before trying again");
                Thread.Sleep(10 * 60 * 1000);
                loginAttempts = 0;
            }

        } while (loginAttempts < maxAttempts);
        Console.WriteLine("Exiting application due to multiple failed login attempts");
        return false;
    }

    private static bool IsValidLogin(string username, string password)
    {

        return username == "Hans@domain.com" && password == "Bytmig123";
    }
}
