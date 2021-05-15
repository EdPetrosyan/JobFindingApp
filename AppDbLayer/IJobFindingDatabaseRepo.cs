using JobFindingModels;
using JobFindingModels.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<List<GetJobsForListingDto>> GetFilteredList(IList<int> categories, IList<int> types, IList<string> locations);
        Task<List<string>> GetLocations();
        Task<List<Company>> GetCompanies();

    }
    public interface IJobFindingDatabaseRepoWriteOnly
    {
        Task MarkAsBookmarked(int id);
        Task AddJob(Job job);
        Task DeleteJob(int id);
    }
}
