using Business.DTOs;
using Business.Services;
using MediRenew.ConsoleApp.Utils;
using Spectre.Console;
using static MediRenew.ConsoleApp.Utils.ResultEnums;


namespace MediRenew.ConsoleApp.ServicesConsoleApp;

public class PatientHandler
{
    private readonly PatientService _patientService;

    public PatientHandler(PatientService patientService)
    {
        _patientService = patientService;
    }

    public async Task AddPatient()
    {
        try
        {
            Console.Clear();
            var newPatient = new PatientDTO();

            Console.WriteLine("Enter first name: ");
            newPatient.FirstName = Console.ReadLine()?.Trim()!;

            Console.WriteLine("Enter last name: ");
            newPatient.LastName = Console.ReadLine()?.Trim()!;

            Console.WriteLine("Enter email: ");
            newPatient.Email = Console.ReadLine()?.Trim()!;

            Console.WriteLine("Enter phone number: ");
            newPatient.PhoneNumber = Console.ReadLine()?.Trim()!;

            Console.WriteLine("Enter address: ");
            newPatient.Address = Console.ReadLine()?.Trim()!;

            Console.WriteLine("Enter postal code: ");
            newPatient.PostalCode = Console.ReadLine()?.Trim()!;

            Console.WriteLine("Enter city: ");
            newPatient.City = Console.ReadLine()?.Trim()!;

            var result = await _patientService.AddPatientAsync(newPatient);

            switch (result)
            {
                case Result.Success:
                    ReturnMessage<PatientDTO>(CrudOperation.Create, result);
                    break;
                case Result.Failure:
                    ReturnMessage<PatientDTO>(CrudOperation.Create, result);
                    break;
                case Result.NotFound:
                    ReturnMessage<PatientDTO>(CrudOperation.Create, result);
                    break;
                default:
                    ReturnMessage<PatientDTO>(CrudOperation.Create, result);
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }

    public async Task ViewOnePatientWithId()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Enter Id");
            PatientDTO patient = null!;

            if (int.TryParse(Console.ReadLine()!, out int id))
            {
                patient = await _patientService.GetOnePatient(id);

                if (patient != null)
                {
                    var table = new Table();

                    table.AddColumn("[yellow]ID[/]");
                    table.AddColumn("[yellow]First name[/]");
                    table.AddColumn("[yellow]Last name[/]");
                    table.AddColumn("[yellow]Address[/]");
                    table.AddColumn("[yellow]City[/]");
                    table.AddColumn("[yellow]Postal Code[/]");
                    table.AddColumn("[yellow]Phone number[/]");
                    table.AddColumn("[yellow]Email[/]");
                    table.AddColumn("[yellow]Dosage[/]");
                    table.AddColumn("[yellow]Medication-type[/]");

                    table.AddRow(
                        patient.Id.ToString(),
                        patient.FirstName,
                        patient.LastName,
                        patient.Address,
                        patient.City,
                        patient.PostalCode,
                        patient.PhoneNumber,
                        patient.Email,
                        patient.Dosage ??= "N/A",
                        patient.MedicationName ??= "N/A"
                    );

                    AnsiConsole.Write(table);
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
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }

    public async Task ViewAllPatiens()
    {
        try
        {
            Console.Clear();
            IEnumerable<PatientDTO> patients = await _patientService.GetAllPatients();

            if (patients is not null)
            {
                var table = new Table();

                table.AddColumn("[yellow]Patient-ID[/]");
                table.AddColumn("[yellow]First name[/]");
                table.AddColumn("[yellow]Last name[/]");
                table.AddColumn("[yellow]Address[/]");
                table.AddColumn("[yellow]City[/]");
                table.AddColumn("[yellow]Postal Code[/]");
                table.AddColumn("[yellow]Phone number[/]");
                table.AddColumn("[yellow]Email[/]");

                foreach (PatientDTO patient in patients)
                {
                    table.AddRow(
                        patient.Id.ToString(),
                        patient.FirstName,
                        patient.LastName,
                        patient.Address,
                        patient.City,
                        patient.PostalCode,
                        patient.PhoneNumber,
                        patient.Email
                    );
                }
                AnsiConsole.Write(table);
                DisplayMessage.Message("");
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    public async Task UpdatePatientById()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Enter Id of the patient you want to update: ");
            if (int.TryParse(Console.ReadLine(), out int patientId))
            {
                var patientToUpdate = await _patientService.GetOnePatient(patientId);

                if (patientToUpdate is not null)
                {
                    Console.Write("First Name:");
                    patientToUpdate.FirstName = Console.ReadLine()!;
                    Console.Write("Last Name:");
                    patientToUpdate.LastName = Console.ReadLine()!;
                    Console.Write("Address: ");
                    patientToUpdate.Address = Console.ReadLine()!;
                    Console.Write("Phone Number: ");
                    patientToUpdate.PhoneNumber = Console.ReadLine()!;
                    Console.Write("City: ");
                    patientToUpdate.City = Console.ReadLine()!;
                    Console.Write("Postal Code");
                    patientToUpdate.PostalCode = Console.ReadLine()!;
                    Console.Write("E-mail: ");
                    patientToUpdate.Email = Console.ReadLine()!;

                    var result = await _patientService.UpdatePatientAsync(patientToUpdate);

                    ReturnMessage<PatientDTO>(CrudOperation.Update, result);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($" ERROR: {ex.Message}");
        }
    }

    public async Task<int?> DeletePatientById()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Enter Id of the patient you want to remove: ");
            if (int.TryParse(Console.ReadLine(), out var patientId))
            {
                var result = await _patientService.RemovePatientService(patientId);
                switch (result)
                {
                    case Result.Success:
                        ReturnMessage<PatientDTO>(CrudOperation.Delete, result);
                        break;
                    case Result.Failure:
                        ReturnMessage<PatientDTO>(CrudOperation.Delete, result);
                        break;
                    case Result.NotFound:
                        ReturnMessage<PatientDTO>(CrudOperation.Delete, result);
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
