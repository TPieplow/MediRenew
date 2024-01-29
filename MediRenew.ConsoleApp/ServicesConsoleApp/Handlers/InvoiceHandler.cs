using Business.DTOs;
using Business.Services;
using Infrastructure.Utils;
using MediRenew.ConsoleApp.Utils;
using Spectre.Console;
using static Infrastructure.Utils.ResultEnums;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

public class InvoiceHandler(InvoiceService invoiceService)
{
    private readonly InvoiceService _invoiceService = invoiceService;

    public async Task AddInvoiceUI()
    {
        Console.Clear();

        var newInvoice = new InvoiceDTO();

        TryConvert.SetPropertyWithConversion(id => newInvoice.PatientId = id, "Enter patient-ID");

        newInvoice.Description = Cancel.AddOrAbort("Enter Description: ");
        if (newInvoice.Description == null) return;

        TryConvert.SetPropertyWithConversion(cost => newInvoice.Cost = cost, "Enter cost: ");
        TryConvert.SetPropertyWithConversion(totalCost => newInvoice.TotalCost = totalCost, "Enter total cost");

        //newInvoice.PatientName = Cancel.AddOrAbort("Enter patients name (First name and last name): ");
        //if (newInvoice.PatientName == null) return;

        //Console.WriteLine("Enter IDs separated by spaces (i.e: 1 2 3");


        TryConvert.SetPropertyWithConversion(medId => newInvoice.PharmacyId = medId, "Enter med ID");

        var result = await _invoiceService.AddInvoiceAsync(newInvoice);
        ReturnMessage<InvoiceDTO>(CrudOperation.Create, result, $"New invoice created for {newInvoice.PatientId}");
        Console.ReadKey();
    }

    public async Task ViewOneInvoice()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Enter Id of the patient you want to update: ");
            var newInvoice = new InvoiceDTO();

            if (int.TryParse(Console.ReadLine(), out int invoiceId))
            {
                newInvoice = await _invoiceService.GetOneInvoice(invoiceId);

                if (newInvoice is not null)
                {
                    Console.Clear();
                    var table = new Table();

                    table.AddColumn("[yellow]Invoice-ID[/]");
                    table.AddColumn("[yellow]Description[/]");
                    table.AddColumn("[yellow]Cost[/]");
                    table.AddColumn("[yellow]Total Cost[/]");
                    table.AddColumn("[yellow]Patient-ID[/]");
                    table.AddColumn("[yellow]Pharmacy-ID[/]");
                    table.AddColumn("[yellow]Medication[/]");

                    table.AddRow(
                            newInvoice.Id.ToString(),
                            newInvoice.Description,
                            newInvoice.Cost.ToString(),
                            newInvoice.TotalCost.ToString(),
                            newInvoice.PatientId.ToString(),
                            newInvoice.PharmacyId.ToString(),
                            newInvoice.Pharmacy.MedicationName
                            );

                    AnsiConsole.Write(table);
                    Console.ReadKey();
                }
                else
                {
                    DisplayMessage.Message("Funkar inte!!!!!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($" ERROR: {ex.Message}");
        }
    }

    public async Task ViewAllInvoices()
    {
        try
        {
            Console.Clear();
            var invoices = await _invoiceService.ViewPatientInvoicesAsync();

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
                    );
                }
                AnsiConsole.Write(table);
                DisplayMessage.Message("");
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    public async Task DeleteInvoiceById()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Enter Id of the invoice you want to remove: ");
            if (int.TryParse(Console.ReadLine(), out var invoiceId))
            {
                var result = await _invoiceService.RemoveInvoiceAsync(invoiceId);
                switch (result)
                {
                    case Result.Success:
                        ReturnMessage<InvoiceDTO>(CrudOperation.Delete, result, "");
                        break;
                    case Result.Failure:
                        ReturnMessage<InvoiceDTO>(CrudOperation.Delete, result, "");
                        break;
                    case Result.NotFound:
                        ReturnMessage<InvoiceDTO>(CrudOperation.Delete, result, "");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            DisplayMessage.Message($" ERROR: {ex.Message}");
        }
    }
}
