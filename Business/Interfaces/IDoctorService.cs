using Business.DTOs;
using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IDoctorService
    {
        /// <summary>
        /// Checks if doctor exists if not maps out model and uses repo to add doctor.
        /// </summary>
        /// <param name="newDoctor">Entity to be added</param>
        /// <returns>Result enum: Success if succesful else failure</returns>
        Task<ResultEnums.Result> AddDoctorAsync(DoctorDTO newDoctor);

        /// <summary>
        /// Gets a list of doctors and maps every entity
        /// </summary>
        /// <returns>Mapped list of doctors</returns>
        Task<IEnumerable<DoctorDTO>> GetAllDoctors();

        /// <summary>
        /// Gets one doctor by using doctor-id
        /// </summary>
        /// <param name="doctorId">Id for the doctor</param>
        /// <returns>If found, doctor-model. Else returns null</returns>
        Task<DoctorDTO> GetOneDoctorAsync(int doctorId);

        /// <summary>
        /// Removes doctor through doctor-id
        /// </summary>
        /// <param name="doctorId">id of the doctor to be removed</param>
        /// <returns>Result enum: Success if successfuly removed else failure</returns>
        Task<ResultEnums.Result> RemoveDoctorAsync(int doctorId);

        /// <summary>
        /// Finds a doctor to update through id remaps with entered updated info
        /// </summary>
        /// <param name="updatedDoctor">doctor-model to be updated</param>
        /// <returns>Result enum: Success if successfuly removed else failure</returns>
        Task<ResultEnums.Result> UpdateDoctorAsync(DoctorDTO updatedDoctor);
    }
}