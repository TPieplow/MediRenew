using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IPatientRepository : IBaseRepository<PatientEntity>
    {
        /// <summary>
        /// Retireves all patients stored in the database.
        /// </summary>
        /// <returns>A IEnumerable list of all patients stored in the database.</returns>
        new Task<IEnumerable<PatientEntity>> GetAllAsync();

        /// <summary>
        /// Retrieving the patient with the belonging descriptions.
        /// </summary>
        /// <param name="id">The id to find the patient and prescirptions.</param>
        /// <returns>The entity if found, else null.</returns>
        PatientEntity GetByIdIncludePrescription(int id);


        new Task<PatientEntity> GetOneAsync(Expression<Func<PatientEntity, bool>> predicate);
    }
}