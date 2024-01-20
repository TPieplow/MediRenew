using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPharmacyFKtoPatients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 1, 1 },
                column: "Date",
                value: new DateTime(2024, 1, 20, 11, 25, 57, 223, DateTimeKind.Local).AddTicks(693));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 2, 2 },
                column: "Date",
                value: new DateTime(2024, 1, 20, 11, 25, 57, 223, DateTimeKind.Local).AddTicks(749));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 3, 3 },
                column: "Date",
                value: new DateTime(2024, 1, 20, 11, 25, 57, 223, DateTimeKind.Local).AddTicks(751));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 1, 20, 11, 25, 57, 223, DateTimeKind.Local).AddTicks(806));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 1, 20, 11, 25, 57, 223, DateTimeKind.Local).AddTicks(819));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 1, 20, 11, 25, 57, 223, DateTimeKind.Local).AddTicks(821));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 1, 1 },
                column: "Date",
                value: new DateTime(2024, 1, 20, 11, 19, 9, 16, DateTimeKind.Local).AddTicks(3225));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 2, 2 },
                column: "Date",
                value: new DateTime(2024, 1, 20, 11, 19, 9, 16, DateTimeKind.Local).AddTicks(3287));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumns: new[] { "DoctorId", "PatientId" },
                keyValues: new object[] { 3, 3 },
                column: "Date",
                value: new DateTime(2024, 1, 20, 11, 19, 9, 16, DateTimeKind.Local).AddTicks(3289));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 1, 20, 11, 19, 9, 16, DateTimeKind.Local).AddTicks(3388));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 1, 20, 11, 19, 9, 16, DateTimeKind.Local).AddTicks(3396));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 1, 20, 11, 19, 9, 16, DateTimeKind.Local).AddTicks(3398));
        }
    }
}
