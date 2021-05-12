using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFindingModels
{
    public class Company : FullAuditModel
    {
        [Required]
        [StringLength(JobFindingModelConstants.MAX_NAME_LENGTH)]
        public string Name { get; set; }
    }
}
