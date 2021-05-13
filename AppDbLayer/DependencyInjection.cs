using AppDbCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


namespace AppDbLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepo(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<JobFindingDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("JobFinding")));


            services.AddScoped<IJobFindingDatabaseRepo, JobFindingDatabaseRepo>();
            services.AddAutoMapper(typeof(JobMapper));

            return services;
        }
    }
}
