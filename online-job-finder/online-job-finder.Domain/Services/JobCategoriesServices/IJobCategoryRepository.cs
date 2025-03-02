using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels.JobCategories;
using online_job_finder.Domain.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.Services.JobCategoriesServices
{
    public interface IJobCategoryRepository
    {
        TblJobCategory CreateJobCategory(JobsCategoriesViewModels jobcateogry);

        List<JobsCategoriesViewModels> GetJobCategories();
        
        JobsCategoriesViewModels? GetJobCategory(string id);

        JobsCategoriesViewModels? UpdateJobCategory(string id, JobsCategoriesViewModels jobCategories);

        JobsCategoriesViewModels? PatchJobCategory(string id, JobsCategoriesViewModels jobCategories);

        bool? DeleteJobCategory(string id);
    }
}
