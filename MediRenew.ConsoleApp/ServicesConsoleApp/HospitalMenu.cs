using Microsoft.Identity.Client;

namespace MediRenew.ConsoleApp.ServicesConsoleApp;

public class HospitalMenu(PatientMenu patientMenu, PrescriptionMenu prescriptionMenu, DoctorMenu doctorMenu)
{

    public readonly PatientMenu _patientMenu = patientMenu;
    public readonly PrescriptionMenu _prescriptionMenu = prescriptionMenu;
    public readonly DoctorMenu _doctorMenu = doctorMenu;

    public async Task MenuAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to MediRenew, a Hospital Database Handler! ");

            string[] menu =
            {
                "1. Patients",
                "2. Doctors",
                "3. Staff",
                "4. Pharmacy-list",
                "5. Prescriptions",
                "6. Appointments",
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
                    await _doctorMenu.DoctorMenuAsync();
                    break;

                case "3":
                    break;

                case "4":
                    break;

                case "5":
                    await _prescriptionMenu.PrescriptionMenuAsync();
                    break;
                case "6":
                    break;

                case "0":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
