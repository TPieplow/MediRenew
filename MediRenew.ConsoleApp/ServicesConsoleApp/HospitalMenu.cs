using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MediRenew.ConsoleApp.ServicesConsoleApp;

public class HospitalMenu(PatientMenu patientMenu, PrescriptionMenu prescriptionMenu, DoctorMenu doctorMenu, StaffMenu staffMenu, InvoiceMenu invoiceMenu, PharmacyMenu pharmacyMenu, AppointmentMenu appointmentMenu)
{
    public readonly PatientMenu _patientMenu = patientMenu;
    public readonly PrescriptionMenu _prescriptionMenu = prescriptionMenu;
    public readonly DoctorMenu _doctorMenu = doctorMenu;
    public readonly StaffMenu _staffMenu = staffMenu;
    public readonly InvoiceMenu _invoiceMenu = invoiceMenu;
    public readonly PharmacyMenu _pharmacyMenu = pharmacyMenu;
    private readonly AppointmentMenu _appointmentMenu = appointmentMenu;

    /// <summary>
    /// The main menu for the UI.
    /// </summary>
    /// <returns>The chosen menu</returns>
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
                "7. Invoices",
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
                    await _pharmacyMenu.PharmacyMenuAsync();
                    break;

                case "5":
                    await _prescriptionMenu.PrescriptionMenuAsync();
                    break;

                case "6":
                    await _appointmentMenu.AppointmentMenuAsync();
                    break;

                case "7":
                    await _invoiceMenu.InvoiceMenuAsync();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
