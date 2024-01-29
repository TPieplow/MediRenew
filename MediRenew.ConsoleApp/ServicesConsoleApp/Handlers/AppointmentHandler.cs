﻿using Business.DTOs;
using Business.Services;
using Infrastructure.Repositories;
using Infrastructure.Utils;
using MediRenew.ConsoleApp.Utils;
using Spectre.Console;
using static Infrastructure.Utils.ResultEnums;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

public class AppointmentHandler(AppointmentService appointmentService)
{
    private readonly AppointmentService _appointmentService = appointmentService;

    public async Task AddAppointment()
    {
        try
        {

            Console.Clear();
            var existingDate = new AppointmentDTO();
            var newAppointment = new AppointmentDTO();
            Console.WriteLine("Enter cancel or empty field to abort.");

            newAppointment.DoctorId = Convert.ToInt32(Cancel.AddOrAbort("Enter ID for the doctor: "));
            if (newAppointment.DoctorId == 0) return;

            newAppointment.PatientId = Convert.ToInt32(Cancel.AddOrAbort("Enter ID for the patient: "));
            if (newAppointment.PatientId == 0) return;

            newAppointment.Date = Convert.ToDateTime(Cancel.AddOrAbort("Enter the appointment-date (format as xxxx-xx-xx xx:xx:xx): "));
            if (newAppointment.Date <= DateTime.Now) return;

            newAppointment.Comments = Cancel.AddOrAbort("Comment on appointment: ");
            if (newAppointment.Comments == null) return;

            var result = await _appointmentService.AddApointment(newAppointment);

            if (result == Result.Failure)
            {
                existingDate = await _appointmentService.GetOneAppointment(newAppointment.PatientId);
            }

            switch (result)
            {
                case Result.Success:
                    ReturnMessage<AppointmentDTO>(CrudOperation.Create, result, "");
                    break;
                case Result.Failure:
                    ReturnMessage<AppointmentDTO>(CrudOperation.Create, result, $"This patient already has an appointment: {existingDate.Date}");
                    break;
                case Result.NotFound:
                    ReturnMessage<AppointmentDTO>(CrudOperation.Create, result, "");
                    break;
                default:
                    ReturnMessage<AppointmentDTO>(CrudOperation.Create, result, "");
                    break;
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
                    Console.ReadKey();
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
                DisplayMessage.Message("");
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    public async Task UpdateAppointmentById()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Enter Id of the appointment you want to update: ");
            if (int.TryParse(Console.ReadLine(), out int patientId))
            {
                var appointmentToUpdate = await _appointmentService.GetOneAppointment(patientId);

                if (appointmentToUpdate is null)
                {
                    Console.WriteLine("Appointment not found");
                    Console.ReadKey();
                }
                else
                {
                    Console.Write("Enter the new date (Format as xxxx-xx-xx xx:xx:xx): ");
                    appointmentToUpdate.Date = Convert.ToDateTime(Console.ReadLine()!);

                    Console.Write("Add any comments: ");
                    appointmentToUpdate.Comments = Console.ReadLine()!;

                    var result = await _appointmentService.UpdateAppointment(appointmentToUpdate);

                    switch (result)
                    {
                        case Result.Success:
                            ReturnMessage<AppointmentDTO>(CrudOperation.Update, result, "Appointment successfully updated.");
                            break;
                        case Result.Failure:
                            ReturnMessage<AppointmentDTO>(CrudOperation.Update, result, "");
                            break;
                        default:
                            ReturnMessage<AppointmentDTO>(CrudOperation.Update, result, "Unexpected error from update operation.");
                            break;
                    }
                }
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
            Console.WriteLine("Enter Id of the patient which appointment you want to remove: ");
            if (int.TryParse(Console.ReadLine(), out var patientId))
            {
                var result = await _appointmentService.RemoveAppointmentAsync(patientId);
                switch (result)
                {
                    case Result.Success:
                        ReturnMessage<AppointmentDTO>(CrudOperation.Delete, result, "");
                        break;
                    case Result.Failure:
                        ReturnMessage<AppointmentDTO>(CrudOperation.Delete, result, "");
                        break;
                    case Result.NotFound:
                        ReturnMessage<AppointmentDTO>(CrudOperation.Delete, result, "");
                        break;
                }
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