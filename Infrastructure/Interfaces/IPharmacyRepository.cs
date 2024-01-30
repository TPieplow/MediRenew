using Infrastructure.HospitalEntities;

namespace Infrastructure.Interfaces
{
    public interface IPharmacyRepository : IBaseRepository<PharmacyEntity>
    {
        Task<IEnumerable<PharmacyEntity>> GetAllAsync();
    }
}