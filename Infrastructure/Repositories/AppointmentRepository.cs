using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD

=======
>>>>>>> 8cda0a89311821e70839b7bed64fc02add54b367

namespace Infrastructure.Repositories
{
    public class AppointmentRepository(CodeFirstDbContext context) : BaseRepository<AppointmentEntity>(context)
    {
        private readonly CodeFirstDbContext _context = context;

        public override async Task<IEnumerable<AppointmentEntity>> GetAllAsync()
        {
            var appointments = await _context.Appointments
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .ToListAsync();
            return appointments;
        }
    }
}
