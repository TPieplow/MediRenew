using Microsoft.Identity.Client;

namespace MediRenew.ConsoleApp.ServicesConsoleApp;

public class HospitalMenu(PatientHandler patient)
{
    public readonly PatientHandler _patient = patient;

    public async Task MenuAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to MediRenew, a Hospital Database Handler! ");

            string[] menu =
            {
                "1. Add",
                "2. ViewOne",
                "3. ViewAll",
                "4. Update",
                "5. Delete",
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
                    await _patient.AddPatient();
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
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
