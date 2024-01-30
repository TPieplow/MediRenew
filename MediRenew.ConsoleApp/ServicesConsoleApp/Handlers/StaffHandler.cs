﻿using Business.DTOs;
using Business.Services;
using Infrastructure.Utils;
using static Infrastructure.Utils.ResultEnums;
using Spectre.Console;
using Table = Spectre.Console.Table;
using MediRenew.ConsoleApp.Utils;
using Business.Interfaces;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

public class StaffHandler(IStaffService staffService)
{
    private readonly IStaffService _staffService = staffService;

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

    public async Task ViewOneStaffMemberWithId()
    {
        try
        {
            Console.Clear();
            Console.Write("Enter [ID]:");
            StaffDTO staff = null!;

            if (int.TryParse(Console.ReadLine(), out int Id))
            {
                staff = await _staffService.GetOneStaffMember(Id);

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
            }
        }
        catch (Exception ex) { Console.WriteLine($"ERROR : {ex.Message}"); }

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

    public async Task UpdateStaffMember()
    {
        try
        {
            Console.WriteLine("Enter [ID] to update:");
            if (int.TryParse(Console.ReadLine(), out var staffId))
            {
                var staffToUpdate = await _staffService.GetOneStaffMember(staffId);
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
                    switch (result)
                    {
                        case Result.Success:
                            ReturnMessage<StaffDTO>(CrudOperation.Update, result, "Patient successfully updated.");
                            break;
                        case Result.NotFound:
                            ReturnMessage<StaffDTO>(CrudOperation.Update, result, "Patient not found.");
                            break;
                        case Result.Failure:
                            ReturnMessage<StaffDTO>(CrudOperation.Update, result, "Phone number already exists.");
                            break;
                        default:
                            ReturnMessage<StaffDTO>(CrudOperation.Update, result, "Unexpected error from update operation.");
                            break;
                    }
                }
            }

        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    public async Task DeleteStaff()
    {
        Console.Clear();
        Console.Write("Enter [ID] to DELETE: ");
        if (int.TryParse(Console.ReadLine(), out var staffId))
        {
            var result = await _staffService.DeleteStaffMember(staffId);
            switch (result)
            {
                case Result.Success:
                    ReturnMessage<StaffDTO>(CrudOperation.Delete, result, "");
                    break;
                case Result.Failure:
                    ReturnMessage<StaffDTO>(CrudOperation.Delete, result, "");
                    break;
                case Result.NotFound:
                    ReturnMessage<StaffDTO>(CrudOperation.Delete, result, "");
                    break;
                default:
                    ReturnMessage<StaffDTO>(CrudOperation.Delete, result, "");
                    break;


            }
        }
    }
}
