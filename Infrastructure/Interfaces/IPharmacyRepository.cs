using Infrastructure.HospitalEntities;

namespace Infrastructure.Interfaces
{
    public interface IPharmacyRepository : IBaseRepository<PharmacyEntity>
    {
        /// <summary>
        /// Gets a list of all medication in pharmacy
        /// </summary>
        /// <returns>IEnum of medications in pharmacy</returns>
        new Task<IEnumerable<PharmacyEntity>> GetAllAsync();
    }
}