using JobFindingModels.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFindingModels
{
    public class JobType : IIdentityModel
    {
        public int Id { get; set; }
        [StringLength(JobFindingModelConstants.MAX_TITlE_LENGTH)]
        public string Type { get; set; }
        public virtual List<Job> Jobs { get; set; }
    }
}
