using Business.DTOs;
using Business.Interfaces;
using Business.Services;
using Spectre.Console;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

public class DepartmentHandler(IDepartmentService departmentService)
{
    private readonly IDepartmentService _departmentService = departmentService;

    public async Task GetAllDepartments()
    {
        try
        {
            Console.Clear();
            var departments = await _departmentService.GetAllDepartments();

            if (departments is not null)
            {
                var table = new Table();

                table.AddColumn("[yellow]Department[/]");
                table.AddColumn("[yellow]Department-ID[/]");

                foreach (DepartmentDTO department in departments)
                {
                    table.AddRow(
                        department.DepartmentName,
                        department.Id.ToString()
                    );
                }
                AnsiConsole.Write(table);
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}
