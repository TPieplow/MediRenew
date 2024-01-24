using Business.Services;
using MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;
using Microsoft.IdentityModel.Tokens;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus;

public class StaffMenu
{
    private readonly StaffHandler _staffHandler;

    public StaffMenu(StaffHandler staffHandler)
    {
        _staffHandler = staffHandler;
    }

    public async Task PatientMenuAsync()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("What would you like to do? ");

            string[] menu =
            {
            "1. Add staff-member",
            "2. Find a staff-member through ID",
            "3. View staff",
            "4. Update staff-member",
            "5. Delete staff-member",
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
                    await _staffHandler.AddStaffMember();
                    break;

                case "2":

                    break;

                case "3":

                    break;

                case "4":

                    break;

                case "5":

                    break;

                case "0":
                    running = false;
                    break;
            }
        }
    }
}
