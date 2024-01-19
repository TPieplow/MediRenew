using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Diagnostics;

namespace Business.Services;

public class PatientService(PatientRepository patientRepository, CodeFirstDbContext codeFirstDbContext)
{
    private readonly PatientRepository _patientRepository = patientRepository;
    private readonly CodeFirstDbContext _codeFirstDbContext = codeFirstDbContext;


    public async Task<bool> AddPatient(PatientEntity newPatient)
    {
        try
        {
            if (_patientRepository.Exists(x => x.Id == newPatient.Id || x.Email == newPatient.Email))
            {
                return false;
            }

            await _patientRepository.Create(newPatient);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine($"ERROR : {ex.Message}"); }
       

    }

    public async Task<PatientEntity> ViewPatient(int patientId)
    {
        try
        {
            if (_patientRepository.Exists(x => x.Id == patientId))
            {
                var patient = await _patientRepository.GetOne(x => x.Id == patientId);
                if (patient is not null) 
                {
                    return patient;
                }
                else
                {
                    return null!;
                }
            }
            return null!;
        }
        catch (Exception ex)
        {

            Console.WriteLine($"ERROR: {ex.Message}");
            return null!; ;
        }
    }
}
