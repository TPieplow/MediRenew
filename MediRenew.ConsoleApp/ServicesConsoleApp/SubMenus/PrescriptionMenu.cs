using Business.Services;
using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;
using MediRenew.ConsoleApp.Utils;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus;

public class PrescriptionMenu(PrescriptionHandler prescriptionHandler)
{

    private readonly PrescriptionHandler _prescriptionHandler = prescriptionHandler;

    public async Task PrescriptionMenuAsync()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Header.StaticHeader();
            Console.WriteLine("What would you like to do? ");

            string[] menu =
            {
                "1. Add prescription",
                "2. Show all prescriptions for one patient",
                "3. View all prescriptions",
                "4. Delete prescription",
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
                    await _prescriptionHandler.AddPrescription();
                    break;

                case "2":
                    await _prescriptionHandler.ViewOnePrescriptionWithId();
                    break;

                case "3":
                    await _prescriptionHandler.ViewAllPrescriptions();
                    DisplayMessage.Message("");
                    break;

                case "4":
                    await _prescriptionHandler.DeletePrescriptionById();
                    break;

                case "0":
                    running = false;
                    break;
            }
        }
    }
}
