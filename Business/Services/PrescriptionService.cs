﻿using Business.DTOs;
using Business.Interfaces;
using Infrastructure.HospitalEntities;
using System.Diagnostics;
using static Infrastructure.Utils.ResultEnums;

namespace Business.Services;

public class PrescriptionService(IPrescriptionRepository prescriptionRepository) : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository = prescriptionRepository;

    public async Task<Result> AddPrescriptionAsync(PrescriptionDTO newPrescription)
    {
        try
        {
            var newPrescriptionEntity = new PrescriptionEntity
            {
                Date = newPrescription.Date,
                Dosage = newPrescription.Dosage,
                PatientId = newPrescription.PatientId,
                DoctorId = newPrescription.DoctorId,
                Cost = newPrescription.Cost,
                PharmacyId = newPrescription.PharmacyId
            };
            var result = await _prescriptionRepository.CreateAsync(newPrescriptionEntity);
            if (result is not null)
            {
                return Result.Success;
            }
            else
            {
                return Result.Failure;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR : {ex.Message}");
        }
        return Result.Failure;
    }

    public async Task<IEnumerable<PrescriptionEntity>> GetPatientPrescriptions(int id)
    {
        try
        {
            var presList = (await _prescriptionRepository.GetAllAsync()).Where(x => x.PatientId == id).ToList();
            return presList;
        }
        catch (Exception ex)
        { Console.WriteLine($"ERROR: {ex.Message}"); }
        return null!;
    }

    public async Task<IEnumerable<PrescriptionDTO>> GetAllPrescriptions()
    {
        try
        {
            var result = (await _prescriptionRepository.GetAllAsync()).ToList();

            return result.Select(x => new PrescriptionDTO
            {
                Id = x.Id,
                Date = x.Date,
                Cost = x.Cost,
                Dosage = x.Dosage,
                MedicationName = x.Pharmacy.MedicationName,
                DoctorName = x.Doctor.FirstName + " " + x.Doctor.LastName,
                PatientName = x.Patient.FirstName + " " + x.Patient.LastName,
                DoctorId = x.DoctorId,
                PatientId = x.PatientId
            });
        }
        catch (Exception ex)
        { Console.WriteLine($"ERROR: {ex.Message}"); }
        return null!;
    }

    public async Task<Result> RemovePrescriptionAsync(int prescriptionId)
    {
        try
        {
            var deletePrescription = await _prescriptionRepository.DeleteAsync(x => x.Id == prescriptionId);
            if (deletePrescription)
            {
                return Result.Success;
            }
            else { return Result.Failure; }
        }
        catch (Exception ex)
        { Console.WriteLine($"ERROR: {ex.Message}"); return Result.Failure; }
    }
}
