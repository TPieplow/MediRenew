using Infrastructure.Repositories;

namespace MediRenew.ConsoleApp.ServicesConsoleApp;

public class PatientService
{
    private readonly PatientRepository _patientRepository;

    public PatientService(PatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

   
}
