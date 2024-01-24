using Business.DTOs;
using Infrastructure.HospitalEntities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Diagnostics;
using static Infrastructure.Utils.ResultEnums;

namespace Business.Services
{
    public class StaffService(StaffRepository staffRepository, DepartmentRepository departmentRepository)
    {
        private readonly StaffRepository _staffRepository = staffRepository;

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

                var staffDTO = new StaffDTO()
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
            catch (Exception ex) { Debug.WriteLine($"ERROR : {ex.Message} "); } return null!;
        }
    }
}
