using Business.DTOs;
using Infrastructure.HospitalEntities;
using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IPrescriptionService
    {
        /// <summary>
        /// Adds a new prescription the the database
        /// </summary>
        /// <param name="newPrescription">Prescription-model sent to repo</param>
        /// <returns>Result: Success if successful else failure</returns>
        Task<ResultEnums.Result> AddPrescriptionAsync(PrescriptionDTO newPrescription);

        /// <summary>
        /// Gets all prescriptions from database
        /// </summary>
        /// <returns>IEnum of prescriptions</returns>
        Task<IEnumerable<PrescriptionDTO>> GetAllPrescriptions();

        /// <summary>
        /// Gets all prescriptions for one patient
        /// </summary>
        /// <param name="id">Patient-Id</param>
        /// <returns>IEnum of prescriptions for a specific patient</returns>
        Task<IEnumerable<PrescriptionEntity>> GetPatientPrescriptions(int id);

        /// <summary>
        /// Removes a prescription from the database
        /// </summary>
        /// <param name="prescriptionId">Id for the prescription to be removed</param>
        /// <returns>Result: Success if successful else failure</returns>
        Task<ResultEnums.Result> RemovePrescriptionAsync(int prescriptionId);
    }
}