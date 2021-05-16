using AppDbCore;
using AppLayer.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobFindingModels;
using JobFindingModels.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AppLayer
{
    public class JobFindingDatabaseRepo : IJobFindingDatabaseRepo
    {
        private readonly JobFindingDbContext _context;
        private readonly IMapper _mapper;

        public JobFindingDatabaseRepo(JobFindingDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<GetJobsForListingDto>> GetJobsForListing()
        {
            return await _context.Jobs
                .AsNoTracking()
                .Include(x => x.Company)
                .Where(x => x.IsActive == true)
                .ProjectTo<GetJobsForListingDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<GetJobByIdDto> GetJobById(int id)
        {
            return await _context.Jobs
                .AsNoTracking()
                .Include(x => x.Company)
                .Include(x => x.Category)
                .Include(x => x.JobType)
                .Where(x => x.Id == id)
                .ProjectTo<GetJobByIdDto>(_mapper.ConfigurationProvider)
                .FirstAsync();
        }

        public async Task<List<GetJobsForListingDto>> SearchJob(string input)
        {
            return await _context.Jobs
                .AsNoTracking()
                .Include(x => x.Company)
                .Where(x => x.IsActive == true && (EF.Functions.Like(x.Title, $"%{input}%")
                            || EF.Functions.Like(x.Description, $"%{input}%")))

                .ProjectTo<GetJobsForListingDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<GetCategoriesDto>> GetJobCategories()
        {

            return await _context.Categories
                .AsNoTracking()
                .ProjectTo<GetCategoriesDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<JobType>> GetJobTypes()
        {
            return await _context.JobTypes
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task<List<string>> GetLocations()
        {
            return await _context.Jobs
                .AsNoTracking()
                .GroupBy(x => x.Location)
                .Select(x => x.Key)
                .ToListAsync();
        }

        public async Task<List<GetJobsForListingDto>> GetFilteredList(IList<int> categories, IList<int> types, IList<string> locations)
        {
            var result = _context.Jobs
                .AsNoTracking()
                .Include(x => x.Company)
                .Include(x => x.JobType)
                .Where(x => x.IsActive == true)
                .AsQueryable();
            if (categories.Count > 0)
            {
                result = result.Where(x => categories.Contains(x.CategoryId.Value));
            }
            if (types.Count > 0)
            {
                result = result.Where(x => types.Contains(x.JobTypeId.Value));
            }
            if (locations.Count > 0)
            {
                result = result.Where(x => locations.Contains(x.Location));
            }
            return await result
                .ProjectTo<GetJobsForListingDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task MarkAsBookmarked(int id)
        {
            var entity = await GetJobForUpdate(id);
            if (entity == null)
            {
                throw new AppException(HttpStatusCode.NotFound, "Entity could not be loaded.");
            }
            entity.IsBookmarked = !entity.IsBookmarked;
            _context.Attach<Job>(entity);
            _context.Entry(entity).Property(x => x.IsBookmarked).IsModified = true;
            await _context.SaveChangesAsync();
        }

        private async Task<Job> GetJobForUpdate(int id)
        {
            return await _context.Jobs.FindAsync(id);
        }

        public async Task AddJob(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Company>> GetCompanies()
        {
            return await _context.Companies
                .AsNoTracking().ToListAsync();
        }

        public async Task DeleteJob(int id)
        {
            var entity = await GetJobForUpdate(id);
            if (entity == null)
            {
                throw new AppException(HttpStatusCode.NotFound, "Entity could not be loaded.");
            }
            entity.IsActive = false;
            entity.IsDeleted = true;
            _context.Attach<Job>(entity);
            _context.Entry(entity).Property(x => x.IsActive).IsModified = true;
            _context.Entry(entity).Property(x => x.IsDeleted).IsModified = true;
            await _context.SaveChangesAsync();
        }

    }
}
