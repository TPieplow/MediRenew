using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class CodeFirstDbContext(DbContextOptions<CodeFirstDbContext> options) : DbContext(options)
{
    public virtual DbSet<AppointmentEntity> Appointments { get; set; }
    public virtual DbSet<AuthenticationEntity> Authentications { get; set; }
    public virtual DbSet<EmployeeEntity> Employees { get; set; }
    public virtual DbSet<PatientEntity> Patients { get; set; }
    public virtual DbSet<PersonProfileEntity> PersonProfiles { get; set; }
    public virtual DbSet<PrescriptionEntity> Prescriptions { get; set; }
    public virtual DbSet<RoleEntity> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>()
            .HasIndex(x => x.Role)
            .IsUnique();

        modelBuilder.Entity<PersonProfileEntity>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<PatientEntity>()
            .HasIndex(x => x.SocialSecurityNo)
            .IsUnique();

        modelBuilder.Entity<AuthenticationEntity>()
            .HasIndex(x => x.UserName)
            .IsUnique();
    }
}