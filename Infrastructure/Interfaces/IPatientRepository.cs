using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IPatientRepository : IBaseRepository<PatientEntity>
    {
        Task<IEnumerable<PatientEntity>> GetAllAsync();
        PatientEntity GetByIdIncludePrescription(int id);
        Task<PatientEntity> GetOneAsync(Expression<Func<PatientEntity, bool>> predicate);

    }
}