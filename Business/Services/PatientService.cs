using Business.DTOs;
using Infrastructure.HospitalEntities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Diagnostics;

namespace Business.Services;

public class PatientService(PatientRepository patientRepository)
{
    private readonly PatientRepository _patientRepository = patientRepository;

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

    public async Task<PatientEntity> GetOnePatient(int patientId)
    {
        try
        {
            if (_patientRepository.Exists(x => x.Id == patientId))
            {
                var patient = await _patientRepository.GetOneAsync(x => x.Id == patientId);
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

    public async Task<IEnumerable<PatientDTO>> GetAllPatients()
    {
        var patients = new List<PatientDTO>();
        var result = await _patientRepository.GetAllAsync();

        foreach (var patient in result)
        {
            patients.Add(new PatientDTO
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                Address = patient.Address,
                PostalCode = patient.PostalCode,
                City = patient.City,
                Prescriptions = patient.Prescriptions
                    .Select(prescription => new PrescriptionDTO
                    {
                        Id = patient.Id,
                        Date = prescription.Date,
                        Cost = prescription.Cost,
                        Dosage = prescription.Dosage,
                        PharmacyId = prescription.PharmacyId,
                        DoctorId = prescription.DoctorId,
                        Pharmacy = new PharmacyDTO
                        {
                            Id = prescription.Pharmacy.Id,
                            MedicationName = prescription.Pharmacy.MedicationName
                        }
                    }).ToList(),
            });
        }
        return patients;
    }
}
