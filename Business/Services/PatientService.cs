using Business.DTOs;
using Infrastructure.HospitalEntities;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq;

namespace Business.Services;

public class PatientService(PatientRepository patientRepository, PrescriptionRepository prescriptionRepository)
{
    private readonly PatientRepository _patientRepository = patientRepository;
    private readonly PrescriptionRepository _prescriptionRepository = prescriptionRepository;

    public async Task<bool> AddPatientAsync(PatientDTO newPatient)
    {
        try
        {
            if (_patientRepository.Exists(x => x.Email == newPatient.Email))
            {
                var existingPatientEntity = _patientRepository.GetOneAsync(x => x.Email == newPatient.Email);

                return false;
            }

            var newPatientEntity = new PatientEntity
            {
                FirstName = newPatient.FirstName,
                LastName = newPatient.LastName,
                Email = newPatient.Email,
                PhoneNumber = newPatient.PhoneNumber,
                Address = newPatient.Address,
                PostalCode = newPatient.PostalCode,
                City = newPatient.City,
            };
            var result = await _patientRepository.CreateAsync(newPatientEntity);
            if (result is not null)
                return true;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR : {ex.Message}");
        }
        return false;
    }

    public async Task<PatientDTO> GetOnePatient(int patientId)
    {
        try
        {
            var patientEntity = await _patientRepository.GetOneAsync(x => x.Id == patientId);
            var patientDTO = new PatientDTO();
            var prescriptions = (await _prescriptionRepository.GetAllAsync()).ToList();


            if (patientEntity == null)
            {
                return null!;
            }

            if (prescriptions.Any(x => patientEntity.Id == x.PatientId))
            {
                return new PatientDTO
                {
                    Id = patientEntity.Id,
                    FirstName = patientEntity.FirstName,
                    LastName = patientEntity.LastName,
                    Address = patientEntity.Address,
                    City = patientEntity.City,
                    PostalCode = patientEntity.PostalCode,
                    PhoneNumber = patientEntity.PhoneNumber,
                    Email = patientEntity.Email,
                    Dosage = prescriptions.FirstOrDefault(x => x.PatientId == patientEntity.Id)!.Dosage,
                    MedicationName = prescriptions.FirstOrDefault(x => x.PatientId == patientEntity.Id)!.Pharmacy.MedicationName
                };
            }
            //foreach (var p in prescriptions)
            //{
            //    if (p.PatientId == patientEntity.Id)
            //    {
            //        patientDTO = new PatientDTO
            //        {
            //            Id = patientEntity.Id,
            //            FirstName = patientEntity.FirstName,
            //            LastName = patientEntity.LastName,
            //            Address = patientEntity.Address,
            //            City = patientEntity.City,
            //            PostalCode = patientEntity.PostalCode,
            //            PhoneNumber = patientEntity.PhoneNumber,
            //            Email = patientEntity.Email,
            //            Dosage = p.Dosage,
            //            MedicationName = p.Pharmacy.MedicationName
            //        };
            //        return patientDTO;
            //    }
            //}

            patientDTO = new PatientDTO
            {
                Id = patientEntity.Id,
                FirstName = patientEntity.FirstName,
                LastName = patientEntity.LastName,
                Address = patientEntity.Address,
                City = patientEntity.City,
                PostalCode = patientEntity.PostalCode,
                PhoneNumber = patientEntity.PhoneNumber,
                Email = patientEntity.Email
            };

            return patientDTO;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<PatientDTO>> GetAllPatients()
    {
        var result = (await _patientRepository.GetAllAsync()).ToList();

        return result.Select(x => new PatientDTO
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            Address = x.Address,
            PostalCode = x.PostalCode,
            City = x.City

        });
    }
}
