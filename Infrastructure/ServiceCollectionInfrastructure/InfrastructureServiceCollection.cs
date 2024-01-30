using Infrastructure.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceCollections
{
    public static class InfrastructureServiceCollection
    {
        public static void InfrastructureDICluster(this IServiceCollection services)
        {
            services.AddDbContext<CodeFirstDbContext>(
                x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=HospitalDb;Integrated Security=True;Trust Server Certificate=True"));

            services.AddDbContext<DatabaseFirstDbContext>(
                x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=HospitalDb;Integrated Security=True;Trust Server Certificate=True"));

            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository > ();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IPharmacyRepository, PharmacyRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        }
    }
}
