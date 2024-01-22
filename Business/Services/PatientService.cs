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

    public async Task<PatientDTO> GetOnePatient(int patientId)
    {
        try
        {
            var patientEntity = await _patientRepository.GetOneAsync(x => x.Id == patientId);

            if (patientEntity != null)
            {
                var patientDTO = new PatientDTO
                {
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
            else
            {
                return null!;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
            return null!;
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
                City = patient.City
            });
        }
        return patients;
    }
}
