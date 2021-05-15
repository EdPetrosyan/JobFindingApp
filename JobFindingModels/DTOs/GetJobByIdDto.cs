namespace JobFindingModels.DTOs
{
    public class GetJobByIdDto : GetJobsForListingDto
    {
        public string JobType { get; set; } = "";
        public string JobDescription { get; set; } = "";
        public string JobCategory { get; set; } = "";

    }
}
