using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DoctorRepository(CodeFirstDbContext context) : BaseRepository<DoctorEntity>(context)
{
    private readonly CodeFirstDbContext _context = context;


    public override async Task<IEnumerable<DoctorEntity>> GetAllAsync()
    {
        var doctors = await _context.Doctors
            .Include(x => x.Department)
            .ToListAsync();
        return doctors;
    }
}
