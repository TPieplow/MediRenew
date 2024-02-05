using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


public class PrescriptionRepository(CodeFirstDbContext context) : BaseRepository<PrescriptionEntity>(context), IPrescriptionRepository
{
    private readonly CodeFirstDbContext _context = context;

    public override async Task<IEnumerable<PrescriptionEntity>> GetAllAsync()
    {
        try
        {
            var prescriptions = await _context.Prescriptions
                .Include(x => x.Pharmacy)
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .ToListAsync();
            return prescriptions;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<PrescriptionEntity>> GetAllForPatient(int id)
    {
        try
        {
            var prescriptions = await _context.Prescriptions
                .Include(x => x.Pharmacy)
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .Where(x => x.PatientId == id)
                .ToListAsync();

            return prescriptions;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR: {ex.Message}");
            return null!;
        }
    }
}

