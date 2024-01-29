using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class StaffRepository(CodeFirstDbContext context) : BaseRepository<StaffEntity>(context)
{
    private readonly CodeFirstDbContext _context = context;

    public override Task<bool> DeleteAsync(Expression<Func<StaffEntity, bool>> predicate)
    {
        return base.DeleteAsync(predicate);
    }

    public async Task<IEnumerable<StaffEntity>> GetAllStaffMembersIncludeDepartAsync()
    {
        try
        {
            return await _context.Set<StaffEntity>()
                .Include(x => x.Department)
                .ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine($"ERROR : {ex.Message}"); }
        return Enumerable.Empty<StaffEntity>();
    }

    public async Task<DepartmentEntity?> GetDepartmentByIdAsync(int id)
    {
        try
        {
            return await _context.Departments
                .Include(x => x.Id)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR: {ex.Message}");
            return null;
        }
    }

    public override async Task<StaffEntity> GetOneAsync(Expression<Func<StaffEntity, bool>> predicate)
    {
        try
        {
            var staff = await _context.Set<StaffEntity>()
                .Include(x => x.Department)
                .FirstOrDefaultAsync(predicate);

            if (staff is not null)
            {
                return staff;
            }
            else
            {
                Debug.WriteLine("Staff not found");
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
