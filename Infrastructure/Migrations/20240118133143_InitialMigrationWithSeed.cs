using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalName = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    HospitalAddress = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    HospitalPhoneNumber = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    HospitalPostalCode = table.Column<string>(type: "varchar(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(7)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => new { x.PatientId, x.DoctorId });
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pharmacys_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pharmacys_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    StaffId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rooms_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Pharmacys_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Pharmacys_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentName", "HospitalId" },
                values: new object[,]
                {
                    { -1, "Surgery", 0 },
                    { 2, "Cardiology", 0 },
                    { 3, "Neurology", 0 },
                    { 4, "Emergeny Department (ER)", 0 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "DepartmentId", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "Hans", "Mattin-Lassei", "1234567890" },
                    { 2, 2, "Ted", "Pieplow", "0987654321" },
                    { 3, 3, "Hassan", "Al-Heidari", "1234098765" }
                });

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "HospitalAddress", "HospitalName", "HospitalPhoneNumber", "HospitalPostalCode" },
                values: new object[] { -1, "testroad 25", "Hospital1", "0707-070707", "12345" });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "City", "Email", "FirstName", "LastName", "PharmacyId", "PhoneNumber", "PostalCode" },
                values: new object[,]
                {
                    { 1, "testroad1", "testcity1", "test1@test1.com", "testPatient1", "testLastName1", null, "123456789", "12345" },
                    { 2, "testroad2", "testcity2", "test2@test2.com", "testPatient2", "testLastName2", null, "183456789", "12375" },
                    { 3, "testroad3", "testcity3", "test3@test3.com", "testPatient3", "testLastName3", null, "183856789", "19375" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "DepartmentId", "FirstName", "LastName", "PhoneNumber", "RoleName" },
                values: new object[,]
                {
                    { 1, 1, "SexyNurse1", "test", "0124356879", "Nurse" },
                    { 2, 2, "SexyNurse2", "test", "009988774", "Cleaner" },
                    { 3, 3, "SexyNurse3", "test", "5432167890", "Receptionist" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "DoctorId", "PatientId", "Comments", "Date" },
                values: new object[,]
                {
                    { 1, 1, "Headfraction", new DateTime(2024, 1, 18, 14, 31, 42, 790, DateTimeKind.Local).AddTicks(5562) },
                    { 2, 2, "Pungbråck", new DateTime(2024, 1, 18, 14, 31, 42, 790, DateTimeKind.Local).AddTicks(5619) },
                    { 3, 3, "Headfraction", new DateTime(2024, 1, 18, 14, 31, 42, 790, DateTimeKind.Local).AddTicks(5621) }
                });

            migrationBuilder.InsertData(
                table: "Pharmacys",
                columns: new[] { "Id", "DoctorId", "Dosage", "MedicationName", "PatientId" },
                values: new object[,]
                {
                    { 1, 1, "1g", "Ibuprofen", null },
                    { 2, 2, "500mg", "Paracetamol", null },
                    { 3, 3, "400mg", "Beta-adrenergic blockers", null }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "PatientId", "RoomNumber", "StaffId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 2, 2 },
                    { 3, 3, 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "Cost", "Description", "PatientId", "PharmacyId", "TotalCost" },
                values: new object[] { 1, 15m, "Medicin (Ibuprofen) + Doctor's Appointment", 1, 1, 30m });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "Id", "Cost", "Date", "DoctorId", "Instructions", "MedicationName", "PatientId", "PharmacyId" },
                values: new object[,]
                {
                    { 1, 10.0m, new DateTime(2024, 1, 18, 14, 31, 42, 790, DateTimeKind.Local).AddTicks(5698), 1, "Every 4 hour", "Ibuprofen", 1, 1 },
                    { 2, 5.0m, new DateTime(2024, 1, 18, 14, 31, 42, 790, DateTimeKind.Local).AddTicks(5705), 2, "Every 4 hour", "Paracetamol", 2, 2 },
                    { 3, 50.0m, new DateTime(2024, 1, 18, 14, 31, 42, 790, DateTimeKind.Local).AddTicks(5707), 3, "Twice a day, morning and before bed", "Beta-adrenergic blockers", 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PatientId",
                table: "Invoices",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PharmacyId",
                table: "Invoices",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacys_DoctorId",
                table: "Pharmacys",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacys_PatientId",
                table: "Pharmacys",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PharmacyId",
                table: "Prescriptions",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PatientId",
                table: "Rooms",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_StaffId",
                table: "Rooms",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Pharmacys");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
