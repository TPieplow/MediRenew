using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories
{
    public class PharmacyRepository(CodeFirstDbContext context) : BaseRepository<PharmacyEntity>(context), IPharmacyRepository
    {
        private readonly CodeFirstDbContext _context = context;

        public override async Task<IEnumerable<PharmacyEntity>> GetAllAsync()
        {
            try
            {
                var pharmacies = await _context.Pharmacys
                    .ToListAsync();
                return pharmacies;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
                return null!;
            }
        }
    }
}
