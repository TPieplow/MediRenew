using Infrastructure.HospitalEntities;

namespace Infrastructure.Interfaces
{
    public interface IAuthentcationRepository : IBaseRepository<AppointmentEntity>
    {
        new Task<IEnumerable<AppointmentEntity>> GetAllAsync();
    }
}