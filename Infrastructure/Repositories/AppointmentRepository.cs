using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository(CodeFirstDbContext context) : BaseRepository<AppointmentEntity>(context)
    {
    }
}
