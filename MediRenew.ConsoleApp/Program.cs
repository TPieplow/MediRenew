using Business.ServiceCollectionBusiness;
using Business.Services;
using Infrastructure.ServiceCollections;
using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServiceCollectionUI;
using MediRenew.ConsoleApp.ServicesConsoleApp;
using MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MediRenew.ConsoleApp;


class Program
{
    static async Task Main()
    {
        // DI-container, prepares all dependencies.
        var builder = Host.CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.BusinessDICluster();
                services.UIDICluster();
                services.InfrastructureDICluster();
            })

            // Stops the visable loggin in the console, only displays error messages. 
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Error);
                logging.AddConsole();
            });

        // Mainbuilder with service provider
        using var host = builder.Build();
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var mainMenu = serviceProvider.GetRequiredService<MainMenu>();
        bool loginSuccess = await mainMenu.ShowMenuAsync();
        
        // If login returns true, access HospitalMenu. 
        if (loginSuccess)
        {
            var hospitalMenu = new HospitalMenu(
            serviceProvider.GetRequiredService<PatientMenu>(),
            serviceProvider.GetRequiredService<PrescriptionMenu>(),
            serviceProvider.GetRequiredService<DoctorMenu>(),
            serviceProvider.GetRequiredService<StaffMenu>(),
            serviceProvider.GetRequiredService<InvoiceMenu>(),
            serviceProvider.GetRequiredService<PharmacyMenu>(),
            serviceProvider.GetRequiredService<AppointmentMenu>());
            await hospitalMenu.MenuAsync();
        }
        else
        {
            Console.WriteLine("Login failed. Exiting application.");
        }
        Console.Clear();
    }
}

