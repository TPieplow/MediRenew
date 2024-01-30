using Business.Interfaces;
using Business.Services;
using MediRenew.ConsoleApp.Login;
using MediRenew.ConsoleApp.ServicesConsoleApp;
using MediRenew.ConsoleApp.ServicesConsoleApp.Handlers;
using MediRenew.ConsoleApp.ServicesConsoleApp.SubMenus;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace MediRenew.ConsoleApp.ServiceCollectionUI;

public static class UIServiceCollection
{
    public static void UIDICluster (this IServiceCollection services)
    {
        services.AddScoped<MainMenu>();
        services.AddScoped<HospitalMenu>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<LoginService>();

        services.AddScoped<RegistrationHandler>();

        services.AddScoped<PatientHandler>();
        services.AddScoped<PatientMenu>();

        services.AddScoped<PrescriptionHandler>();
        services.AddScoped<PrescriptionMenu>();

        services.AddScoped<DoctorHandler>();
        services.AddScoped<DoctorMenu>();

        services.AddScoped<AppointmentHandler>();
        services.AddScoped<AppointmentMenu>();

        services.AddScoped<StaffHandler>();
        services.AddScoped<StaffMenu>();

        services.AddScoped<InvoiceHandler>();
        services.AddScoped<InvoiceMenu>();

        services.AddScoped<PharmacyHandler>();
        services.AddScoped<PharmacyMenu>();
    }
}
