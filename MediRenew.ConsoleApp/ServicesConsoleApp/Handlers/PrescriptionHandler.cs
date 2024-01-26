using Business.DTOs;
using Business.Services;
using Infrastructure.Utils;
using MediRenew.ConsoleApp.Utils;
using Spectre.Console;
using static Infrastructure.Utils.ResultEnums;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

public class PrescriptionHandler(PrescriptionService prescriptionService, PatientHandler patientHandler)
{
    private readonly PrescriptionService _prescriptionService = prescriptionService;
    private readonly PatientHandler _patientHandler = patientHandler;

    public async Task AddPrescription()
    {
        try
        {

            Console.Clear();
            var newPrescription = new PrescriptionDTO();

            newPrescription.Date = DateTime.Now;

            newPrescription.DoctorId = Convert.ToInt32(Cancel.AddOrAbort("Enter your DoctorId: "));
            if (newPrescription.DoctorId == 0) return;

            await _patientHandler.ViewAllPatients();
            newPrescription.PatientId = Convert.ToInt32(Cancel.AddOrAbort("Enter the patients Id: "));
            if (newPrescription.PatientId == 0) return;

            newPrescription.PharmacyId = Convert.ToInt32(Cancel.AddOrAbort("Enter the medication Id (check medication-list for ID-No): "));
            if (newPrescription.PharmacyId == 0) return;

            newPrescription.Dosage = Cancel.AddOrAbort("Enter Dosage: ");
            if (newPrescription.Dosage == null) return;

            var result = await _prescriptionService.AddPrescriptionAsync(newPrescription);

            switch (result)
            {
                case Result.Success:
                    ReturnMessage<PrescriptionDTO>(CrudOperation.Create, result, "");
                    break;
                case Result.Failure:
                    ReturnMessage<PrescriptionDTO>(CrudOperation.Create, result, "Invalid ID input (non-existent)");
                    break;
                case Result.NotFound:
                    ReturnMessage<PrescriptionDTO>(CrudOperation.Create, result, "");
                    break;
                default:
                    ReturnMessage<PrescriptionDTO>(CrudOperation.Create, result, "");
                    break;
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
                DisplayMessage.Message("");
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }


    public async Task ViewOnePrescriptionWithId()
    {
        try
        {
            Console.Clear();
            Console.Write("Enter the Id of the patient: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var patientPres = await _prescriptionService.GetPatientPrescriptions(id);

                if (patientPres.Any())
                {
                    foreach (var item in patientPres)
                    {
                        Console.WriteLine($"PrescriptionNo. {item.Id}");
                        Console.WriteLine($"Doctor: {item.Doctor.FirstName} {item.Doctor.LastName}");
                        Console.WriteLine($"Patient: {item.Patient.FirstName} {item.Patient.LastName}");
                        Console.WriteLine($"Cost: {item.Cost}");
                        Console.WriteLine($"Dosage: {item.Dosage}");
                        Console.WriteLine($"MedicationName: {item.Pharmacy.MedicationName}");
                    }
                    Console.ReadKey();
                }
                else
                {
                    DisplayMessage.Message("Patient not found.");
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
            Console.WriteLine("Enter Id of the prescription you want to remove: ");
            if (int.TryParse(Console.ReadLine(), out var prescriptionId))
            {
                var result = await _prescriptionService.RemovePrescriptionAsync(prescriptionId);
                switch (result)
                {
                    case Result.Success:
                        ReturnMessage<PrescriptionDTO>(CrudOperation.Delete, result, "");
                        break;
                    case Result.Failure:
                        ReturnMessage<PrescriptionDTO>(CrudOperation.Delete, result, "");
                        break;
                    case Result.NotFound:
                        ReturnMessage<PrescriptionDTO>(CrudOperation.Delete, result, "");
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
}

