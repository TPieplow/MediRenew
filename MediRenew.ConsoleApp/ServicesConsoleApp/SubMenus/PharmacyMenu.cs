using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;


namespace MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus;

public class PharmacyMenu(PharmacyHandler pharmacyHandler)
{
    private readonly PharmacyHandler _pharmacyHandler = pharmacyHandler;

    public async Task PharmacyMenuAsync()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Header.StaticHeader();
            Console.WriteLine("What would you like to do? ");

            string[] menu =
            {
            "1. View Pharmacy List",
            "0. Return to main menu"
        };

            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine(menu[i]);
            }

            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    await _pharmacyHandler.ViewAllPharmacies();
                    break;

                case "0":
                    running = false;
                    break;
            }
        }
    }
}
