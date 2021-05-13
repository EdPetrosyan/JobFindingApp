using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFindingModels
{
    public class JobsFilter
    {
        public IList<int> CategoryIds { get; set; }
        public IList<int> TypeIds { get; set; }
    }
}
