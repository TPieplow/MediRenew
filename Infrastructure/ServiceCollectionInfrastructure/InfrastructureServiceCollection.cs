using Infrastructure.Contexts;
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

            services.AddScoped<AuthenticationRepository>();
            services.AddScoped<PatientRepository>();
            services.AddScoped<PrescriptionRepository>();
            services.AddScoped<DoctorRepository>();
            services.AddScoped<AppointmentRepository>();
            services.AddScoped<StaffRepository>();
            services.AddScoped<DepartmentRepository>();
            services.AddScoped<InvoiceRepository>();
            services.AddScoped<PharmacyRepository>();
        }
    }
}
