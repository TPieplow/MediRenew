using Business.DTOs;
using Business.Services;
using Infrastructure.Utils;
using static Infrastructure.Utils.ResultEnums;

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
}
