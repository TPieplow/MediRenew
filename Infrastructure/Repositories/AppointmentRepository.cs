﻿using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository(CodeFirstDbContext context) : BaseRepository<AppointmentEntity>(context), IAuthentcationRepository
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
