using Business.ServiceCollectionBusiness;
using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.ServiceCollections;
using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServiceCollectionUI;
using MediRenew.ConsoleApp.ServicesConsoleApp;
using MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;
using MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MediRenew.ConsoleApp;


class Program
{
    static async Task Main()
    {
        //Skapar en DI - container och förbereder det vi behöver ha tillgång till i appen.
        var builder = Host.CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.BusinessDICluster();
                services.UIDICluster();
                services.InfrastructureDICluster();
            })


            // Stoppar loggning i consolen, visar enbart error nu. 
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Error);
                logging.AddConsole();
            });

        // Mainbuilder, som håller inloggning och de DI som behövs.
        using var host = builder.Build();
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var loginService = serviceProvider.GetRequiredService<LoginService>();

        Console.Clear();

        if (loginService.Login()) 
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
    }
}

