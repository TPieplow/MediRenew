using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;

public interface IPrescriptionRepository : IBaseRepository<PrescriptionEntity>
{
    Task<IEnumerable<PrescriptionEntity>> GetAllAsync();
    Task<IEnumerable<PrescriptionEntity>> GetAllForPatient(int id);
}