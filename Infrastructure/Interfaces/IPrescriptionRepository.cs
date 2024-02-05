using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;

public interface IPrescriptionRepository : IBaseRepository<PrescriptionEntity>
{
    /// <summary>
    /// Retrieves all prescriptions including patient, doctor & pharmacy relations
    /// </summary>
    /// <returns>IEnum of prescriptions</returns>
    new Task<IEnumerable<PrescriptionEntity>> GetAllAsync();

    /// <summary>
    /// Gets all prescriptions for one specific patient
    /// </summary>
    /// <param name="id">Patient-Id</param>
    /// <returns>IEnum of prescriptions for one patient</returns>
    Task<IEnumerable<PrescriptionEntity>> GetAllForPatient(int id);
}