using AutoMapper;
using JobFindingModels;
using JobFindingModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDbLayer
{
    public class JobMapper : Profile
    {
        public JobMapper()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Job, GetJobsForListingDto>()
                .ForMember(x => x.CompanyName, opt => opt.MapFrom(y => y.Company.Name));
        }
    }
}
