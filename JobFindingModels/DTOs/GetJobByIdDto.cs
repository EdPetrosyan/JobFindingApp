using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFindingModels.DTOs
{
    public class GetJobByIdDto: GetJobsForListingDto
    {
        public string JobType { get; set; } = "";
        public string JobDescription { get; set; } = "";
        public string JobCategory { get; set; } = "";

    }
}
