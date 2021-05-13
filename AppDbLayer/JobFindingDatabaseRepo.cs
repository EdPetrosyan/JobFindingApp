using AppDbCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobFindingModels;
using JobFindingModels.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLayer
{
    public class JobFindingDatabaseRepo : IJobFindingDatabaseRepo
    {
        private readonly JobFindingDbContext _context;
        private readonly IMapper _mapper;

        public JobFindingDatabaseRepo(JobFindingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<GetJobsForListingDto>> GetJobsForListing()
        {
            return await _context.Jobs
                .Include(x => x.Company)
                .ProjectTo<GetJobsForListingDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<GetJobByIdDto> GetJobById(int id)
        {
            //SqlParameter idParam = new("id", id);
            return await _context.Jobs
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
                .Include(x => x.Company)
                .Where(x => EF.Functions.Like(x.Title, $"%{input}%") 
                            || EF.Functions.Like(x.Description, $"%{input}%"))
                .ProjectTo<GetJobsForListingDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    } 
}
