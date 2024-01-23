using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServicesConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MediRenew.ConsoleApp;


class Program
{
    static async Task Main()
    {
        // Skapar en DI-container och förbereder det vi behöver ha tillgång till i appen.
        var builder = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<CodeFirstDbContext>(x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=HospitalDb;Integrated Security=True;Trust Server Certificate=True"));
                services.AddScoped<PatientRepository>();
                services.AddScoped<PatientService>();
                services.AddScoped<PatientHandler>();
                services.AddScoped<LoginService>();
            })
            // Stoppar loggningen av SQL-querys när användaren hämtar från db.
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Error);
                logging.AddConsole();
            });

        // Skapar ett nytt scope och kallar på LoginService
        using var host = builder.Build();
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var loginService = serviceProvider.GetRequiredService<LoginService>();

        Console.Clear();

        if (loginService.Login()) 
        {
            var hospitalMenu = new HospitalMenu(serviceProvider.GetRequiredService<PatientHandler>());
            await hospitalMenu.MenuAsync();
        }
        else
        {
            Console.WriteLine("Login failed. Exiting application.");
        }
    }
}

