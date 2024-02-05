using Infrastructure.HospitalEntities;

namespace Infrastructure.Interfaces
{
    public interface IAppointmentRepository : IBaseRepository<AppointmentEntity>
    {
        /// <summary>
        /// Retrieve all appointments from database including patient/doctor relations
        /// </summary>
        /// <returns>IEnum of appointments</returns>
        new Task<IEnumerable<AppointmentEntity>> GetAllAsync();
    }
}