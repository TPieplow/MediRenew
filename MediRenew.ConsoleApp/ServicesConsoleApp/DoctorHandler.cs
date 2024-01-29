using Business.DTOs;
using Business.Services;
using Infrastructure.Utils;
using MediRenew.ConsoleApp.Utils;
using Spectre.Console;
using static Infrastructure.Utils.ResultEnums;

namespace MediRenew.ConsoleApp.ServicesConsoleApp
{
    public class DoctorHandler
    {
        private readonly DoctorService _doctorService;

        public DoctorHandler(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

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

                newDoctor.DepartmentId = Convert.ToInt32(Cancel.AddOrAbort("Enter the department-ID: "));
                if (newDoctor.DepartmentId == 0) return;

                var result = await _doctorService.AddDoctorAsync(newDoctor);

                switch (result)
                {
                    case Result.Success:
                        ReturnMessage<DoctorDTO>(CrudOperation.Create, result, "");
                        break;
                    case Result.Failure:
                        ReturnMessage<DoctorDTO>(CrudOperation.Create, result, "A doctor with this phone number already exists.");
                        break;
                    case Result.NotFound:
                        ReturnMessage<DoctorDTO>(CrudOperation.Create, result, "");
                        break;
                    default:
                        ReturnMessage<DoctorDTO>(CrudOperation.Create, result, "");
                        break;
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
                            doctor.DepartmentName

                        );

                        AnsiConsole.Write(table);
                        Console.ReadKey();
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
                    DisplayMessage.Message("");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public async Task UpdateDoctorById()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Enter Id of the doctor you want to update: ");
                if (int.TryParse(Console.ReadLine(), out int doctorId))
                {
                    var doctorToUpdate = await _doctorService.GetOneDoctorAsync(doctorId);

                    if (doctorToUpdate is not null)
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

                        switch (result)
                        {
                            case Result.Success:
                                ReturnMessage<DoctorDTO>(CrudOperation.Update, result, "Doctor successfully updated.");
                                break;
                            case Result.NotFound:
                                ReturnMessage<DoctorDTO>(CrudOperation.Update, result, "Doctor not found.");
                                break;
                            case Result.Failure:
                                ReturnMessage<DoctorDTO>(CrudOperation.Update, result, "Email already exists.");
                                break;
                            default:
                                ReturnMessage<DoctorDTO>(CrudOperation.Update, result, "Unexpected error from update operation.");
                                break;
                        }
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
                Console.WriteLine("Enter Id of the patient you want to remove: ");
                if (int.TryParse(Console.ReadLine(), out var doctorId))
                {
                    var result = await _doctorService.RemoveDoctorAsync(doctorId);
                    switch (result)
                    {
                        case Result.Success:
                            ReturnMessage<DoctorDTO>(CrudOperation.Delete, result, "");
                            break;
                        case Result.Failure:
                            ReturnMessage<DoctorDTO>(CrudOperation.Delete, result, "");
                            break;
                        case Result.NotFound:
                            ReturnMessage<DoctorDTO>(CrudOperation.Delete, result, "");
                            break;
                    }
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
