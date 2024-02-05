using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IDoctorRepository : IBaseRepository<DoctorEntity>
    {
        /// <summary>
        /// Retrieve all doctors from database including Department relation
        /// </summary>
        /// <returns>IEnum of doctors including department</returns>
        new Task<IEnumerable<DoctorEntity>> GetAllAsync();

        /// <summary>
        /// Retrieve one doctor including department by given conditions
        /// </summary>
        /// <param name="predicate">Given conditions</param>
        /// <returns>If found returns doctor, else null</returns>
        new Task<DoctorEntity> GetOneAsync(Expression<Func<DoctorEntity, bool>> predicate);
    }
}