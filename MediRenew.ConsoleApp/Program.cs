
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
        var builder = Host.CreateDefaultBuilder().ConfigureServices(services => 
        {
            services.AddDbContext<CodeFirstDbContext>(x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=HospitalDb;Integrated Security=True;Trust Server Certificate=True"));
            services.AddScoped<PatientRepository>();
            services.AddScoped<PatientService>();
            services.AddScoped<PatientHandler>();
            services.AddScoped<LoginService>();
            services.AddScoped<LoggerFactory>();
        });

        Console.Clear();
        var host = builder.Build();

        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var loginService = serviceProvider.GetRequiredService<LoginService>();
        
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
