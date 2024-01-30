using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentEntity>> GetAllAsync();
        Task<DepartmentEntity> GetOneAsync(Expression<Func<DepartmentEntity, bool>> predicate);
        Task<DepartmentEntity> UpdateAsync(DepartmentEntity entity);
    }
}