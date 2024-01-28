using Business.DTOs;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;

namespace Business.Services;

public class PharmacyService(PharmacyRepository repository)
{
    private readonly PharmacyRepository _repository = repository;

    public async Task<IEnumerable<PharmacyDTO>> ViewAllPharmacy()
    {
        try
        {
            var result = (await _repository.GetAllAsync()).ToList();
            if (result.Count < 0)
            {
                return new List<PharmacyDTO>();
            }

            return result.Select(x => new PharmacyDTO
            {
                Id = x.Id,
                MedicationName = x.MedicationName,
            });
        }
        catch (Exception ex)
        { Console.WriteLine($"ERROR: {ex.Message}"); }
        return null!;
    }
}
