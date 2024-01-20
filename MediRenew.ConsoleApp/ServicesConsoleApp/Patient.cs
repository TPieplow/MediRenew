using Business.DTOs;
using Business.Services;
using Infrastructure.HospitalEntities;
using Infrastructure.Repositories;

namespace MediRenew.ConsoleApp.ServicesConsoleApp;

public class Patient
{
    private readonly PatientRepository _patientRepository;
    private readonly PatientService _patientService;

    public Patient(PatientRepository patientRepository, PatientService patientService)
    {
        _patientRepository = patientRepository;
        _patientService = patientService;
    }

    public async Task AddPatient()
    {
        try
        {
            Console.WriteLine("Enter first name: ");
            string firstName = Console.ReadLine()!;

            Console.WriteLine("Enter last name: ");
            string lastName = Console.ReadLine()!;

            Console.WriteLine("Enter email: ");
            string email = Console.ReadLine()!;

            Console.WriteLine("Enter phone number: ");
            string phoneNumber = Console.ReadLine()!;

            Console.WriteLine("Enter address: ");
            string address = Console.ReadLine()!;

            Console.WriteLine("Enter postal code: ");
            string postalCode = Console.ReadLine()!;

            Console.WriteLine("Enter city: ");
            string city = Console.ReadLine()!;

            var newPatient = new PatientDTO
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                PostalCode = postalCode,
                City = city,
            };

            bool successAdded = await _patientService.AddPatientAsync(newPatient);

            if (successAdded)
            {
                Console.WriteLine("Patient added successfully!");
            }
            else
            {
                Console.WriteLine("Failed to add patient. Email already exists");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }
}
