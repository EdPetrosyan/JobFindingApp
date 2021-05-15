using AppDbCore;
using Microsoft.EntityFrameworkCore;
using System;
using Shouldly;
using Moq;
using Xunit;
using AppLayer;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.Data.Sqlite;
using System.Linq;
using JobFindingModels;
using System.Threading.Tasks;

namespace Tests
{
    public class JobFindingDbContextTests : IDisposable
    {
        private DbContextOptions<JobFindingDbContext> _options;
        private static MapperConfiguration _mapperConfig;
        private static IMapper _mapper;
        private JobFindingDbContext _dbContext;
        private IJobFindingDatabaseRepo _dbRepo;

        private const string JOB1_TITLE = "job 1 title";
        private const string JOB2_TITLE = "job 2 title";

        private const string CATEGORY1_NAME = "category 1 name";
        private const string CATEGORY2_NAME = "category 2 name";

        private const string JOB_TYPE1_NAME = "job type 1 name";
        private const string JOB_TYPE2_NAME = "job type 2 name";

        private const string COMPANY1_NAME = "company 1 name";
        private const string COMPANY2_NAME = "company 2 name";

        private const string LOCATION1 = "location 1";
        private const string LOCATION2 = "location 2";

        private const string DESCRIPTION1 = "description 1";
        private const string DESCRIPTION2 = "description 2";

        private const string JOB3_TITLE = "job 3 title";
        private const string DESCRIPTION3 = "description 3";

        public JobFindingDbContextTests()
        {
            SetupOptions();
            SetupSql();
            BuildDefaults();
        }
        private void SetupOptions()
        {
            _options = new DbContextOptionsBuilder<JobFindingDbContext>()
               .UseInMemoryDatabase(databaseName: "JobFindingTest")
               .Options;

            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(JobMapper));

            _mapperConfig = new MapperConfiguration(c => c.AddProfile<JobMapper>());
            _mapperConfig.AssertConfigurationIsValid();
            _mapper = _mapperConfig.CreateMapper();


        }
        private void SetupSql()
        {
            _dbContext = new JobFindingDbContext(_options);
            _dbContext.Database.EnsureCreated();
        }
       
        private void BuildDefaults()
        {
            
            var job1Details = _dbContext.Jobs.SingleOrDefault(x => x.Title.Equals(JOB1_TITLE));
            var job2Details = _dbContext.Jobs.SingleOrDefault(x => x.Title.Equals(JOB2_TITLE));


            if (job1Details != null && job2Details != null)
            {
                return;
            }

            var category1 = new JobCategory { Name = CATEGORY1_NAME, IsActive = true, IsDeleted = false };
            var category2 = new JobCategory { Name = CATEGORY2_NAME, IsActive = true, IsDeleted = false };

            _dbContext.Categories.Add(category1);
            _dbContext.SaveChanges();

            _dbContext.Categories.Add(category2);
            _dbContext.SaveChanges();

            var jobType1 = new JobType { Type = JOB_TYPE1_NAME };
            var jobType2 = new JobType { Type = JOB_TYPE2_NAME };

            _dbContext.JobTypes.AddRange(jobType1);
            _dbContext.SaveChanges();

            _dbContext.JobTypes.AddRange(jobType2);
            _dbContext.SaveChanges();

            var company1 = new Company { Name = COMPANY1_NAME, IsActive = true, IsDeleted = false };
            var company2 = new Company { Name = COMPANY2_NAME, IsActive = true, IsDeleted = false };

            _dbContext.Companies.AddRange(company1);
            _dbContext.SaveChanges();

            _dbContext.Companies.AddRange(company2);
            _dbContext.SaveChanges();

            var job1 = new Job
            {
                CategoryId = category1.Id,
                JobTypeId = jobType1.Id,
                CompanyId = company1.Id,
                Title = JOB1_TITLE,
                Location = LOCATION1,
                Description = DESCRIPTION1,
                IsBookmarked = false,
            };

            var job2 = new Job
            {
                CategoryId = category2.Id,
                JobTypeId = jobType2.Id,
                CompanyId = company2.Id,
                Title = JOB2_TITLE,
                Location = LOCATION2,
                Description = DESCRIPTION2,
                IsBookmarked = false,
            };

            _dbContext.Jobs.Add(job1);
            _dbContext.SaveChanges();

            _dbContext.Jobs.Add(job2);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetJobsForListing_ShoudBeOK()
        {
           
            //act
            _dbRepo = new JobFindingDatabaseRepo(_dbContext, _mapper);
            var items = await _dbRepo.GetJobsForListing();
            //assert
            items.ShouldNotBeNull();
            items.Count.ShouldBe(2);
            var first = items.Where(x=>x.Id == 1).SingleOrDefault();
            first.Title.ShouldBe(JOB1_TITLE);
            first.CompanyName.ShouldBe(COMPANY1_NAME);
            first.Location.ShouldBe(LOCATION1);
            first.IsBookmarked.ShouldBe(false);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task MarkAsBookmarked_ShouldChangeIsBookmarkedState(int id)
        {
            _dbRepo = new JobFindingDatabaseRepo(_dbContext, _mapper);
            var itemsBefore = await _dbRepo.GetJobsForListing();
            await _dbRepo.MarkAsBookmarked(id);
            var itemsAfter = await _dbRepo.GetJobsForListing();

            itemsAfter.Where(x => x.Id == id).SingleOrDefault().IsBookmarked.ShouldBe(!itemsBefore.Where(x => x.Id == id).SingleOrDefault().IsBookmarked);
        }

        [Fact]
        public async Task AddJob_GivenNewJobObject_ShouldBeAddedSuccessFully()
        {
            var job = new Job
            {
                CategoryId = 1,
                JobTypeId = 1,
                CompanyId = 1,
                Title = JOB3_TITLE,
                Location = LOCATION1,
                Description = DESCRIPTION3,
                IsBookmarked = false,
            };

            _dbRepo = new JobFindingDatabaseRepo(_dbContext, _mapper);
            await _dbRepo.AddJob(job);
            _dbContext.Jobs.Count().ShouldBe(3);
            _dbContext.Jobs.Where(x=>x.Id == 3).SingleOrDefault().Title.ShouldBe(JOB3_TITLE);
            _dbContext.Jobs.Where(x => x.Id == 3).SingleOrDefault().Description.ShouldBe(DESCRIPTION3);
        }

    }
}
