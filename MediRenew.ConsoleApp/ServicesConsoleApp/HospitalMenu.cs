using Microsoft.Identity.Client;

namespace MediRenew.ConsoleApp.ServicesConsoleApp;

public class HospitalMenu(PatientMenu patientMenu)
{
    public readonly PatientMenu _patientMenu = patientMenu;

    public async Task MenuAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to MediRenew, a Hospital Database Handler! ");

            string[] menu =
            {
<<<<<<< HEAD
                "1. Add",
                "2. ViewOne",
                "3. ViewAll", 
                "4. Update",
                "5. Delete",
=======
                "1. Patients",
                "2. Doctors",
                "3. Staff",
                "4. Pharmacy-list",
                "5. Prescriptions",
>>>>>>> 1af591fab77db7569f3edabb131e97ea71d13d78
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
