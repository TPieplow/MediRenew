using Infrastructure.HospitalEntities;

namespace Infrastructure.Interfaces
{
    public interface IAppointmentRepository : IBaseRepository<AppointmentEntity>
    {
        new Task<IEnumerable<AppointmentEntity>> GetAllAsync();
    }
}