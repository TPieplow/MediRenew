using Business.DTOs;
using Business.Interfaces;
using Business.Services;
using Infrastructure.Utils;
using MediRenew.ConsoleApp.Utils;
using Spectre.Console;
using static Infrastructure.Utils.ResultEnums;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers
{
    public class DoctorHandler(IDoctorService doctorService, DepartmentHandler departmentHandler)
    {
        private readonly IDoctorService _doctorService = doctorService;
        private readonly DepartmentHandler _departmentHandler = departmentHandler;

        public async Task AddDoctor()
        {
            try
            {
                Console.Clear();
                var newDoctor = new DoctorDTO();
                Console.WriteLine("Enter cancel to abort.");

                newDoctor.FirstName = Cancel.AddOrAbort("Enter first name: ");
                if (newDoctor.FirstName == null) return;

                newDoctor.LastName = Cancel.AddOrAbort("Enter last name: ");
                if (newDoctor.LastName == null) return;

                newDoctor.PhoneNumber = Cancel.AddOrAbort("Enter phone number:");
                if (newDoctor.PhoneNumber == null) return;

                await _departmentHandler.GetAllDepartments();
                newDoctor.DepartmentId = Convert.ToInt32(Cancel.AddOrAbort("Enter the department-ID: "));
                if (newDoctor.DepartmentId == 0) return;

                var result = await _doctorService.AddDoctorAsync(newDoctor);

                ReturnMessage<DoctorDTO>(CrudOperation.Create, result, "");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public async Task ViewOneDoctorWithId()
        {
            try
            {
                Console.Clear();
                await ViewAllDoctors();
                Console.WriteLine("Enter Id");
                DoctorDTO doctor = null!;

                if (int.TryParse(Console.ReadLine()!, out int Id))
                {
                    doctor = await _doctorService.GetOneDoctorAsync(Id);

                    if (doctor is not null)
                    {
                        var table = new Table();

                        table.AddColumn("[yellow]ID[/]");
                        table.AddColumn("[yellow]First name[/]");
                        table.AddColumn("[yellow]Last name[/]");
                        table.AddColumn("[yellow]Phone number[/]");
                        table.AddColumn("[yellow]Department[/]");


                        table.AddRow(
                            doctor.Id.ToString(),
                            doctor.FirstName,
                            doctor.LastName,
                            doctor.PhoneNumber,
                            doctor.Department.DepartmentName

                        );

                        AnsiConsole.Write(table);
                        DisplayMessage.Message("");
                    }
                    else
                    {
                        DisplayMessage.Message("Doctor not found.");
                    }
                }
                else
                {
                    DisplayMessage.Message("Invalid input.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }


        public async Task ViewAllDoctors()
        {
            try
            {
                Console.Clear();
                var doctors = await _doctorService.GetAllDoctors();

                if (doctors is not null)
                {
                    var table = new Table();

                    table.AddColumn("[yellow]Doctor-ID[/]");
                    table.AddColumn("[yellow]First name[/]");
                    table.AddColumn("[yellow]Last name[/]");
                    table.AddColumn("[yellow]Phone[/]");
                    table.AddColumn("[yellow]Department[/]");

                    foreach (DoctorDTO doctor in doctors)
                    {
                        table.AddRow(
                            doctor.Id.ToString(),
                            doctor.FirstName,
                            doctor.LastName,
                            doctor.PhoneNumber,
                            doctor.DepartmentName
                        );
                    }
                    AnsiConsole.Write(table);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public async Task UpdateDoctorById()
        {
            try
            {
                Console.Clear();
                await ViewAllDoctors();
                Console.WriteLine("Enter Id of the doctor you want to update: ");
                if (int.TryParse(Console.ReadLine(), out int doctorId))
                {
                    var doctorToUpdate = await _doctorService.GetOneDoctorAsync(doctorId);

                    if (doctorToUpdate is null)
                    {
                        DisplayMessage.Message("Doctor not found");
                    }
                    else
                    {
                        Console.Write("First Name:");
                        doctorToUpdate.FirstName = Console.ReadLine()!;

                        Console.Write("Last Name:");
                        doctorToUpdate.LastName = Console.ReadLine()!;

                        Console.Write("Phone Number: ");
                        doctorToUpdate.PhoneNumber = Console.ReadLine()!;

                        Console.Write("Department-ID: ");
                        doctorToUpdate.DepartmentId = Convert.ToInt32(Console.ReadLine());

                        var result = await _doctorService.UpdateDoctorAsync(doctorToUpdate);
                        ReturnMessage<DoctorDTO>(CrudOperation.Update, result, "");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ERROR: {ex.Message}");
            }
        }

        public async Task<int?> DeleteDoctorById()
        {
            try
            {
                Console.Clear();
                await ViewAllDoctors();
                Console.WriteLine("WARNING! DELETING A DOCTOR WILL REMOVE IT'S APPOINTMENTS"); //Röd text här
                Console.WriteLine("Enter Id of the doctor you want to remove: ");
                if (int.TryParse(Console.ReadLine(), out var doctorId))
                {
                    var result = await _doctorService.RemoveDoctorAsync(doctorId);

                    ReturnMessage<DoctorDTO>(CrudOperation.Delete, result, "");
                }
            }
            catch (Exception ex)
            {
                DisplayMessage.Message($" ERROR: {ex.Message}");
            }
            return null;
        }
    }
}
