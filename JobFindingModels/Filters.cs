using JobFindingModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFindingModels
{
    public class Filters
    {
        public IList<GetCategoriesDto> Categories { get; set; }
        public IList<JobType> JobTypes { get; set; }
        public IList<string> Locations { get; set; }
    }
}
