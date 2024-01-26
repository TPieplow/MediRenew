using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MediRenew.ConsoleApp.ServicesConsoleApp;

public class HospitalMenu(PatientMenu patientMenu, PrescriptionMenu prescriptionMenu, DoctorMenu doctorMenu, StaffMenu staffMenu, AppointmentMenu appointmentMenu)
{
    public readonly PatientMenu _patientMenu = patientMenu;
    public readonly PrescriptionMenu _prescriptionMenu = prescriptionMenu;
    public readonly DoctorMenu _doctorMenu = doctorMenu;
    public readonly StaffMenu _staffMenu = staffMenu;
    private readonly AppointmentMenu _appointmentMenu = appointmentMenu;

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
                    await _staffMenu.StaffMenuAsync();
                    break;

                case "4":

                    break;

                case "5":
                    await _prescriptionMenu.PrescriptionMenuAsync();
                    break;
                case "6":
                    await _appointmentMenu.AppointmentMenuAsync();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
