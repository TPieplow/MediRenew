using Business.DTOs;
using Business.Interfaces;
using Infrastructure.Utils;
using MediRenew.ConsoleApp.Utils;
using Spectre.Console;
using static Infrastructure.Utils.ResultEnums;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;

public class InvoiceHandler(IInvoiceService invoiceService, PharmacyHandler pharmacyHandler, PatientHandler patientHandler)
{
    private readonly IInvoiceService _invoiceService = invoiceService;
    private readonly PharmacyHandler _pharmacyHandler = pharmacyHandler;
    private readonly PatientHandler _patientHandler = patientHandler;

    public async Task AddInvoiceUI()
    {
        Console.Clear();
        var newInvoice = new InvoiceDTO();

        await _patientHandler.ViewAllPatients();
        TryConvert.SetPropertyWithConversion(id => newInvoice.PatientId = id, "Enter patient-ID");
        if (newInvoice.PatientId == 0) return;

        await _pharmacyHandler.ViewAllPharmacies();
        TryConvert.SetPropertyWithConversion(medId => newInvoice.PharmacyId = medId, "Enter med ID");
        if (newInvoice.PharmacyId == 0) return;

        newInvoice.Description = Cancel.AddOrAbort("Enter Description: ");
        if (newInvoice.Description == null) return;

        TryConvert.SetPropertyWithConversion(cost => newInvoice.Cost = cost, "Enter cost: ");
        if (newInvoice.Cost == 0) return;

        TryConvert.SetPropertyWithConversion(totalCost => newInvoice.TotalCost = totalCost, "Enter total cost");
        if (newInvoice.TotalCost == 0) return;

        var result = await _invoiceService.AddInvoiceAsync(newInvoice);
        if (result == Result.Failure)
        {
            ReturnMessage<InvoiceDTO>(CrudOperation.Create, result, "Invalid ID, patient doesnt exist. Please try again");
        }
        else
        {
            ReturnMessage<InvoiceDTO>(CrudOperation.Create, result, "");
        }
    }

    public async Task ViewOneInvoice()
    {
        try
        {
            Console.Clear();
            await ViewAllInvoices();
            Console.WriteLine("Enter Id of the invoice you want to see: ");
            var newInvoice = new InvoiceDTO();

            if (int.TryParse(Console.ReadLine(), out int invoiceId))
            {
                newInvoice = await _invoiceService.GetOneInvoiceAsync(invoiceId);

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
                    DisplayMessage.Message("");
                }
                else
                {
                    DisplayMessage.Message("Invoice not found");
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
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    public async Task DeleteInvoiceById()
    {
        try
        {
            Console.Clear();
            await ViewAllInvoices();
            Console.WriteLine("Enter Id of the invoice you want to remove: ");
            if (int.TryParse(Console.ReadLine(), out var invoiceId))
            {
                var result = await _invoiceService.RemoveInvoiceAsync(invoiceId);
                if (result == Result.Failure)
                {
                    ReturnMessage<InvoiceDTO>(CrudOperation.Delete, result, "Invalid ID. Please try again");
                }
                else
                {
                    ReturnMessage<InvoiceDTO>(CrudOperation.Delete, result, "");
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
    }
}
