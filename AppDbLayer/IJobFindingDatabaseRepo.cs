using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFindingModels;
using JobFindingModels.DTOs;

namespace AppDbLayer
{
    public interface IJobFindingDatabaseRepo : IJobFindingDatabaseRepoReadOnly, IJobFindingDatabaseRepoWriteOnly
    {
    }
    public interface IJobFindingDatabaseRepoReadOnly
    {
        Task<List<GetJobsForListingDto>> GetJobsForListing();
        Task<GetJobByIdDto> GetJobById(int id);
    }
    public interface IJobFindingDatabaseRepoWriteOnly
    {

    }
}
