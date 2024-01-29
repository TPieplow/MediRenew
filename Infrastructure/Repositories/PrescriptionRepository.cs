using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


public class PrescriptionRepository(CodeFirstDbContext context) : BaseRepository<PrescriptionEntity>(context)
{
    private readonly CodeFirstDbContext _context = context;

    public override async Task<IEnumerable<PrescriptionEntity>> GetAllAsync()
    {
        var prescriptions = await _context.Prescriptions
            .Include(x => x.Pharmacy)
            .Include(x => x.Patient)
            .Include(x => x.Doctor)
            .ToListAsync();
        return prescriptions;
    }

    public async Task<IEnumerable<PrescriptionEntity>> GetAllForPatient(int id)
    {
        var prescriptions = await _context.Prescriptions
            .Include(x => x.Pharmacy)
            .Include(x => x.Patient)
            .Include(x => x.Doctor)
            .Where(x => x.PatientId == id)
            .ToListAsync();

        return prescriptions;
    }
}

