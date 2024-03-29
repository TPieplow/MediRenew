﻿using Business.DTOs;
using Business.Interfaces;
using Infrastructure.Utils;
using MediRenew.ConsoleApp.Utils;
using Moq.Protected;
using Spectre.Console;
using static Infrastructure.Utils.ResultEnums;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

public class PrescriptionHandler(IPrescriptionService prescriptionService, PatientHandler patientHandler, DoctorHandler doctorHandler, PharmacyHandler pharmacyHandler)
{
    private readonly IPrescriptionService _prescriptionService = prescriptionService;
    private readonly PatientHandler _patientHandler = patientHandler;
    private readonly DoctorHandler _doctorHandler = doctorHandler;
    private readonly PharmacyHandler _pharmacyHandler = pharmacyHandler;

    public async Task AddPrescription()
    {
        try
        {
            Console.Clear();
            var newPrescription = new PrescriptionDTO();

            newPrescription.Date = DateTime.Now;

            await _doctorHandler.ViewAllDoctors();
            AnsiConsole.Write(new Markup("[Red]Type cancel to abort operation[/]"));

            TryConvert.SetPropertyWithConversion(doctorId => newPrescription.DoctorId = doctorId, "\nEnter Doctor-ID: ");
            if (newPrescription.DoctorId == 0) return;

            await _patientHandler.ViewAllPatients();
            TryConvert.SetPropertyWithConversion(patientId => newPrescription.PatientId = patientId, "Enter patient-ID: ");
            if (newPrescription.PatientId == 0) return;

            await _pharmacyHandler.ViewAllPharmacies();
            TryConvert.SetPropertyWithConversion(pharmacyId => newPrescription.PharmacyId = pharmacyId, "Enter pharmacy-ID: ");
            if (newPrescription.PharmacyId == 0) return;

            newPrescription.Dosage = Cancel.AddOrAbort("Enter Dosage: ");
            if (newPrescription.Dosage == null) return;

            TryConvert.SetPropertyWithConversion(cost => newPrescription.Cost = cost, "Enter the cost: ");
            if (newPrescription.Cost == 0) return;

            var result = await _prescriptionService.AddPrescriptionAsync(newPrescription);
            if (result == Result.Failure)
            {
                ReturnMessage<PrescriptionDTO>(CrudOperation.Create, result, "Couldnt add prescription, please try again.");
            }
            else
            {
                ReturnMessage<PrescriptionDTO>(CrudOperation.Create, result, "");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }

    public async Task ViewAllPrescriptions()
    {
        try
        {
            Console.Clear();
            var prescriptions = await _prescriptionService.GetAllPrescriptions();

            if (prescriptions is not null)
            {
                var table = new Table();

                table.AddColumn("[yellow]Prescription No.[/]");
                table.AddColumn("[yellow]Patient-ID[/]");
                table.AddColumn("[yellow]Patient name[/]");
                table.AddColumn("[yellow]Doctor-ID[/]");
                table.AddColumn("[yellow]Doctor name[/]");
                table.AddColumn("[yellow]Medication[/]");
                table.AddColumn("[yellow]Dosage[/]");
                table.AddColumn("[yellow]Cost[/]");
                table.AddColumn("[yellow]Date[/]");

                foreach (PrescriptionDTO p in prescriptions)
                {
                    table.AddRow(
                        p.Id.ToString(),
                        p.PatientId.ToString(),
                        p.PatientName,
                        p.DoctorId.ToString(),
                        p.DoctorName,
                        p.MedicationName,
                        p.Dosage,
                        p.Cost.ToString(),
                        p.Date.ToString()
                    );
                }
                AnsiConsole.Write(table);
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    public async Task ViewOnePrescriptionWithId()
    {
        try
        {
            Console.Clear();
            await _patientHandler.ViewAllPatients();
            Console.Write("\nEnter the Id of the patient: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var patientPres = await _prescriptionService.GetPatientPrescriptions(id);

                if (patientPres.Any())
                {
                    var table = new Table();

                    table.AddColumn("[yellow]Prescription No.[/]");
                    table.AddColumn("[yellow]Patient-ID[/]");
                    table.AddColumn("[yellow]Patient name[/]");
                    table.AddColumn("[yellow]Doctor-ID[/]");
                    table.AddColumn("[yellow]Doctor name[/]");
                    table.AddColumn("[yellow]Medication[/]");
                    table.AddColumn("[yellow]Dosage[/]");
                    table.AddColumn("[yellow]Cost[/]");
                    table.AddColumn("[yellow]Date[/]");

                    foreach (var item in patientPres)
                    {
                        table.AddRow(
                            item.Id.ToString(),
                            item.PatientId.ToString(),
                            item.Patient.FirstName + " " + item.Patient.LastName,
                            item.DoctorId.ToString(),
                            item.Doctor.FirstName + " " + item.Doctor.LastName,
                            item.Pharmacy.MedicationName,
                            item.Dosage,
                            item.Cost.ToString(),
                            item.Date.ToString()
                        );
                    }
                    AnsiConsole.Write(table);
                    Console.ReadKey();
                }
                else
                {
                    DisplayMessage.Message("Patient does not exist or has no active prescriptions.");
                }
            }
            else
            {
                DisplayMessage.Message("Invalid input.");
            }
        }
        catch (Exception ex)
        { Console.WriteLine($"ERROR: {ex.Message}"); }
    }

    public async Task<int?> DeletePrescriptionById()
    {
        try
        {
            Console.Clear();
            await ViewAllPrescriptions();
            Console.WriteLine("Enter Id of the prescription you want to remove: ");
            if (int.TryParse(Console.ReadLine(), out var prescriptionId))
            {
                var result = await _prescriptionService.RemovePrescriptionAsync(prescriptionId);
                if (result == Result.Failure)
                {
                    ReturnMessage<PrescriptionDTO>(CrudOperation.Delete, result, "Couldnt delete prescription, please try again.");
                }
                else
                {
                    ReturnMessage<PrescriptionDTO>(CrudOperation.Delete, result, "");
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
}

