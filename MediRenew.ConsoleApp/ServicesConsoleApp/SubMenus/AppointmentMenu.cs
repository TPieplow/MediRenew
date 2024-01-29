using Business.DTOs;
using Infrastructure.Repositories;
using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus;

public class AppointmentMenu(AppointmentHandler appointmentHandler)
{
    private readonly AppointmentHandler _appointmentHandler = appointmentHandler;


    public async Task AppointmentMenuAsync()
    {
        await _appointmentHandler.RemoveAppointmentsAfterDateAsync();
        bool running = true;
        while (running)
        {
            Console.Clear();
            Header.StaticHeader();
            Console.WriteLine("What would you like to do? ");

            string[] menu =
            {
                "1. Add Appointment",
                "2. Find an appointment through Patient-ID",
                "3. View all appointments",
                "4. Update appointment",
                "5. Delete appointment",
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
                    await _appointmentHandler.AddAppointment();
                    break;

                case "2":
                    await _appointmentHandler.ViewOneWithPatId();
                    break;

                case "3":
                    await _appointmentHandler.GetAllAppointments();
                    break;

                case "4":
                    await _appointmentHandler.UpdateAppointmentById();
                    break;

                case "5":
                    await _appointmentHandler.DeleteAppointmentByPatientId();
                    break;

                case "0":
                    running = false;
                    break;
            }
        }
    }
}
