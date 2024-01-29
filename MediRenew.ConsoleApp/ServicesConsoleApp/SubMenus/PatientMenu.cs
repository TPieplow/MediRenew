using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;
using MediRenew.ConsoleApp.Utils;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus
{
    public class PatientMenu(PatientHandler patient)
    {
        private readonly PatientHandler _patient = patient;

        public async Task PatientMenuAsync()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Header.StaticHeader();
                Console.WriteLine("What would you like to do? ");

                string[] menu =
                {
                "1. Add patient",
                "2. Find a patient through ID",
                "3. View all patients",
                "4. Update patient",
                "5. Delete patient",
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
                        await _patient.AddPatient();
                        break;

                    case "2":
                        await _patient.ViewOnePatientWithId();
                        break;

                    case "3":
                        await _patient.ViewAllPatients();
                        DisplayMessage.Message("");
                        break;

                    case "4":
                        await _patient.UpdatePatientById();
                        break;

                    case "5":
                        await _patient.DeletePatientById();
                        break;

                    case "0":
                        running = false;
                        break;
                }
            }
        }
    }
}
