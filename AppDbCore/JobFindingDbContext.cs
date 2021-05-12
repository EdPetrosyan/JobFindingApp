using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using JobFindingModels;

namespace AppDbCore
{
    public class JobFindingDbContext : DbContext
    {
        static IConfigurationRoot _configuration;

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobCategory> Categories { get; set; }
        public DbSet<Company>  Companies { get; set; }

        public JobFindingDbContext():base()
        {

        }
        public JobFindingDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true);
                _configuration = builder.Build();
                var connStr = _configuration.GetConnectionString("JobFinding");
                optionsBuilder.UseSqlServer(connStr);
            }
        }

        public override int SaveChanges()
        {
            var tracker = ChangeTracker;
            foreach (var entry in tracker.Entries())
            {
                if(entry.Entity is FullAuditModel)
                {
                    var auditModel = entry.Entity as FullAuditModel;
                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                        case EntityState.Modified:
                            auditModel.LastModifiedDate = DateTime.Now;
                            break;
                        case EntityState.Added:
                            auditModel.CreatedDate = DateTime.Now;
                            break;
                        default:
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
