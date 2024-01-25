using Business.DTOs;
using Business.Services;
using Infrastructure.Utils;
using static Infrastructure.Utils.ResultEnums;
using Spectre.Console;
using Table = Spectre.Console.Table;
using MediRenew.ConsoleApp.Utils;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

public class StaffHandler(StaffService staffService)
{
    private readonly StaffService _staffService = staffService;

    public async Task AddStaffMember()
    {
        var newStaff = new StaffDTO();

        Console.WriteLine("Type cancel to abort operation");
        newStaff.FirstName = Cancel.AddOrAbort("Enter first name: ");
        if (newStaff.FirstName is null) return;

        newStaff.LastName = Cancel.AddOrAbort("Enter last name: ");
        if (newStaff.LastName is null) return;

        newStaff.RoleName = Cancel.AddOrAbort("Add a role: ");
        if (newStaff.RoleName is null) return;

        newStaff.PhoneNumber = Cancel.AddOrAbort("Enter phone number: ");
        if (newStaff.PhoneNumber is null) return;

        Console.WriteLine("Department [ID]");
        if (int.TryParse(Console.ReadLine(), out int departmentid))
        {
            newStaff.DepartmentId = departmentid;
        }
        //newStaff.DepartmentName = Cancel.AddOrAbort("Enter department: ");
        //if (newStaff.DepartmentName is null) return;    

        var result = await _staffService.AddStaffMember(newStaff);

        switch (result)
        {
            case Result.Success:
                ReturnMessage<StaffDTO>(CrudOperation.Create, result, "");
                break;
            case Result.Failure:
                ReturnMessage<StaffDTO>(CrudOperation.Create, result, $"{newStaff.FirstName} added successfully.");
                break;
            case Result.NotFound:
                ReturnMessage<StaffDTO>(CrudOperation.Create, result, "");
                break;
            default:
                ReturnMessage<StaffDTO>(CrudOperation.Create, result, "");
                break;
        }
    }

    public async Task ViewAllStaff()
    {
        try
        {
            IEnumerable<StaffDTO> staffMembers = await _staffService.GetAllStaff();

            if (staffMembers is not null)
            {
                var table = new Table();

                table.AddColumn("ID");
                table.AddColumn("First Name");
                table.AddColumn("Last Name");
                table.AddColumn("Role ");
                table.AddColumn("Phone Number");
                table.AddColumn("Department ID");

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
                DisplayMessage.Message("");
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }

    }
}
