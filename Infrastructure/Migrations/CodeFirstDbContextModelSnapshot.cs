﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(CodeFirstDbContext))]
    partial class CodeFirstDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.HospitalEntities.AppointmentEntity", b =>
                {
                    b.Property<int>("PatientId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("PatientId", "DoctorId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            PatientId = 1,
                            DoctorId = 1,
                            Comments = "Headfraction",
                            Date = new DateTime(2024, 1, 18, 13, 18, 54, 708, DateTimeKind.Local).AddTicks(8114)
                        },
                        new
                        {
                            PatientId = 2,
                            DoctorId = 2,
                            Comments = "Pungbråck",
                            Date = new DateTime(2024, 1, 18, 13, 18, 54, 708, DateTimeKind.Local).AddTicks(8175)
                        },
                        new
                        {
                            PatientId = 3,
                            DoctorId = 3,
                            Comments = "Headfraction",
                            Date = new DateTime(2024, 1, 18, 13, 18, 54, 708, DateTimeKind.Local).AddTicks(8177)
                        });
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.DepartmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HospitalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

<<<<<<< HEAD
<<<<<<< HEAD
                    b.HasIndex("PersonId");

                    b.ToTable("Appointments", (string)null);
=======
                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            DepartmentName = "Surgery",
                            HospitalId = 0
                        },
                        new
                        {
                            Id = 2,
                            DepartmentName = "Cardiology",
                            HospitalId = 0
                        },
                        new
                        {
                            Id = 3,
                            DepartmentName = "Neurology",
                            HospitalId = 0
                        },
                        new
                        {
                            Id = 4,
                            DepartmentName = "Emergeny Department (ER)",
                            HospitalId = 0
                        });
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.DoctorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepartmentId = 1,
                            FirstName = "Hans",
                            LastName = "Mattin-Lassei",
                            PhoneNumber = "1234567890"
                        },
                        new
                        {
                            Id = 2,
                            DepartmentId = 2,
                            FirstName = "Ted",
                            LastName = "Pieplow",
                            PhoneNumber = "0987654321"
                        },
                        new
                        {
                            Id = 3,
                            DepartmentId = 3,
                            FirstName = "Hassan",
                            LastName = "Al-Heidari",
                            PhoneNumber = "1234098765"
                        });
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.HospitalEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("HospitalAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("HospitalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("HospitalPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("HospitalPostalCode")
                        .IsRequired()
                        .HasColumnType("varchar(7)");

                    b.HasKey("Id");

                    b.ToTable("Hospitals");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            HospitalAddress = "testroad 25",
                            HospitalName = "Hospital1",
                            HospitalPhoneNumber = "0707-070707",
                            HospitalPostalCode = "12345"
                        });
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.InvoiceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

<<<<<<< HEAD
                    b.Property<decimal>("TotalCost")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Invoices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cost = 15m,
                            Description = "Medicin (Ibuprofen) + Doctor's Appointment",
                            PatientId = 1,
                            PharmacyId = 1,
                            TotalCost = 30m
                        });
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.PatientEntity", b =>
>>>>>>> d8759a46a99d994baf59f861508d361a2b0cbecf
=======
                    b.ToTable("Appointments");
>>>>>>> 27ab09b0e8d08d7ca2ba60edccb1bd6030218a2e
                });

            modelBuilder.Entity("Infrastructure.Entities.AuthenticationEntity", b =>
                {
                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("PersonId");

                    b.HasIndex("UserName")
                        .IsUnique();

<<<<<<< HEAD
                    b.ToTable("Authentications", (string)null);
=======
                    b.ToTable("Authentications");

                    b.HasData(
                        new
                        {
                            PersonId = 3,
                            Password = "asdtasd",
                            UserName = "tastast"
                        },
                        new
                        {
                            PersonId = 4,
                            Password = "asdtasdt",
                            UserName = "tasdt"
                        });
>>>>>>> 27ab09b0e8d08d7ca2ba60edccb1bd6030218a2e
                });

            modelBuilder.Entity("Infrastructure.Entities.PersonEntity", b =>
>>>>>>> 27ab09b0e8d08d7ca2ba60edccb1bd6030218a2e
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

<<<<<<< HEAD
                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");
=======
                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

<<<<<<< HEAD
                    b.HasIndex("RoleId");

                    b.ToTable("Persons", (string)null);
=======
                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            Id = -2,
                            Active = true,
                            Created = new DateTime(2024, 1, 17, 15, 45, 1, 587, DateTimeKind.Utc).AddTicks(1981)
                        },
                        new
                        {
                            Id = -3,
                            Active = true,
                            Created = new DateTime(2024, 1, 17, 15, 45, 1, 587, DateTimeKind.Utc).AddTicks(1986)
                        });
>>>>>>> 27ab09b0e8d08d7ca2ba60edccb1bd6030218a2e
                });

            modelBuilder.Entity("Infrastructure.Entities.PersonProfileEntity", b =>
                {
                    b.Property<int>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProfileId"));

                    b.Property<bool>("ActivePrescriptions")
                        .HasColumnType("bit");
>>>>>>> 27ab09b0e8d08d7ca2ba60edccb1bd6030218a2e

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("varchar(7)");

<<<<<<< HEAD
                    b.HasKey("Id");
=======
                    b.Property<int>("RoleId")
                        .HasColumnType("int");
>>>>>>> 27ab09b0e8d08d7ca2ba60edccb1bd6030218a2e

                    b.ToTable("Patients");

<<<<<<< HEAD
                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "testroad1",
                            City = "testcity1",
                            Email = "test1@test1.com",
                            FirstName = "testPatient1",
                            LastName = "testLastName1",
                            PhoneNumber = "123456789",
                            PostalCode = "12345"
                        },
                        new
                        {
                            Id = 2,
                            Address = "testroad2",
                            City = "testcity2",
                            Email = "test2@test2.com",
                            FirstName = "testPatient2",
                            LastName = "testLastName2",
                            PhoneNumber = "183456789",
                            PostalCode = "12375"
                        },
                        new
                        {
                            Id = 3,
                            Address = "testroad3",
                            City = "testcity3",
                            Email = "test3@test3.com",
                            FirstName = "testPatient3",
                            LastName = "testLastName3",
                            PhoneNumber = "183856789",
                            PostalCode = "19375"
=======
                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProfileId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("SocialSecurityNo")
                        .IsUnique();

<<<<<<< HEAD
                    b.ToTable("PersonProfiles", (string)null);
=======
                    b.ToTable("PersonProfiles");

                    b.HasData(
                        new
                        {
                            ProfileId = 1,
                            ActivePrescriptions = true,
                            City = "Toarp",
                            Country = "Polski",
                            Email = "Hassan.A@gmail.com",
                            FirstName = "Hassan",
                            LastName = "Al-Heidari",
                            LastVisit = new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PersonId = -2,
                            PhoneNumber = "1234567890",
                            PostalCode = "24550",
                            RoleId = 3,
                            SocialSecurityNo = "987654321",
                            StreetName = "Toarpsvägen"
                        },
                        new
                        {
                            ProfileId = 2,
                            ActivePrescriptions = true,
                            City = "Blentarp",
                            Country = "Deutsch",
                            Email = "Ted.P@gmail.com",
                            FirstName = "Ted",
                            LastName = "Pieplow",
                            LastVisit = new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PersonId = -3,
                            PhoneNumber = "0022334455",
                            PostalCode = "27450",
                            RoleId = 2,
                            SocialSecurityNo = "880330",
                            StreetName = "Vildsvinsvägen"
>>>>>>> 27ab09b0e8d08d7ca2ba60edccb1bd6030218a2e
                        });
>>>>>>> 27ab09b0e8d08d7ca2ba60edccb1bd6030218a2e
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.PharmacyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Dosage")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("MedicationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

<<<<<<< HEAD
                    b.ToTable("Prescriptions", (string)null);
=======
                    b.ToTable("Pharmacys");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DoctorId = 1,
                            Dosage = "1g",
                            MedicationName = "Ibuprofen"
                        },
                        new
                        {
                            Id = 2,
                            DoctorId = 2,
                            Dosage = "500mg",
                            MedicationName = "Paracetamol"
                        },
                        new
                        {
                            Id = 3,
                            DoctorId = 3,
                            Dosage = "400mg",
                            MedicationName = "Beta-adrenergic blockers"
                        });
>>>>>>> d8759a46a99d994baf59f861508d361a2b0cbecf
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.PrescriptionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MedicationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Prescriptions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cost = 10.0m,
                            Date = new DateTime(2024, 1, 18, 13, 18, 54, 708, DateTimeKind.Local).AddTicks(8226),
                            DoctorId = 1,
                            Instructions = "Every 4 hour",
                            MedicationName = "Ibuprofen",
                            PatientId = 1,
                            PharmacyId = 1
                        },
                        new
                        {
                            Id = 2,
                            Cost = 5.0m,
                            Date = new DateTime(2024, 1, 18, 13, 18, 54, 708, DateTimeKind.Local).AddTicks(8239),
                            DoctorId = 2,
                            Instructions = "Every 4 hour",
                            MedicationName = "Paracetamol",
                            PatientId = 2,
                            PharmacyId = 2
                        },
                        new
                        {
                            Id = 3,
                            Cost = 50.0m,
                            Date = new DateTime(2024, 1, 18, 13, 18, 54, 708, DateTimeKind.Local).AddTicks(8241),
                            DoctorId = 3,
                            Instructions = "Twice a day, morning and before bed",
                            MedicationName = "Beta-adrenergic blockers",
                            PatientId = 3,
                            PharmacyId = 3
                        });
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.RoomEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<int?>("StaffId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("StaffId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PatientId = 1,
                            RoomNumber = 1,
                            StaffId = 1
                        },
                        new
                        {
                            Id = 2,
                            PatientId = 2,
                            RoomNumber = 2,
                            StaffId = 2
                        },
                        new
                        {
                            Id = 3,
                            PatientId = 3,
                            RoomNumber = 3,
                            StaffId = 3
                        });
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.StaffEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

<<<<<<< HEAD
<<<<<<< HEAD
                    b.ToTable("Roles", (string)null);
=======
                    b.ToTable("Staff");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepartmentId = 1,
                            FirstName = "SexyNurse1",
                            LastName = "test",
                            PhoneNumber = "0124356879",
                            RoleName = "Nurse"
                        },
                        new
                        {
                            Id = 2,
                            DepartmentId = 2,
                            FirstName = "SexyNurse2",
                            LastName = "test",
                            PhoneNumber = "009988774",
                            RoleName = "Cleaner"
                        },
                        new
                        {
                            Id = 3,
                            DepartmentId = 3,
                            FirstName = "SexyNurse3",
                            LastName = "test",
                            PhoneNumber = "5432167890",
                            RoleName = "Receptionist"
                        });
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.AppointmentEntity", b =>
                {
                    b.HasOne("Infrastructure.HospitalEntities.DoctorEntity", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.HospitalEntities.PatientEntity", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.InvoiceEntity", b =>
                {
                    b.HasOne("Infrastructure.HospitalEntities.PatientEntity", "Patient")
>>>>>>> d8759a46a99d994baf59f861508d361a2b0cbecf
=======
                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Role = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Role = "Doctor"
                        },
                        new
                        {
                            Id = 3,
                            Role = "Patient"
                        });
>>>>>>> 27ab09b0e8d08d7ca2ba60edccb1bd6030218a2e
                });

            modelBuilder.Entity("Infrastructure.Entities.PersonProfileEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.PersonEntity", "Person")
                        .WithOne("PersonProfile")
                        .HasForeignKey("Infrastructure.Entities.PersonProfileEntity", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.RoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Infrastructure.Entities.PrescriptionEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.PersonEntity", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.PersonEntity", "Patient")
>>>>>>> 27ab09b0e8d08d7ca2ba60edccb1bd6030218a2e
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .IsRequired();

                    b.HasOne("Infrastructure.HospitalEntities.PharmacyEntity", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.PharmacyEntity", b =>
                {
                    b.HasOne("Infrastructure.HospitalEntities.DoctorEntity", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId");

                    b.HasOne("Infrastructure.HospitalEntities.PatientEntity", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

<<<<<<< HEAD
            modelBuilder.Entity("Infrastructure.HospitalEntities.PrescriptionEntity", b =>
                {
                    b.HasOne("Infrastructure.HospitalEntities.DoctorEntity", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.HospitalEntities.PatientEntity", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.HospitalEntities.PharmacyEntity", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.RoomEntity", b =>
                {
                    b.HasOne("Infrastructure.HospitalEntities.PatientEntity", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.HasOne("Infrastructure.HospitalEntities.StaffEntity", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId");

                    b.Navigation("Patient");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Infrastructure.HospitalEntities.StaffEntity", b =>
                {
                    b.HasOne("Infrastructure.HospitalEntities.DepartmentEntity", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
=======
            modelBuilder.Entity("Infrastructure.Entities.PersonEntity", b =>
                {
                    b.Navigation("PersonProfile")
                        .IsRequired();
>>>>>>> 27ab09b0e8d08d7ca2ba60edccb1bd6030218a2e
                });
#pragma warning restore 612, 618
        }
    }
}
