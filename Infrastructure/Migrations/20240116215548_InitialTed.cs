using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialTed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Persons_PersonId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Roles_RoleId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Persons_PersonId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Persons_RoleId",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Prescriptions",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_PersonId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_PatientId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Persons",
                newName: "ProfileId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Appointments",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PersonId",
                table: "Appointments",
                newName: "IX_Appointments_PatientId");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "ActivePrescriptions",
                table: "PersonProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleEntityId",
                table: "PersonProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ProfileId",
                table: "Persons",
                column: "ProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonProfiles_RoleEntityId",
                table: "PersonProfiles",
                column: "RoleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Persons_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Persons_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Authentications_Persons_PersonId",
                table: "Authentications",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonProfiles_Roles_RoleEntityId",
                table: "PersonProfiles",
                column: "RoleEntityId",
                principalTable: "Roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_PersonProfiles_ProfileId",
                table: "Persons",
                column: "ProfileId",
                principalTable: "PersonProfiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Persons_DoctorId",
                table: "Prescriptions",
                column: "DoctorId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Persons_PatientId",
                table: "Prescriptions",
                column: "PatientId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Persons_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Persons_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Authentications_Persons_PersonId",
                table: "Authentications");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonProfiles_Roles_RoleEntityId",
                table: "PersonProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_PersonProfiles_ProfileId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Persons_DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Persons_PatientId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ProfileId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_PersonProfiles_RoleEntityId",
                table: "PersonProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "RoleEntityId",
                table: "PersonProfiles");

            migrationBuilder.DropColumn(
                name: "DoctorId",
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
                name: "ProfileId",
                table: "Persons",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Appointments",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                newName: "IX_Appointments_PersonId");

            migrationBuilder.AlterColumn<bool>(
                name: "ActivePrescriptions",
                table: "PersonProfiles",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

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
                name: "FK_Persons_Roles_RoleId",
                table: "Persons",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Persons_PersonId",
                table: "Prescriptions",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
