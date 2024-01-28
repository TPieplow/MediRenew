using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MediRenew.ConsoleApp.Login;
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
        // Skapar en DI-container och förbereder det vi behöver ha tillgång till i appen.
        var builder = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<CodeFirstDbContext>(
                    x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=HospitalDb;Integrated Security=True;Trust Server Certificate=True"));

                services.AddDbContext<DatabaseFirstDbContext>(
                    x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=HospitalDb;Integrated Security=True;Trust Server Certificate=True"));

                services.AddScoped<HospitalMenu>();

                services.AddScoped<LoginService>();
                services.AddScoped<DatabaseManager>();

                services.AddScoped<PatientRepository>();
                services.AddScoped<PatientService>();
                services.AddScoped<PatientHandler>();
                services.AddScoped<PatientMenu>();

                services.AddScoped<PrescriptionRepository>();
                services.AddScoped<PrescriptionService>();
                services.AddScoped<PrescriptionHandler>();
                services.AddScoped<PrescriptionMenu>();

                services.AddScoped<DoctorRepository>();
                services.AddScoped<DoctorService>();
                services.AddScoped<DoctorHandler>();
                services.AddScoped<DoctorMenu>();

                services.AddScoped<AppointmentRepository>();
                services.AddScoped<AppointmentService>();

                services.AddScoped<StaffRepository>();
                services.AddScoped<StaffService>();
                services.AddScoped<StaffHandler>();
                services.AddScoped<StaffMenu>();

                services.AddScoped<DepartmentRepository>();

                services.AddScoped<InvoiceRepository>();
                services.AddScoped<InvoiceService>();
                services.AddScoped<InvoiceHandler>();
                services.AddScoped<InvoiceMenu>();

                services.AddScoped<PharmacyRepository>();
                services.AddScoped<PharmacyService>();
                services.AddScoped<PharmacyHandler>();
                services.AddScoped<PharmacyMenu>();

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
                serviceProvider.GetRequiredService<PharmacyMenu>());
            await hospitalMenu.MenuAsync();
        }
        else
        {
            Console.WriteLine("Login failed. Exiting application.");
        }
    }
}

