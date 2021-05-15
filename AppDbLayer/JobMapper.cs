using AutoMapper;
using JobFindingModels;
using JobFindingModels.DTOs;

namespace AppLayer
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
            CreateMap<Job, GetJobByIdDto>()
                .ForMember(x => x.CompanyName, opt => opt.MapFrom(y => y.Company.Name))
                .ForMember(x => x.JobCategory, opt => opt.MapFrom(y => y.Category.Name))
                .ForMember(x => x.JobType, opt => opt.MapFrom(y => y.JobType.Type))
                .ForMember(x => x.JobDescription, opt => opt.MapFrom(y => y.Description));
            CreateMap<JobCategory, GetCategoriesDto>();
        }
    }
}
