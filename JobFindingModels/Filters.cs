using JobFindingModels.DTOs;
using System.Collections.Generic;

namespace JobFindingModels
{
    public class Filters
    {
        public IList<GetCategoriesDto> Categories { get; set; }
        public IList<JobType> JobTypes { get; set; }
        public IList<string> Locations { get; set; }
    }
}
