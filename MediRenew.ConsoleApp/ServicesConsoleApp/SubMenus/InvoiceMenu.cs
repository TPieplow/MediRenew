using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;
using MediRenew.ConsoleApp.Utils;

namespace MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus;

public class InvoiceMenu(InvoiceHandler invoiceHandler)
{
    private readonly InvoiceHandler _invoiceHandler = invoiceHandler;

    public async Task InvoiceMenuAsync()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Header.StaticHeader();

            string[] menu =
            [
                "1. Create Invoice",
                "2. Find invoice through ID",
                "3. View invoices",
                "4. Delete invoice",
                "0. Return to main menu"
            ];

            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine(menu[i]);
            }

            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    await _invoiceHandler.AddInvoiceUI();
                    break;

                case "2":
                    await _invoiceHandler.ViewOneInvoice();
                    break;

                case "3":
                    await _invoiceHandler.ViewAllInvoices();
                    DisplayMessage.Message("");
                    break;

                case "4":
                    await _invoiceHandler.DeleteInvoiceById();
                    break;

                case "0":
                    running = false;
                    break;
            }
        }
    }
}
