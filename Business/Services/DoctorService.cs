using Business.DTOs;
using Infrastructure.HospitalEntities;
using System.Diagnostics;
using Infrastructure.Repositories;
using static Infrastructure.Utils.ResultEnums;

namespace Business.Services;

public class DoctorService
{
    private readonly DoctorRepository _doctorRepository;

    public DoctorService(DoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Result> AddDoctorAsync(DoctorDTO newDoctor)
    {
        try
        {
            if(_doctorRepository.Exists(x => x.PhoneNumber == newDoctor.PhoneNumber))
            {
                return Result.Failure;
            }
            var newDoctorEntity = new DoctorEntity
            {
                FirstName = newDoctor.FirstName,
                LastName = newDoctor.LastName,
                PhoneNumber = newDoctor.PhoneNumber,
                DepartmentId = newDoctor.DepartmentId
            };

            var result = await _doctorRepository.CreateAsync(newDoctorEntity);
            if (result is not null)
                return Result.Success;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR : {ex.Message}");
        }
        return Result.Failure;
    }

    public async Task<DoctorDTO> GetOneDoctorAsync(int doctorId)
    {
        try
        {
            var doctorEntity = await _doctorRepository.GetOneAsync(x => x.Id == doctorId);

            if (doctorEntity == null)
            {
                return null!;
            }


            var doctorDTO = new DoctorDTO
            {
                Id = doctorEntity.Id,
                FirstName = doctorEntity.FirstName,
                LastName = doctorEntity.LastName,
                PhoneNumber = doctorEntity.PhoneNumber,
                DepartmentName = doctorEntity.Department.DepartmentName
            };

            return doctorDTO;
        }
        catch (Exception ex) { Console.WriteLine($"ERROR: {ex.Message}"); return null!; }
    }

    public async Task<IEnumerable<DoctorDTO>> GetAllDoctors()
    {
        try
        {
            var result = (await _doctorRepository.GetAllAsync()).ToList();

            return result.Select(x => new DoctorDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                DepartmentName = x.Department.DepartmentName
            });
        }
        catch (Exception ex)
        { Console.WriteLine($"ERROR: {ex.Message}"); }
        return null!;
    }

    public async Task<Result> UpdateDoctorAsync(DoctorDTO updatedDoctor)
    {
        try
        {
            var existingDoctorPhone = await _doctorRepository.GetOneAsync(x => x.PhoneNumber == updatedDoctor.PhoneNumber);
            if (existingDoctorPhone is not null)
            {
                return Result.Failure;
            }

            var existingDoctor = await _doctorRepository.GetOneAsync(x => x.Id == updatedDoctor.Id);
            if (existingDoctor is not null)
            {
                existingDoctor.FirstName = updatedDoctor.FirstName;
                existingDoctor.LastName = updatedDoctor.LastName;
                existingDoctor.PhoneNumber = updatedDoctor.PhoneNumber;
                existingDoctor.DepartmentId = updatedDoctor.DepartmentId;


                await _doctorRepository.UpdateAsync(existingDoctor);
                return Result.Success;
            }
            else { return Result.NotFound; }
        }
        catch (Exception ex)
        { Console.WriteLine($"ERROR: {ex.Message}"); return Result.Failure; }
    }


    public async Task<Result> RemoveDoctorAsync(int doctorId)
    {
        try
        {
            var deleteDoctor = await _doctorRepository.DeleteAsync(x => x.Id == doctorId);
            if (deleteDoctor)
            {
                return Result.Success;
            }
            else { return Result.Failure; }
        }
        catch (Exception ex)
        { Console.WriteLine($"ERROR: {ex.Message}"); return Result.Failure; }
    }

}


