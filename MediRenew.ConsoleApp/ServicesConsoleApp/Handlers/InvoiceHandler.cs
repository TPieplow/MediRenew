using Business.DTOs;
using Business.Services;
using MediRenew.ConsoleApp.Utils;
using Spectre.Console;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

public class InvoiceHandler(InvoiceService invoiceService)
{
    private readonly InvoiceService _invoiceService = invoiceService;

    public async Task ViewAllInvoices()
    {
        try
        {
            Console.Clear();
            var invoices = await _invoiceService.ViewPatientInvoices();

            if (invoices is not null)
            {
                var table = new Table();

                table.AddColumn("[yellow]Invoice-ID[/]");
                table.AddColumn("[yellow]Description[/]");
                table.AddColumn("[yellow]Cost[/]");
                table.AddColumn("[yellow]Total Cost[/]");
                table.AddColumn("[yellow]Patient-ID[/]");
                table.AddColumn("[yellow]Patient Name[/]");
                table.AddColumn("[yellow]Medication[/]");

                foreach (InvoiceDTO invoice in invoices)
                {
                    table.AddRow(
                        invoice.Id.ToString(),
                        invoice.Description,
                        invoice.Cost.ToString(),
                        invoice.TotalCost.ToString(),
                        invoice.PatientId.ToString(),
                        invoice.PatientName,
                        invoice.MedicationName
                    ) ;
                }
                AnsiConsole.Write(table);
                DisplayMessage.Message("");
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}
