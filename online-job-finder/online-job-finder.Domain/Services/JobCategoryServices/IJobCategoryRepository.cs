namespace online_job_finder.Domain.Services.JobCategoryServices;

public interface IJobCategoryRepository
{
    Task<JobCategoryViewModels> CreateJobCategory(JobCategoryViewModels models);

    Task<List<JobCategoryViewModels>> GetJobCategories();

    Task<JobCategoryViewModels?> GetJobCategory(string id);

    Task<JobCategoryViewModels?> UpdateJobCategory(string id, JobCategoryViewModels models);

    Task<JobCategoryViewModels?> PatchJobCategory(string id, JobCategoryViewModels models);

    Task<bool?> DeleteJobCategory(string id);
}
