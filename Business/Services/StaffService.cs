using Business.DTOs;
using Business.Interfaces;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System.Diagnostics;
using static Infrastructure.Utils.ResultEnums;

namespace Business.Services;

public class StaffService(IStaffRepository staffRepository) : IStaffService
{
    private readonly IStaffRepository _staffRepository = staffRepository;

    public async Task<Result> AddStaffMember(StaffDTO staff)
    {
        try
        {
            if (staff.DepartmentId <= 0)
            {
                return Result.Failure;
            }

            var department = await _staffRepository.GetDepartmentByIdAsync(staff.DepartmentId);

            var newStaffEntity = new StaffEntity
            {
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                RoleName = staff.RoleName,
                PhoneNumber = staff.PhoneNumber,
                DepartmentId = staff.DepartmentId,
            };
            var result = await _staffRepository.CreateAsync(newStaffEntity);


            if (result is not null)
            {
                return Result.Success;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR : {ex.Message}");
        }
        return Result.Failure;
    }

    public async Task<StaffDTO> GetOneStaffMember(int staffId)
    {
        try
        {
            var staffEntity = await _staffRepository.GetOneAsync(x => x.Id == staffId);

            if (staffEntity is null)
            {
                return null!;
            }

            var staffDTO = new StaffDTO
            {
                Id = staffEntity.Id,
                FirstName = staffEntity.FirstName,
                LastName = staffEntity.LastName,
                RoleName = staffEntity.RoleName,
                PhoneNumber = staffEntity.PhoneNumber,
                Department = staffEntity.Department,
            };

            return staffDTO;
        }
        catch (Exception ex) { Debug.WriteLine($"ERROR : {ex.Message} "); }
        return null!;
    }

    public async Task<IEnumerable<StaffDTO>> GetAllStaff()
    {
        try
        {
            var staffEntity = (await _staffRepository.GetAllStaffMembersIncludeDepartAsync()).ToList();
            return staffEntity.Select(x => new StaffDTO()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                RoleName = x.RoleName,
                PhoneNumber = x.PhoneNumber,
                DepartmentName = x.Department.DepartmentName
            });
        }
        catch (Exception ex) { Debug.WriteLine($"ERROR : {ex.Message} "); }
        return null!;
    }

    public async Task<Result> UpdateStaffAsync(StaffDTO staff)
    {
        try
        {
            var existingNumber = await _staffRepository.GetOneAsync(x => x.PhoneNumber == staff.PhoneNumber);
            if (existingNumber is not null)
            {
                return Result.Failure;
            }

            var staffToUpdate = await _staffRepository.GetOneAsync(x => x.Id == staff.Id);
            if (staffToUpdate is not null)
            {
                staffToUpdate.FirstName = staff.FirstName;
                staffToUpdate.LastName = staff.LastName;
                staffToUpdate.PhoneNumber = staff.PhoneNumber;
                staffToUpdate.RoleName = staff.RoleName;
                staffToUpdate.Department.Id = staff.Department.Id;

                await _staffRepository.UpdateAsync(staffToUpdate);
                return Result.Success;
            }
        }
        catch (Exception ex) { Debug.WriteLine($"ERROR : {ex.Message} "); }
        return Result.Failure;
    }

    public async Task<Result> DeleteStaffMember(int staffId)
    {
        try
        {
            var staffToDelete = await _staffRepository.DeleteAsync(x => x.Id == staffId);

            if (staffToDelete)
            {
                return Result.Success;
            }
            else { return Result.Failure; }
        }
        catch (Exception ex) { Debug.WriteLine($"ERROR : {ex.Message} "); }
        return Result.Failure;
    }
}
