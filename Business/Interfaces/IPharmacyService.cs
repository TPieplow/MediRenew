using Business.DTOs;

namespace Business.Interfaces
{
    public interface IPharmacyService
    {
        Task<IEnumerable<PharmacyDTO>> ViewAllPharmacy();
    }
}