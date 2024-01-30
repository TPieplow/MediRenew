using Business.DTOs;
using Business.Interfaces;
using Spectre.Console;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

public class PharmacyHandler(IPharmacyService pharmacyService)
{
    private readonly IPharmacyService _pharmacyService = pharmacyService;

    public async Task ViewAllPharmacies()
    {
        try
        {
            Console.Clear();
            var pharmacies = await _pharmacyService.ViewAllPharmacy();

            if (pharmacies is not null)
            {
                var table = new Table();

                table.AddColumn("[yellow]Pharmacy-ID[/]");
                table.AddColumn("[yellow]Medication Name[/]");

                foreach (PharmacyDTO pharmacy in pharmacies)
                {
                    table.AddRow(
                        pharmacy.Id.ToString(),
                        pharmacy.MedicationName
                    );
                }

                AnsiConsole.Write(table);
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}
