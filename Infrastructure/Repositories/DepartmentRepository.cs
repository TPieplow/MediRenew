using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class DepartmentRepository : BaseRepository<DepartmentEntity>, IDepartmentRepository
{
    private readonly CodeFirstDbContext _context;
    public DepartmentRepository(CodeFirstDbContext context) : base(context)
    {
        _context = context;
    }

    public override Task<IEnumerable<DepartmentEntity>> GetAllAsync()
    {
        return base.GetAllAsync();
    }

    public override Task<DepartmentEntity> GetOneAsync(Expression<Func<DepartmentEntity, bool>> predicate)
    {
        return base.GetOneAsync(predicate);
    }

    public override Task<DepartmentEntity> UpdateAsync(DepartmentEntity entity)
    {
        return base.UpdateAsync(entity);
    }
}
