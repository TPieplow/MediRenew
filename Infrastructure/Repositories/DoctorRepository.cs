using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

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

    public override async Task<DoctorEntity> GetOneAsync(Expression<Func<DoctorEntity, bool>> predicate)
    {
        try
        {
            var doctor = await _context.Set<DoctorEntity>()
                .Include(x => x.Department)
                .FirstOrDefaultAsync(predicate);

            if (doctor is not null)
            {
                return doctor;
            }
            else
            {
                Debug.WriteLine("Doctor not found");
                return null!;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR: {ex.Message}");
            return null!;
        }
    }
}
