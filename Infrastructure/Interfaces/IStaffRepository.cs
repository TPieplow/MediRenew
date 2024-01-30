using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IStaffRepository : IBaseRepository<StaffEntity>
    {
        Task<bool> DeleteAsync(Expression<Func<StaffEntity, bool>> predicate);
        Task<IEnumerable<StaffEntity>> GetAllStaffMembersIncludeDepartAsync();
        Task<DepartmentEntity?> GetDepartmentByIdAsync(int id);
        Task<StaffEntity> GetOneAsync(Expression<Func<StaffEntity, bool>> predicate);
    }
}