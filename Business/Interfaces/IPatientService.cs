using Business.DTOs;
using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IPatientService
    {
        /// <summary>
        /// Adds a new patient to the database, checking of the patient alerady exist via e-mail.
        /// </summary>
        /// <param name="newPatient">The patient to be added into the database.</param>
        /// <returns>A result indicating success or failure of the CRUD-operation.</returns>
        Task<ResultEnums.Result> AddPatientAsync(PatientDTO newPatient);

        /// <summary>
        /// Retreives a list of all patients stored in the database.
        /// </summary>
        /// <returns>Returns a IEnumerable-list with all patients stored in the database.</returns>
        Task<IEnumerable<PatientDTO>> GetAllPatients();

        /// <summary>
        /// Retreives a patient by its Id.
        /// </summary>
        /// <param name="patientId">The Id to fetch the patient from database.</param>
        /// <returns>A patientDTO containing the retrieved patient.</returns>
        Task<PatientDTO> GetOnePatient(int patientId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>A result indicating success or failure of the CRUD-operation.</returns>
        Task<ResultEnums.Result> RemovePatientAsync(int patientId);

        /// <summary>
        /// Updates a patient using email to locate it in the database.
        /// </summary>
        /// <param name="updatedPatient">The patientDTO to update.</param>
        /// <returns>A result indicating success or failure of the CRUD-operation.</returns>
        Task<ResultEnums.Result> UpdatePatientAsync(PatientDTO updatedPatient);
    }
}