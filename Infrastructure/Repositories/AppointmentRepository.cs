using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using static Infrastructure.Utils.ResultEnums;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository(CodeFirstDbContext context) : BaseRepository<AppointmentEntity>(context)
    {
        public override async Task<IEnumerable<AppointmentEntity>> GetAllAsync()
        {
            var appointments = await context.Appointments
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .ToListAsync();
            return appointments;
        }
    }
}
