using Business.DTOs;
using Infrastructure.Utils;
using static Infrastructure.Utils.ResultEnums;
using Spectre.Console;
using Table = Spectre.Console.Table;
using MediRenew.ConsoleApp.Utils;
using Business.Interfaces;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

public class StaffHandler(IStaffService staffService, DepartmentHandler departmentHandler)
{
    private readonly IStaffService _staffService = staffService;
    private readonly DepartmentHandler _departmentHandler = departmentHandler;

    public async Task AddStaffMember()
    {
        var newStaff = new StaffDTO();

        Console.Clear();
        AnsiConsole.Write(new Markup("[Red]Type cancel to abort operation[/]"));
        newStaff.FirstName = Cancel.AddOrAbort("\nEnter first name: ");
        if (newStaff.FirstName is null) return;

        newStaff.LastName = Cancel.AddOrAbort("Enter last name: ");
        if (newStaff.LastName is null) return;

        newStaff.RoleName = Cancel.AddOrAbort("Add a role: ");
        if (newStaff.RoleName is null) return;

        newStaff.PhoneNumber = Cancel.AddOrAbort("Enter phone number: ");
        if (newStaff.PhoneNumber is null) return;

        await _departmentHandler.GetAllDepartments();
        TryConvert.SetPropertyWithConversion(id => newStaff.DepartmentId = id, "Enter department-ID");

        var result = await _staffService.AddStaffMemberAsync(newStaff);

        if (result == Result.Failure)
        {
            ReturnMessage<StaffDTO>(CrudOperation.Create, result, "A staffmember with this phone number already exists.");
        }
        else
        {
            ReturnMessage<StaffDTO>(CrudOperation.Create, result, "");
        }
    }

    public async Task ViewOneStaffMemberWithId()
    {
        try
        {
            Console.Clear();
            await ViewAllStaff();
            Console.Write("Enter [ID]:");
            StaffDTO staff = null!;

            if (int.TryParse(Console.ReadLine(), out int Id))
            {
                staff = await _staffService.GetOneStaffMemberAsync(Id);

                if (staff is not null)
                {
                    Console.Clear();
                    var table = new Table();

                    table.AddColumn("ID");
                    table.AddColumn("First Name");
                    table.AddColumn("Last Name");
                    table.AddColumn("Role");
                    table.AddColumn("Phone Number");
                    table.AddColumn("Department");
                    table.AddColumn("Department-ID");

                    table.AddRow(
                        staff.Id.ToString(),
                        staff.FirstName,
                        staff.LastName,
                        staff.RoleName,
                        staff.PhoneNumber,
                        staff.Department.DepartmentName,
                        staff.Department.Id.ToString()
                        ); ;

                    AnsiConsole.Write(table);
                    Console.ReadKey();
                }
                else
                {
                    DisplayMessage.Message("Staff not found");
                }
            }
            else
            {
                DisplayMessage.Message("Invalid ID, please try again...");
            }
        }
        catch (Exception ex) { Console.WriteLine($"ERROR : {ex.Message}"); }

    }

    public async Task ViewAllStaff()
    {
        try
        {
            IEnumerable<StaffDTO> staffMembers = await _staffService.GetAllStaffAsync();

            if (staffMembers is not null)
            {
                Console.Clear();
                var table = new Table();

                table.AddColumn("[yellow]ID[/]");
                table.AddColumn("[yellow]First Name[/]");
                table.AddColumn("[yellow]Last Name[/]");
                table.AddColumn("[yellow]Role[/]");
                table.AddColumn("[yellow]Phone Number[/]");
                table.AddColumn("[yellow]Department ID[/]");

                foreach (StaffDTO staff in staffMembers)
                {
                    table.AddRow(
                        staff.Id.ToString(),
                        staff.FirstName,
                        staff.LastName,
                        staff.RoleName,
                        staff.PhoneNumber,
                        staff.DepartmentName
                        );
                }
                AnsiConsole.Write(table);
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    public async Task UpdateStaffMember()
    {
        try
        {
            Console.Clear();
            await ViewAllStaff();
            AnsiConsole.Write(new Markup("[Red]Type cancel to abort operation[/]"));
            Console.WriteLine("\nEnter [ID] to update:");
            if (int.TryParse(Console.ReadLine(), out var staffId))
            {
                var staffToUpdate = await _staffService.GetOneStaffMemberAsync(staffId);
                if (staffToUpdate is not null)
                {
                    staffToUpdate.FirstName = Cancel.AddOrAbort("First Name: ");
                    if (staffToUpdate.FirstName == null) { return; }

                    staffToUpdate.LastName = Cancel.AddOrAbort("Last Name: ");
                    if (staffToUpdate.LastName == null) { return; }

                    staffToUpdate.RoleName = Cancel.AddOrAbort("Role: ");
                    if (staffToUpdate.RoleName == null) { return; }

                    staffToUpdate.PhoneNumber = Cancel.AddOrAbort("Phone Number");
                    if (staffToUpdate.PhoneNumber == null) { return; }

                    staffToUpdate.DepartmentName = Cancel.AddOrAbort("Department");
                    if (staffToUpdate.DepartmentName == null) { return; }

                    var result = await _staffService.UpdateStaffAsync(staffToUpdate);

                    if (result == Result.Failure)
                    {
                        ReturnMessage<StaffDTO>(CrudOperation.Update, result, "A staffmember with this phone number already exists.");
                    }
                    else
                    {
                        ReturnMessage<StaffDTO>(CrudOperation.Update, result, "");
                    }
                }
                else
                {
                    DisplayMessage.Message("Staff not found");
                }
            }
            else
            {
                DisplayMessage.Message("Invalid ID, please try again...");
            }

        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    public async Task DeleteStaff()
    {
        Console.Clear();
        await ViewAllStaff();
        AnsiConsole.Write(new Markup("Enter [Green]ID[/] to delete: "));
        if (int.TryParse(Console.ReadLine(), out var staffId))
        {
            var result = await _staffService.DeleteStaffMemberAsync(staffId);
            ReturnMessage<StaffDTO>(CrudOperation.Delete, result, "");
        }
        else
        {
            DisplayMessage.Message("Invalid ID, please try again...");
        }
    }
}
