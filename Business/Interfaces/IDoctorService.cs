using Business.DTOs;
using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IDoctorService
    {
        Task<ResultEnums.Result> AddDoctorAsync(DoctorDTO newDoctor);
        Task<IEnumerable<DoctorDTO>> GetAllDoctors();
        Task<DoctorDTO> GetOneDoctorAsync(int doctorId);
        Task<ResultEnums.Result> RemoveDoctorAsync(int doctorId);
        Task<ResultEnums.Result> UpdateDoctorAsync(DoctorDTO updatedDoctor);
    }
}