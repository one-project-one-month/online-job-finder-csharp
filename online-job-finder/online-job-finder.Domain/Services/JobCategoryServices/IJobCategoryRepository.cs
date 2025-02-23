using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Domain.Services.JobCategoryServices;

public interface IJobCategoryRepository
{
    JobCategoryViewModels CreateJobCategory(JobCategoryViewModels models);

    List<JobCategoryViewModels> GetJobCategories();

    JobCategoryViewModels? GetJobCategory(string id);

    JobCategoryViewModels? UpdateJobCategory(string id, JobCategoryViewModels models);

    JobCategoryViewModels? PatchJobCategory(string id, JobCategoryViewModels models);

    bool? DeleteJobCategory(string id);
}
