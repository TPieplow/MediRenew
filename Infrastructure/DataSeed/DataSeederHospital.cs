
using Infrastructure.HospitalEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataSeed
{
    public class DataSeederHospital
    {
        public static void HospitalSeeder(ModelBuilder modelBuilder)
        {
            SeedHospital(modelBuilder);
            SeedDepartment(modelBuilder);
            SeedDoctor(modelBuilder);
            SeedStaff(modelBuilder);
            SeedPatient(modelBuilder);
            SeedPharmacy(modelBuilder);
            SeedAppointment(modelBuilder);
            SeedRoom(modelBuilder);
            SeedPrescription(modelBuilder);
            SeedInvoice(modelBuilder);
        }

        public static void SeedHospital(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HospitalEntity>().HasData(

                new HospitalEntity()
                {
                    Id = -1,
                    HospitalName = "Hospital1",
                    HospitalAddress = "testroad 25",
                    HospitalPhoneNumber = "0707-070707",
                    HospitalPostalCode = "12345",
                });
        }

        public static void SeedDepartment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentEntity>().HasData(
                new DepartmentEntity()
                {
                    Id = -1, 
                    DepartmentName = "Surgery",
                },
                new DepartmentEntity()
                {
                    Id = 2,
                    DepartmentName = "Cardiology",
                },
                new DepartmentEntity()
                {
                    Id = 3,
                    DepartmentName = "Neurology",
                },
                new DepartmentEntity()
                {
                    Id = 4,
                    DepartmentName = "Emergeny Department (ER)",
                });
        }

        public static void SeedDoctor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorEntity>().HasData(
                new DoctorEntity()
                {
                    Id = 1,
                    FirstName = "Hans",
                    LastName = "Mattin-Lassei",
                    PhoneNumber = "1234567890",
                    DepartmentId = 1,
                },
                new DoctorEntity()
                {
                    Id = 2,
                    FirstName = "Ted",
                    LastName = "Pieplow",
                    PhoneNumber = "0987654321",
                    DepartmentId = 2,
                },
                new DoctorEntity()
                {
                    Id = 3,
                    FirstName = "Hassan",
                    LastName = "Al-Heidari",
                    PhoneNumber = "1234098765",
                    DepartmentId = 3,
                });
        }

        public static void SeedStaff(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StaffEntity>().HasData(
                new StaffEntity()
                {
                    Id =1,
                    FirstName = "SexyNurse1",
                    LastName = "test",
                    RoleName = "Nurse",
                    PhoneNumber = "0124356879",
                    DepartmentId = 1,
                },
                new StaffEntity()
                {
                    Id = 2,
                    FirstName = "SexyNurse2",
                    LastName= "test",
                    RoleName = "Cleaner",
                    PhoneNumber = "009988774",
                    DepartmentId = 2,
                },
                new StaffEntity()
                {
                    Id = 3,
                    FirstName = "SexyNurse3",
                    LastName = "test",
                    RoleName = "Receptionist",
                    PhoneNumber = "5432167890",
                    DepartmentId = 3,
                });
        }

        public static void SeedPatient(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientEntity>().HasData(
                new PatientEntity()
                {
                    Id = 1,
                    FirstName = "testPatient1",
                    LastName = "testLastName1",
                    Address = "testroad1",
                    City = "testcity1",
                    PostalCode = "12345",
                    PhoneNumber = "123456789",
                    Email = "test1@test1.com",
                },
                new PatientEntity()
                {
                    Id = 2,
                    FirstName = "testPatient2",
                    LastName = "testLastName2",
                    Address = "testroad2",
                    City = "testcity2",
                    PostalCode = "12375",
                    PhoneNumber = "183456789",
                    Email = "test2@test2.com",
                },
                new PatientEntity()
                {
                    Id = 3,
                    FirstName = "testPatient3",
                    LastName = "testLastName3",
                    Address = "testroad3",
                    City = "testcity3",
                    PostalCode = "19375",
                    PhoneNumber = "183856789",
                    Email = "test3@test3.com",
                });
        }

        public static void SeedPharmacy(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PharmacyEntity>().HasData(
                new PharmacyEntity()
                {
                    Id = 1,
                    MedicationName = "Ibuprofen",
                },
                new PharmacyEntity()
                {
                    Id = 2,
                    MedicationName = "Paracetamol",
                },
                new PharmacyEntity()
                {
                    Id = 3,
                    MedicationName = "Beta-adrenergic blockers",
                });
        }

        public static void SeedAppointment(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<AppointmentEntity>().HasData(
                new AppointmentEntity()
                {
                    PatientId = 1,
                    DoctorId = 1,
                    Date = DateTime.Now,
                    Comments = "Headfraction"
                },
                
                new AppointmentEntity()
                {
                    PatientId = 2,
                    DoctorId = 2,
                    Date = DateTime.Now,
                    Comments = "Pungbråck"
                },
                
                new AppointmentEntity()
                {
                    PatientId = 3,
                    DoctorId = 3,
                    Date = DateTime.Now,
                    Comments = "Headfraction"
                });
        }

        public static void SeedRoom(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomEntity>().HasData(
                new RoomEntity()
                {
                    Id = 1,
                    IsOccupied = true,
                    RoomNumber = 1,
                    PatientId = 1,
                    StaffId = 1
                },
                new RoomEntity()
                {
                    Id = 2,
                    IsOccupied = false,
                    RoomNumber = 2,
                    PatientId = null,
                    StaffId = null
                },
                new RoomEntity()
                {
                    Id = 3,
                    IsOccupied = true,
                    RoomNumber = 3,
                    PatientId = 3,
                    StaffId = 3
                });
        }

        public static void SeedPrescription(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrescriptionEntity>().HasData(
                new PrescriptionEntity()
                {
                    Id = 1,
                    Date = DateTime.Now,
                    Cost = 10.0m,
                    Dosage = "Every 4 hour",
                    PatientId = 1,
                    DoctorId = 1,
                    PharmacyId = 1,
                },
                new PrescriptionEntity()
                {
                    Id = 2,
                    Date = DateTime.Now,
                    Cost = 5.0m,
                    Dosage = "Every 4 hour",
                    PatientId = 2,
                    DoctorId = 2,
                    PharmacyId = 2,
                },
                new PrescriptionEntity()
                {
                    Id = 3,
                    Date = DateTime.Now,
                    Cost = 50.0m,
                    Dosage = "Twice a day, morning and before bed",
                    PatientId = 3,
                    DoctorId = 3,
                    PharmacyId = 3,
                }) ;
        }

        public static void SeedInvoice(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceEntity>().HasData(
                new InvoiceEntity()
                {
                    Id = 1,
                    Description = "Medicin (Ibuprofen) + Doctor's Appointment",
                    Cost = 15m,
                    TotalCost = 30m,
                    PatientId = 1,
                    PharmacyId = 1,
                });
        }
    }
}
