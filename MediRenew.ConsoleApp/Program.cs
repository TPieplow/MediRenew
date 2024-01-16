﻿using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MediRenew.ConsoleApp;

public class Program
{
    static void Main(string[] args)
    {

        var builder = Host.CreateDefaultBuilder();

        builder.ConfigureServices(services =>
        {
            services.AddDbContext<CodeFirstDbContext>(x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=MediRenewHospitalDb;Integrated Security=True;Trust Server Certificate=True"));
        });

        builder.Build();
    }
}