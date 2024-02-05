using Infrastructure.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServiceCollections
{
    public static class InfrastructureServiceCollection
    {
        public static void InfrastructureDICluster(this IServiceCollection services)
        {
            services.AddDbContext<CodeFirstDbContext>(
                x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=HospitalDb;Integrated Security=True;Trust Server Certificate=True"));

            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository > ();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IPharmacyRepository, PharmacyRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();


            services.AddDbContext<DatabaseFirstDbContext>(
                x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=HospitalDb;Integrated Security=True;Trust Server Certificate=True"));

            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        }
    }
}
