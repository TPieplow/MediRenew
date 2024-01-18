using Infrastructure.DataSeed;
using Infrastructure.HospitalEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class CodeFirstDbContext
{

        modelBuilder.Entity<AppointmentEntity>().HasKey(
            nameof(AppointmentEntity.PatientId), nameof(AppointmentEntity.DoctorId));

        //modelBuilder.Entity<PrescriptionEntity>()
        //    .Property(p => p.Cost)
        //    .HasColumnType("decimal(10, 2)");


        DataSeederHospital.HospitalSeeder(modelBuilder);


        //lägg till mer konfig om det behövs.
    }
}