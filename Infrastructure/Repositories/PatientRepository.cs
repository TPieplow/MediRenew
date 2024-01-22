using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class PatientRepository : BaseRepository<PatientEntity>, IPatientRepository
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
        return _context.Patients.Include(x => x.Prescriptions)
            .FirstOrDefault(x => x.Id == id)!;
    }

    public bool RemovePatient(int id)
    {
        var patient = _context.Patients.Find(id);
        if (patient is { })
        {
            _context.Patients.Remove(patient);
            return true;
        }

        return false;
    }

    public override Task<PatientEntity> UpdateAsync(PatientEntity entity)
    {
        return base.UpdateAsync(entity);
    }
}
