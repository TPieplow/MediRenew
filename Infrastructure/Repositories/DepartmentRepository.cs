using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class DepartmentRepository : Repository<DepartmentEntity>
{
    private readonly CodeFirstDbContext _context;
    public DepartmentRepository(CodeFirstDbContext context) : base(context)
    {
        _context = context;
    }

    public override Task<DepartmentEntity> Create(DepartmentEntity entity)
    {
        return base.Create(entity);
    }
    public override Task<bool> Delete(Expression<Func<DepartmentEntity, bool>> predicate)
    {
        return base.Delete(predicate);
    }

    public override Task<IEnumerable<DepartmentEntity>> GetAll()
    {
        return base.GetAll();
    }

    public override Task<DepartmentEntity> GetOne(Expression<Func<DepartmentEntity, bool>> predicate)
    {
        return base.GetOne(predicate);
    }

    public override Task<DepartmentEntity> Update(DepartmentEntity entity)
    {
        return base.Update(entity);
    }
}
