using Business.DTOs;
using Business.Services;
using Spectre.Console;
using System.Runtime.ExceptionServices;


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

            bool successAdded = await _patientService.AddPatientAsync(newPatient);

            if (successAdded)
            {
                Console.WriteLine("Patient added successfully!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Failed to add patient. Email already exists");
                Console.ReadKey();
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
                Console.ReadKey();
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }


}
