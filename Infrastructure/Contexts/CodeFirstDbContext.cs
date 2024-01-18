using global::Infrastructure.DataSeed;
using global::Infrastructure.HospitalEntities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Contexts;

public class CodeFirstDbContext(DbContextOptions<CodeFirstDbContext> options) : DbContext(options)
{
    public virtual DbSet<HospitalEntity> Hospitals { get; set; }
    public virtual DbSet<DepartmentEntity> Departments { get; set; }
    public virtual DbSet<DoctorEntity> Doctors { get; set; }
    public virtual DbSet<StaffEntity> Staff { get; set; }
    public virtual DbSet<PatientEntity> Patients { get; set; }
    public virtual DbSet<PharmacyEntity> Pharmacys { get; set; }
    public virtual DbSet<AppointmentEntity> Appointments { get; set; }
    public virtual DbSet<RoomEntity> Rooms { get; set; }
    public virtual DbSet<PrescriptionEntity> Prescriptions { get; set; }
    public virtual DbSet<InvoiceEntity> Invoices { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
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
