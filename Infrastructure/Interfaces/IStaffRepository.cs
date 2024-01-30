using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IStaffRepository : IBaseRepository<StaffEntity>
    {
        /// <summary>
        /// Deletes a staff member from the database
        /// </summary>
        /// <param name="predicate">The predicate to find the staff member to delete.</param>
        /// <returns>True or false.</returns>
        new Task<bool> DeleteAsync(Expression<Func<StaffEntity, bool>> predicate);

        /// <summary>
        /// Gets all staff members including Departments.
        /// </summary>
        /// <returns>Returns a list of staff members and departments</returns>
        Task<IEnumerable<StaffEntity>> GetAllStaffMembersIncludeDepartAsync();

        /// <summary>
        /// Gets all departments by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A department entity</returns>
        Task<DepartmentEntity?> GetDepartmentByIdAsync(int id);

        /// <summary>
        /// Gets one staffmember from the database
        /// </summary>
        /// <param name="predicate">The predicate to specify the chosen staff member.</param>
        /// <returns></returns>
        new Task<StaffEntity> GetOneAsync(Expression<Func<StaffEntity, bool>> predicate);
    }
}