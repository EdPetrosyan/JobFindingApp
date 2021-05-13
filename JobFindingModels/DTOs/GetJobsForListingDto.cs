using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JobFindingModels.DTOs
{
    public class GetJobsForListingDto 
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string Location { get; set; } = "";
        public bool IsBookmarked { get; set; } = false;
        public DateTime CreatedDate { get; set; } = default;
    }
}
