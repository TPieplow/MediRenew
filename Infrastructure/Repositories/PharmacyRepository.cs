using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PharmacyRepository(CodeFirstDbContext context) : BaseRepository<PharmacyEntity>(context)
    {
        private readonly CodeFirstDbContext _context = context;

        public override async Task<IEnumerable<PharmacyEntity>> GetAllAsync()
        {
            var pharmacies = await _context.Pharmacys
                .ToListAsync();
            return pharmacies;
        }
    }
}
