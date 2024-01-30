using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IDoctorRepository : IBaseRepository<DoctorEntity>
    {
        new Task<IEnumerable<DoctorEntity>> GetAllAsync();
        new Task<DoctorEntity> GetOneAsync(Expression<Func<DoctorEntity, bool>> predicate);
    }
}