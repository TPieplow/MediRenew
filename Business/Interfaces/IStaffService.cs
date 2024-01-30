using Business.DTOs;
using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IStaffService
    {
        Task<ResultEnums.Result> AddStaffMember(StaffDTO staff);
        Task<ResultEnums.Result> DeleteStaffMember(int patientId);
        Task<IEnumerable<StaffDTO>> GetAllStaff();
        Task<StaffDTO> GetOneStaffMember(int staffId);
        Task<ResultEnums.Result> UpdateStaffAsync(StaffDTO staff);
    }
}