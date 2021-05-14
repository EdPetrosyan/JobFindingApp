using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using JobFindingModels;
using JobFindingModels.DTOs;
using System.Threading.Tasks;
using System.Threading;

namespace AppDbCore
{
    public class JobFindingDbContext : DbContext
    {
        static IConfigurationRoot _configuration;

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobCategory> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<GetJobsForListingDto> JobsForListingDtos {get;set;}
        public DbSet<GetJobByIdDto> GetJobByIdDtos { get; set; }

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

       
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GetJobsForListingDto>(x => x.ToView("GetJobsForListing"));
            modelBuilder.Entity<GetJobByIdDto>(x => x.ToView("GetJobById"));
        }

        private void OnBeforeSaving()
        {
            var tracker = ChangeTracker;
            foreach (var entry in tracker.Entries())
            {
                if (entry.Entity is FullAuditModel)
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
                            auditModel.IsActive = true;
                            auditModel.IsDeleted = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
