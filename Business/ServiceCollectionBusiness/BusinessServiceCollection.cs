using Business.Services;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Business.ServiceCollectionBusiness;

public static class BusinessServiceCollection
{
    public static void BusinessDICluster(this IServiceCollection services)
    {
        services.AddScoped<PatientService>();
        services.AddScoped<PrescriptionService>();
        services.AddScoped<DoctorService>();
        services.AddScoped<AppointmentService>();
        services.AddScoped<StaffService>();
        services.AddScoped<DepartmentRepository>();
        services.AddScoped<InvoiceService>();
        services.AddScoped<PharmacyService>();
    }
}
