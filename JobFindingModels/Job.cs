using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobFindingModels
{
    public class Job : FullAuditModel
    {
        [Required]
        [StringLength(JobFindingModelConstants.MAX_TITlE_LENGTH)]
        public string Title { get; set; }

        [Required]
        [StringLength(JobFindingModelConstants.MAX_DESCRIPTION_LENGTH)]
        public string Description { get; set; }

        [Required]
        [StringLength(JobFindingModelConstants.MAX_LOCATION_LENGTH)]
        public string Location { get; set; }

        [DefaultValue(false)]
        public bool IsBookmarked { get; set; }
        public virtual JobCategory Category { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        public virtual Company Company { get; set; }
        [Required]
        public int? CompanyId { get; set; }
        public virtual JobType JobType { get; set; }
        [Required]
        public int? JobTypeId { get; set; }
    }
}
