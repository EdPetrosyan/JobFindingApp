using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFindingModels
{
    public class Job : FullAuditModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public virtual JobCategory Category { get; set; }
        public int? CategoryId { get; set; }
        public virtual Company Company { get; set; }
        public int? CompanyId { get; set; }
    }
}
