using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class PatientRepository(CodeFirstDbContext context) : BaseRepository<PatientEntity>(context), IPatientRepository
{
    private readonly CodeFirstDbContext _context = context;

    public override async Task<IEnumerable<PatientEntity>> GetAllAsync()
    {
        try
        {
        return await _context.Patients.ToListAsync();

        } catch (Exception ex)
        {
            Debug.WriteLine($"ERROR: {ex.Message}");
            return null!;
        }
    }

    public override async Task<PatientEntity> GetOneAsync(Expression<Func<PatientEntity, bool>> predicate)
    {
        try
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(predicate);

            if (patient is not null)
            {
                return patient;
            }
            else
            {
                Debug.WriteLine("Patient not found");
                return null!;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR: {ex.Message}");
            return null!;
        }
    }

    public PatientEntity GetByIdIncludePrescription(int id)
    {
        try
        {
            return _context.Patients.Include(x => x.Prescriptions)
                .FirstOrDefault(x => x.Id == id)!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR: {ex.Message}");
            return null!;
        }
    }
}
