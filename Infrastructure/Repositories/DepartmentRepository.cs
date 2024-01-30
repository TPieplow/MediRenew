using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class DepartmentRepository(CodeFirstDbContext context) : BaseRepository<DepartmentEntity>(context), IDepartmentRepository
{
    private readonly CodeFirstDbContext _context = context;


}
