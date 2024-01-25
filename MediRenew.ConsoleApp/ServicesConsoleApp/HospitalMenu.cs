using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus;
using Microsoft.Identity.Client;

namespace MediRenew.ConsoleApp.ServicesConsoleApp;

public class HospitalMenu(PatientMenu patientMenu, StaffMenu staffMenu)
{
    public readonly PatientMenu _patientMenu = patientMenu;
    public readonly StaffMenu _staffMenu = staffMenu;

    public async Task MenuAsync()
    {
        while (true)
        {
            Console.Clear();
            Header.StaticHeader();

            string[] menu =
            {
                "1. Patients",
                "2. Doctors",
                "3. Staff",
                "4. Pharmacy-list",
                "5. Prescriptions",
                "0. Exit application"
            };

            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine(menu[i]);
            }

            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    await _patientMenu.PatientMenuAsync();
                    break;

                case "2":
                    await _patientMenu.PatientMenuAsync();
                    break;

                case "3":
                    await _staffMenu.PatientMenuAsync();
                    break;

                case "4":
                    break;

                case "5":
                    break;

                case "0":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
