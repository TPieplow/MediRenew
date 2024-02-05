using Business.DTOs;
using Business.Interfaces;
using Infrastructure.Utils;
using MediRenew.ConsoleApp.Utils;
using Spectre.Console;
using static Infrastructure.Utils.ResultEnums;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

public class AppointmentHandler(IAppointmentService appointmentService, PatientHandler patientHandler, DoctorHandler doctorHandler)
{
    private readonly IAppointmentService _appointmentService = appointmentService;
    private readonly PatientHandler _patientHandler = patientHandler;
    private readonly DoctorHandler _doctorHandler = doctorHandler;

    public async Task AddAppointment()
    {
        try
        {
            Console.Clear();
            var existingDate = new AppointmentDTO();
            var newAppointment = new AppointmentDTO();
            AnsiConsole.Write(new Markup("[Red]Type cancel to abort operation[/]"));

            await _doctorHandler.ViewAllDoctors();
            TryConvert.SetPropertyWithConversion(id => newAppointment.DoctorId = id, "Enter doctor-ID");
            if (newAppointment.DoctorId == 0) return;

            await _patientHandler.ViewAllPatients();
            TryConvert.SetPropertyWithConversion(id => newAppointment.PatientId = id, "Enter patient-ID");
            if (newAppointment.PatientId == 0) return;

            newAppointment.Date = Convert.ToDateTime(Cancel.AddOrAbort("Enter the appointment-date (format as xxxx-xx-xx xx:xx:xx): "));
            if (newAppointment.Date <= DateTime.Now) return;

            newAppointment.Comments = Cancel.AddOrAbort("Comment on appointment: ");
            if (newAppointment.Comments == null) return;

            var result = await _appointmentService.AddApointment(newAppointment);

            if (result == Result.Failure)
            {
                existingDate = await _appointmentService.GetOneAppointment(newAppointment.PatientId);
                ReturnMessage<AppointmentDTO>(CrudOperation.Create, result, $"Patient already has an appointment: {existingDate.Date}");
            }
            else
            {
                ReturnMessage<AppointmentDTO>(CrudOperation.Create, result, "");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }

    public async Task ViewOneWithPatId()
    {
        try
        {
            Console.Clear();
            await _patientHandler.ViewAllPatients();
            Console.WriteLine("Enter the patients ID");
            AppointmentDTO appointment = null!;

            if (int.TryParse(Console.ReadLine()!, out int Id))
            {
                appointment = await _appointmentService.GetOneAppointment(Id);

                if (appointment is not null)
                {
                    var table = new Table();

                    table.AddColumn("[yellow]Date[/]");
                    table.AddColumn("[yellow]Doctor[/]");
                    table.AddColumn("[yellow]Patient[/]");
                    table.AddColumn("[yellow]Comment[/]");


                    table.AddRow(
                        appointment.Date.ToString(),
                        appointment.DoctorName,
                        appointment.PatientName,
                        appointment.Comments!
                    );

                    AnsiConsole.Write(table);
                    DisplayMessage.Message("");
                }
                else
                {
                    DisplayMessage.Message("Patient has no appointment.");
                }
            }
            else
            {
                DisplayMessage.Message("Invalid input.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }

    public async Task GetAllAppointments()
    {
        try
        {
            Console.Clear();
            var appointments = await _appointmentService.GetAllAppointments();

            if (appointments is not null)
            {
                var table = new Table();

                table.AddColumn("[yellow]Patient-ID[/]");
                table.AddColumn("[yellow]Patient name[/]");
                table.AddColumn("[yellow]Doctor ID[/]");
                table.AddColumn("[yellow]Doctor Name[/]");
                table.AddColumn("[yellow]Date[/]");
                table.AddColumn("[yellow]Comments[/]");

                foreach (AppointmentDTO appointment in appointments)
                {
                    table.AddRow(
                        appointment.PatientId.ToString(),
                        appointment.PatientName,
                        appointment.DoctorId.ToString(),
                        appointment.DoctorName,
                        appointment.Date.ToString(),
                        appointment.Comments ?? "No comment"
                    );
                }
                AnsiConsole.Write(table);
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    public async Task UpdateAppointmentById()
    {
        try
        {
            Console.Clear();
            await GetAllAppointments();
            Console.WriteLine("Enter the patitent-Id of the appointment you want to update: ");
            if (int.TryParse(Console.ReadLine(), out int patientId))
            {
                var appointmentToUpdate = await _appointmentService.GetOneAppointment(patientId);

                if (appointmentToUpdate is null)
                {
                    DisplayMessage.Message("Appointment not found");
                }
                else
                {
                    Console.Write("Enter the new date (Format as xxxx-xx-xx xx:xx:xx): ");
                    appointmentToUpdate.Date = Convert.ToDateTime(Console.ReadLine()!);

                    Console.Write("Add any comments: ");
                    appointmentToUpdate.Comments = Console.ReadLine()!;

                    var result = await _appointmentService.UpdateAppointment(appointmentToUpdate);

                    if (result == Result.Failure)
                    {
                        ReturnMessage<AppointmentDTO>(CrudOperation.Update, result, "Invalid ID, couldnt update appointment. Please try again...");
                    }
                    else
                    {
                        ReturnMessage<DoctorDTO>(CrudOperation.Update, result, "");
                    }
                }
            }
            else
            {
                DisplayMessage.Message("Invalid ID. Please try again...");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($" ERROR: {ex.Message}");
        }
    }

    public async Task<int?> DeleteAppointmentByPatientId()
    {
        try
        {
            Console.Clear();
            await GetAllAppointments();
            Console.WriteLine("Enter Id of the patient which appointment you want to remove: ");
            if (int.TryParse(Console.ReadLine(), out var patientId))
            {
                var result = await _appointmentService.RemoveAppointmentAsync(patientId);
                if (result == Result.Failure)
                {
                    ReturnMessage<DoctorDTO>(CrudOperation.Delete, result, "Invalid ID, couldnt delete appointment. Please try again...");
                }
                else
                {
                    ReturnMessage<DoctorDTO>(CrudOperation.Delete, result, "");
                }
            }
            else
            {
                DisplayMessage.Message("Invalid ID, please try again...");
            }
        }
        catch (Exception ex)
        {
            DisplayMessage.Message($" ERROR: {ex.Message}");
        }
        return null;
    }

    public async Task RemoveAppointmentsAfterDateAsync()
    {
        var appointments = await _appointmentService.GetAllAppointments();

        foreach (AppointmentDTO a in appointments)
        {
            if (a.Date < DateTime.Now)
            {
                await _appointmentService.RemoveAppointmentAsync(a.PatientId);
            }
        }
    }
}
