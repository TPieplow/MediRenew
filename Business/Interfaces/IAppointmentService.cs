using Business.DTOs;
using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IAppointmentService
    {
        /// <summary>
        /// Service to add appointment and mapping entity. Checks if patient has appointment if not sends to repo to create else returns Result enum failure.
        /// </summary>
        /// <param name="newAppointment">Appointment model to be created</param>
        /// <returns>Result enum(failure, success etc)</returns>
        Task<ResultEnums.Result> AddApointment(AppointmentDTO newAppointment);

        /// <summary>
        /// Uses appointmentRepo to fetch a list of appointments then maps the models in the list
        /// </summary>
        /// <returns>A list of appointments</returns>
        Task<IEnumerable<AppointmentDTO>> GetAllAppointments();

        /// <summary>
        /// Fetches one appointment with patient-id
        /// </summary>
        /// <param name="id">The patient-id(FK in Appointments-table)</param>
        /// <returns>Appointment found with patient-id else null</returns>
        Task<AppointmentDTO> GetOneAppointment(int id);

        /// <summary>
        /// Finds appointment with patient-id then uses repo to delete
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>Result enum (failure, success etc)</returns>
        Task<ResultEnums.Result> RemoveAppointmentAsync(int patientId);

        /// <summary>
        /// Uses patient-id to find appointment. Can change date and comment, sends to repo for update if appointment is found
        /// </summary>
        /// <param name="appointmentToUpdate">Appointment to update through patient-id</param>
        /// <returns>Result enum (failure, success etc)</returns>
        Task<ResultEnums.Result> UpdateAppointment(AppointmentDTO appointmentToUpdate);
    }
}