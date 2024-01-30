using Business.DTOs;

namespace Business.Interfaces
{
    public interface IPharmacyService
    {
        /// <summary>
        /// Retrieves all pharmacy stored in the database.
        /// </summary>
        /// <returns>Retrieves an IEnumerable containing all Pharmacy stored in the database.</returns>
        Task<IEnumerable<PharmacyDTO>> ViewAllPharmacy();
    }
}