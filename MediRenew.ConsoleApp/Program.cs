
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
        var builder = Host.CreateDefaultBuilder().ConfigureServices(services => 
        {
            services.AddDbContext<CodeFirstDbContext>(x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=HospitalDb;Integrated Security=True;Trust Server Certificate=True"));
            services.AddScoped<PatientRepository>();
            services.AddScoped<PatientService>();
            services.AddScoped<PatientHandler>();
            services.AddScoped<LoginService>();
        });

        // Den här delen tar hand om loggningen, dvs det var detta som gjorde att vi slipper få ut alla querys när vi hämtar från DB, dessutom vill man
        // inte visa hur tabell är uppbyggt för user, det är en säkerhetsrisk
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders(); // Tar bort befintliga loggare
            logging.SetMinimumLevel(LogLevel.Error); //Den här delen låter oss ange nivån för loggning
            logging.AddConsole(); // Denna del lägger till konsolloggningsleverantören
        });

        Console.Clear();

        // Detta är delen som startar själva appen, här har jag lagt in LoginService klassen.
        var host = builder.Build();
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var loginService = serviceProvider.GetRequiredService<LoginService>();
        
        // Då method Login är en bool, vid lyckad inloggning returnerar den true och vi hamnar i if statement, det är därifrån MenuAsync startar upp.
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
