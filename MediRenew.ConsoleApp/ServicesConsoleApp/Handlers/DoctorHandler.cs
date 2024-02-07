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
                AnsiConsole.Write(new Markup("[Red]Type cancel to abort operation[/]"));

                newDoctor.FirstName = Cancel.AddOrAbort("\nEnter first name: ");
                if (newDoctor.FirstName == null) return;

                newDoctor.LastName = Cancel.AddOrAbort("Enter last name: ");
                if (newDoctor.LastName == null) return;

                newDoctor.PhoneNumber = Cancel.AddOrAbort("Enter phone number:");
                if (newDoctor.PhoneNumber == null) return;

                await _departmentHandler.GetAllDepartments();
                TryConvert.SetPropertyWithConversion(id => newDoctor.DepartmentId = id, "Enter department-ID");
                if (newDoctor.DepartmentId == 0) return;

                var result = await _doctorService.AddDoctorAsync(newDoctor);
                if (result == Result.Success)
                {
                    ReturnMessage<DoctorDTO>(CrudOperation.Create, result, "");
                }
                else if (result == Result.Failure)
                {
                    ReturnMessage<DoctorDTO>(CrudOperation.Create, result, "A doctor with this phone number already exists");
                }
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
                    DisplayMessage.Message("Invalid ID, please try again...");
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
                        if (result == Result.Success)
                        {
                            ReturnMessage<DoctorDTO>(CrudOperation.Update, result, "");
                        }
                        else if (result == Result.Failure)
                        {
                            ReturnMessage<DoctorDTO>(CrudOperation.Update, result, "A doctor with this phone number already exists");
                        }
                    }
                }
                else
                {
                    DisplayMessage.Message("Invalid ID, please try again...");
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
                AnsiConsole.Write(new Markup("[Red]WARNING! DELETING A DOCTOR WILL REMOVE IT'S APPOINTMENTS[/]\n"));
                Console.WriteLine("Enter Id of the doctor you want to remove: ");
                if (int.TryParse(Console.ReadLine(), out var doctorId))
                {
                    var result = await _doctorService.RemoveDoctorAsync(doctorId);
                    if (result == Result.Failure)
                    {
                        ReturnMessage<DoctorDTO>(CrudOperation.Delete, result, "Invalid ID, please try again.");
                    }
                    else
                    {
                        ReturnMessage<DoctorDTO>(CrudOperation.Delete, result, "");
                    }
                }
                else
                {
                    DisplayMessage.Message("Invalid ID, please try again...");
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
