using Business.DTOs;
using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IPatientService
    {
        Task<ResultEnums.Result> AddPatientAsync(PatientDTO newPatient);
        Task<IEnumerable<PatientDTO>> GetAllPatients();
        Task<PatientDTO> GetOnePatient(int patientId);
        Task<ResultEnums.Result> RemovePatientAsync(int patientId);
        Task<ResultEnums.Result> UpdatePatientAsync(PatientDTO updatedPatient);
    }
}