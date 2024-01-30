using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Business.ServiceCollectionBusiness;

public static class BusinessServiceCollection
{
    public static void BusinessDICluster(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IPrescriptionService, PrescriptionService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<DepartmentService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IPharmacyService, PharmacyService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
    }
}
