using Business.DTOs;
using Infrastructure.HospitalEntities;
using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IPrescriptionService
    {
        Task<ResultEnums.Result> AddPrescriptionAsync(PrescriptionDTO newPrescription);
        Task<IEnumerable<PrescriptionDTO>> GetAllPrescriptions();
        Task<IEnumerable<PrescriptionEntity>> GetPatientPrescriptions(int id);
        Task<ResultEnums.Result> RemovePrescriptionAsync(int prescriptionId);
    }
}