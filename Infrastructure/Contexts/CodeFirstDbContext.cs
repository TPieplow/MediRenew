using Infrastructure.DataSeed;
using Infrastructure.HospitalEntities;
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

        modelBuilder.Entity<DepartmentEntity>()
            .HasOne(d => d.Hospital)
            .WithMany(h => h.Departments)
            .HasForeignKey(d => d.HospitalId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DoctorEntity>()
            .HasOne(d => d.Department)
            .WithMany(c => c.Doctors)
            .HasForeignKey(f => f.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StaffEntity>()
            .HasOne(d => d.Department)
            .WithMany(s => s.Staff)
            .HasForeignKey(f => f.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DepartmentEntity>()
            .HasIndex(d => d.DepartmentName)
            .IsUnique();

        DataSeederHospital.HospitalSeeder(modelBuilder);
    }
}