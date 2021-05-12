using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFindingModels
{
    public class Job : FullAuditModel
    {
        [StringLength(JobFindingModelConstants.MAX_TITlE_LENGTH)]
        public string Title { get; set; }

        [StringLength(JobFindingModelConstants.MAX_DESCRIPTION_LENGTH)]
        public string Description { get; set; }

        [StringLength(JobFindingModelConstants.MAX_LOCATION_LENGTH)]
        public string Location { get; set; }

        [DefaultValue(false)]
        public bool IsBookmarked { get; set; }
        public virtual JobCategory Category { get; set; }
        public int? CategoryId { get; set; }
        public virtual Company Company { get; set; }
        public int? CompanyId { get; set; }
        public virtual JobType JobType { get; set; }
        public int? JobTypeId { get; set; }
    }
}
