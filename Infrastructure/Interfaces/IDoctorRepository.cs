using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IDoctorRepository : IBaseRepository<DoctorEntity>
    {
        Task<IEnumerable<DoctorEntity>> GetAllAsync();
        Task<DoctorEntity> GetOneAsync(Expression<Func<DoctorEntity, bool>> predicate);
    }
}