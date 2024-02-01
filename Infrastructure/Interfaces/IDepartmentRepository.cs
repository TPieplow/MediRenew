using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IDepartmentRepository : IBaseRepository<DepartmentEntity>
    {
        /// <summary>
        /// Gets all departments from the database.
        /// </summary>
        /// <returns>A list of departments</returns>
        new Task<IEnumerable<DepartmentEntity>> GetAllAsync();
        /// <summary>
        /// Gets one department
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        new Task<DepartmentEntity> GetOneAsync(Expression<Func<DepartmentEntity, bool>> predicate);
        /// <summary>
        /// Updates the departments
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns></returns>
        new Task<DepartmentEntity> UpdateAsync(DepartmentEntity entity);
    }
}