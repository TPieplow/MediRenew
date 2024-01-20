using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class PatientRepository : Repository<PatientEntity>
{
    private readonly CodeFirstDbContext _context;
    public PatientRepository(CodeFirstDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<PatientEntity>> GetAllAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public override async Task<PatientEntity> GetOneAsync(Expression<Func<PatientEntity, bool>> predicate)
    {
        try
        {
            var patient = await _context.Patients
                .Include(patient => patient.Prescriptions)
                .Where(predicate)
                .FirstOrDefaultAsync();

            if (patient != null)
            {
                return patient;
            }
            else
            {
                Debug.WriteLine("Patient not found");
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR: {ex.Message}");
            return null;
        }
    }
}
