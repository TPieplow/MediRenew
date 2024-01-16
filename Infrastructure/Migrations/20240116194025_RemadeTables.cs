using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemadeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Employees_EmployeeId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonProfiles_Employees_EmployeeId",
                table: "PersonProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonProfiles_Patients_PatientId",
                table: "PersonProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patients_PatientId",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_PersonProfiles_EmployeeId",
                table: "PersonProfiles");

            migrationBuilder.DropIndex(
                name: "IX_PersonProfiles_PatientId",
                table: "PersonProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_EmployeeId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "PersonProfiles");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Prescriptions",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_PersonId");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "PersonProfiles",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Authentications",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Appointments",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                newName: "IX_Appointments_PersonId");

            migrationBuilder.AddColumn<bool>(
                name: "ActivePrescriptions",
                table: "PersonProfiles",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastVisit",
                table: "PersonProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialSecurityNo",
                table: "PersonProfiles",
                type: "varchar(13)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonProfiles_SocialSecurityNo",
                table: "PersonProfiles",
                column: "SocialSecurityNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_RoleId",
                table: "Persons",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Persons_PersonId",
                table: "Appointments",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Persons_PersonId",
                table: "Prescriptions",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Persons_PersonId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Persons_PersonId",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_PersonProfiles_SocialSecurityNo",
                table: "PersonProfiles");

            migrationBuilder.DropColumn(
                name: "ActivePrescriptions",
                table: "PersonProfiles");

            migrationBuilder.DropColumn(
                name: "LastVisit",
                table: "PersonProfiles");

            migrationBuilder.DropColumn(
                name: "SocialSecurityNo",
                table: "PersonProfiles");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Prescriptions",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_PersonId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_PatientId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "PersonProfiles",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Authentications",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Appointments",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PersonId",
                table: "Appointments",
                newName: "IX_Appointments_PatientId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "PersonProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ActivePrescriptions = table.Column<int>(type: "int", nullable: true),
                    LastVisit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrescriptionId = table.Column<int>(type: "int", nullable: false),
                    SocialSecurityNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonProfiles_EmployeeId",
                table: "PersonProfiles",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonProfiles_PatientId",
                table: "PersonProfiles",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmployeeId",
                table: "Appointments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_EmployeeId",
                table: "Patients",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_SocialSecurityNo",
                table: "Patients",
                column: "SocialSecurityNo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Employees_EmployeeId",
                table: "Appointments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonProfiles_Employees_EmployeeId",
                table: "PersonProfiles",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonProfiles_Patients_PatientId",
                table: "PersonProfiles",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patients_PatientId",
                table: "Prescriptions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
