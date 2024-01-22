using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


public class PrescriptionRepository : BaseRepository<PrescriptionEntity>
{
    private readonly CodeFirstDbContext _context;
    public PrescriptionRepository(CodeFirstDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<PrescriptionEntity>> GetAllAsync()
    {
        var foo = await _context.Prescriptions
            .Include(x => x.Pharmacy)
            .Include(x => x.Patient)
            .ToListAsync();
        return foo;
    }

    public async Task<IEnumerable<PrescriptionEntity>> GetAllForPatient(int id)
    {
        var foo = await _context.Prescriptions
            .Include(x => x.Pharmacy)
            .Include(x => x.Patient)
            .Where(x => x.PatientId == id)
            .ToListAsync();

        return foo;
    }
}