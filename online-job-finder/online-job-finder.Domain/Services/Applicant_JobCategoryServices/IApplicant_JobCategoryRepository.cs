namespace online_job_finder.Domain.Services.Applicant_JobCategoryServices
{
    public interface IApplicant_JobCategoryRepository
    {
        Task<Applicant_JobCategoryViewModels> CreateApplicant_JobCategory(Applicant_JobCategoryViewModels models, string userid);
        Task<List<Applicant_JobCategoryViewModels>> GetApplicant_JobCategories(string userid);
    }
}