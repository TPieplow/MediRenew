using Business.DTOs;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using MediRenew.ConsoleApp.Utils;
using System.Diagnostics;

using static MediRenew.ConsoleApp.Utils.ResultEnums;

namespace Business.Services;

public class PatientService(PatientRepository patientRepository, PrescriptionRepository prescriptionRepository)
{
    private readonly PatientRepository _patientRepository = patientRepository;
    private readonly PrescriptionRepository _prescriptionRepository = prescriptionRepository;

<<<<<<< HEAD

    public async Task<bool> AddPatientAsync(PatientDTO newPatient)
=======
    public async Task<Result> AddPatientAsync(PatientDTO newPatient)
>>>>>>> teds_branch
    {
        try
        {
            if (_patientRepository.Exists(x => x.Email == newPatient.Email))
            {
                return Result.Failure;
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
                return Result.Success;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR : {ex.Message}");
        }
        return Result.Failure;
    }

    public async Task<PatientDTO> GetOnePatient(int patientId)
    {
        try
        {
            var patientEntity = await _patientRepository.GetOneAsync(x => x.Id == patientId);

            if (patientEntity == null)
            {
                return null!;
            }

<<<<<<< HEAD
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
<<<<<<< HEAD
                return patientDTO;
            }
            else
=======
            }

            patientDTO = new PatientDTO
>>>>>>> 1af591fab77db7569f3edabb131e97ea71d13d78
=======
            var patientDTO = new PatientDTO
>>>>>>> teds_branch
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
            var prescription = (await _prescriptionRepository.GetAllAsync())
                .FirstOrDefault(x => x.PatientId == patientEntity.Id);

            if (prescription is not null)
            {
                patientDTO.Dosage = prescription.Dosage;
                patientDTO.MedicationName = prescription.Pharmacy.MedicationName;
            }

            return patientDTO;
        }
        catch (Exception ex) { Console.WriteLine($"ERROR: {ex.Message}"); return null!; }
    }

    public async Task<IEnumerable<PatientDTO>> GetAllPatients()
    {
        try
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
        catch (Exception ex)
        { Console.WriteLine($"ERROR: {ex.Message}"); }
        return null!;

    }

    public async Task<Result> UpdatePatientAsync(PatientDTO updatedPatient)
    {
        try
        {
            var existingEmail = await _patientRepository.GetOneAsync(x => x.Email == updatedPatient.Email);
            if (existingEmail is not null)
            {
                return Result.Failure;
            }

            var existingPatient = await _patientRepository.GetOneAsync(x => x.Id == updatedPatient.Id);
            if (existingPatient is not null)
            {
                existingPatient.FirstName = updatedPatient.FirstName;
                existingPatient.LastName = updatedPatient.LastName;
                existingPatient.Address = updatedPatient.Address;
                existingPatient.City = updatedPatient.City;
                existingPatient.PostalCode = updatedPatient.PostalCode;
                existingPatient.Email = updatedPatient.Email;
                existingPatient.PhoneNumber = updatedPatient.PhoneNumber;

                await _patientRepository.UpdateAsync(existingPatient);
                return Result.Success;
            }
            else { return Result.NotFound; }
        }
        catch (Exception ex)
        { Console.WriteLine($"ERROR: {ex.Message}"); return Result.Failure; }
    }

    public async Task<Result> RemovePatientService(int patientId)
    {
        try
        {
            var deletePatient = await _patientRepository.DeleteAsync(x => x.Id == patientId);
            if (deletePatient)
            {
                return Result.Success;
            }
            else { return Result.Failure; }
        }
        catch (Exception ex)
        { Console.WriteLine($"ERROR: {ex.Message}"); return Result.Failure; }
    }

}
