using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataSeed
{
    public class DataSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder);
            SeedPersonProfiles(modelBuilder);
            SeedPersons(modelBuilder);
            SeedAuthentications(modelBuilder);
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleEntity>().HasData(
            new RoleEntity { Id = 1, Role = "Admin" },
            new RoleEntity { Id = 2, Role = "Doctor" },
            new RoleEntity { Id = 3, Role = "Patient" }
            );
        }

        private static void SeedPersonProfiles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonProfileEntity>().HasData(
                new PersonProfileEntity
                {
                    ProfileId = 1,
                    FirstName = "Hassan",
                    LastName = "Al-Heidari",
                    Email = "Hassan.A@gmail.com",
                    PhoneNumber = "1234567890",
                    Country = "Polski",
                    PostalCode = "24550",
                    City = "Toarp",
                    StreetName = "Toarpsvägen",
                    SocialSecurityNo = 987654321,
                    ActivePrescriptions = true,
                    LastVisit = new DateTime(2024, 1, 16),
                    PersonId = 1,
                    RoleEntityId = 3
                },
                new PersonProfileEntity
                {
                    ProfileId = 2,
                    FirstName = "Ted",
                    LastName = "Pieplow",
                    Email = "Ted.P@gmail.com",
                    PhoneNumber = "0022334455",
                    Country = "Deutsch",
                    PostalCode = "27450",
                    City = "Blentarp",
                    StreetName = "Vildsvinsvägen",
                    SocialSecurityNo = 880330,
                    ActivePrescriptions = true,
                    LastVisit = new DateTime(2024, 1, 16),
                    PersonId = 2,
                    RoleEntityId = 2
                });
        }

        private static void SeedPersons(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>().HasData(
                new PersonEntity
                {
                    Id = 1,
                },
                new PersonEntity
                {
                    Id = 2,
                }
                );
        }

        private static void SeedAuthentications(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthenticationEntity>().HasData(
                new AuthenticationEntity
                {

                });
        }
    }
}
