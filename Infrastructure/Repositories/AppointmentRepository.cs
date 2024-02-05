using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository(CodeFirstDbContext context) : BaseRepository<AppointmentEntity>(context), IAppointmentRepository
    {
        private readonly CodeFirstDbContext _context = context;

        public override async Task<IEnumerable<AppointmentEntity>> GetAllAsync()
        {
            try
            {
                var appointments = await _context.Appointments
                    .Include(x => x.Patient)
                    .Include(x => x.Doctor)
                    .ToListAsync();
                return appointments;
            }
            catch (Exception ex)
            { Debug.WriteLine($"ERROR : {ex.Message}"); }
            return null!;
        }
    }
}
