using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<PatientEntity>> GetAllAsync();
        PatientEntity GetByIdIncludePrescription(int id);
        Task<PatientEntity> GetOneAsync(Expression<Func<PatientEntity, bool>> predicate);
        bool RemovePatient(int id);
        Task<PatientEntity> UpdateAsync(PatientEntity entity);
    }
}