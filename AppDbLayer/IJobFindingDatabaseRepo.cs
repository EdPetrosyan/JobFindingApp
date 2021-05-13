using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFindingModels;
using JobFindingModels.DTOs;

namespace AppLayer
{
    public interface IJobFindingDatabaseRepo : IJobFindingDatabaseRepoReadOnly, IJobFindingDatabaseRepoWriteOnly
    {
    }
    public interface IJobFindingDatabaseRepoReadOnly
    {
        Task<List<GetJobsForListingDto>> GetJobsForListing();
        Task<GetJobByIdDto> GetJobById(int id);
        Task<List<GetJobsForListingDto>> SearchJob(string input);
        Task<List<GetCategoriesDto>> GetJobCategories();
        Task<List<JobType>> GetJobTypes();
        Task<List<GetJobsForListingDto>> GetFilteredList(IList<int> categories, IList<int> types);

    }
    public interface IJobFindingDatabaseRepoWriteOnly
    {

    }
}
