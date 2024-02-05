using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;
using MediRenew.ConsoleApp.Utils;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus;

public class StaffMenu
{
    private readonly StaffHandler _staffHandler;

    public StaffMenu(StaffHandler staffHandler)
    {
        _staffHandler = staffHandler;
    }

    public async Task StaffMenuAsync()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Header.StaticHeader();

            string[] menu =
            [
                "1. Add staff-member",
                "2. Find a staff-member through ID",
                "3. View staff",
                "4. Update staff-member",
                "5. Delete staff-member",
                "0. Return to main menu"
            ];

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
                    await _staffHandler.ViewOneStaffMemberWithId();
                    break;

                case "3":
                    await _staffHandler.ViewAllStaff();
                    DisplayMessage.Message("");
                    break;

                case "4":
                    await _staffHandler.UpdateStaffMember();
                    break;

                case "5":
                    await _staffHandler.DeleteStaff();
                    break;

                case "0":
                    running = false;
                    break;
            }
        }
    }
}
