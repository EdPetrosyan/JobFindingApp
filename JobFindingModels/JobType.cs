using JobFindingModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFindingModels
{
    public class JobType : IIdentityModel
    {
        public int Id { get ; set ; }
        [StringLength(JobFindingModelConstants.MAX_TITlE_LENGTH)]
        public string Type { get; set; }
        public virtual List<Job> Jobs { get; set; }
    }
}
