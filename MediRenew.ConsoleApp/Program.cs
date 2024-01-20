using Infrastructure.Contexts;
using Infrastructure.Repositories;
using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServicesConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MediRenew.ConsoleApp;


class Program
{
    static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder();

        builder.ConfigureServices(services =>
        {
            services.AddDbContext<CodeFirstDbContext>(x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=HospitalDb;Integrated Security=True;Trust Server Certificate=True"));
            services.AddScoped<PatientRepository>();
            services.AddScoped<LoginService>();
        });

        var host = builder.Build();

        using (var scope = host.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var loginService = serviceProvider.GetRequiredService<LoginService>();

            if (LoginService.Login())
            {
                var hospitalMenu = new HospitalMenu(serviceProvider.GetRequiredService<PatientService>());
                hospitalMenu.Menu();
            }
            else
            {
                Console.WriteLine("Login failed. Exiting application.");
            }
        }
    }
}
