using Business.DTOs;
using Business.Interfaces;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using System.Diagnostics;
using static Infrastructure.Utils.ResultEnums;

namespace Business.Services;

public class StaffService(IStaffRepository staffRepository) : IStaffService
{
    private readonly IStaffRepository _staffRepository = staffRepository;

    public async Task<Result> AddStaffMemberAsync(StaffDTO staff)
    {
        try
        {
            //if (staff.DepartmentId <= 0)
            //{
            //    return Result.Failure;
            //}

            if(_staffRepository.Exists(x => x.Id == staff.Id))
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

    public async Task<StaffDTO> GetOneStaffMemberAsync(int staffId)
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

    public async Task<IEnumerable<StaffDTO>> GetAllStaffAsync()
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
            if (existingNumber is not null && !existingNumber.Id.Equals(staff.Id))
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

    public async Task<Result> DeleteStaffMemberAsync(int staffId)
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
