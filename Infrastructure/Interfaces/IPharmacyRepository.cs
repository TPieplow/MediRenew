using Infrastructure.HospitalEntities;

namespace Infrastructure.Interfaces
{
    public interface IPharmacyRepository : IBaseRepository<PharmacyEntity>
    {
        new Task<IEnumerable<PharmacyEntity>> GetAllAsync();
    }
}