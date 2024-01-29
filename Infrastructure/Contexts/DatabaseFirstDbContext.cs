
using Infrastructure.DatabaseFirstEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public partial class DatabaseFirstDbContext(DbContextOptions<DatabaseFirstDbContext> options) : DbContext(options)
{
    public virtual DbSet<AuthenticationEntity> Authentications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthenticationEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authenti__3214EC0777D9D78F");

            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
