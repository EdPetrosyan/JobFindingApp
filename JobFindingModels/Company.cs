using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFindingModels
{
    public class Company : FullAuditModel
    {
        [Required]
        [StringLength(JobFindingModelConstants.MAX_NAME_LENGTH)]
        public string Name { get; set; }
        public virtual IList<Job> Jobs { get; set; }
    }
}
