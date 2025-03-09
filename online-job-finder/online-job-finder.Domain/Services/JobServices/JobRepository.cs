using Microsoft.EntityFrameworkCore;
using Mysqlx;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.Services.JobServices
{
    public class JobRepository
    {
        private readonly AppDbContext _context;
        static int JobIdCounter = 1000;
        public JobRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<JobsViewModels> GetJobsAsync()
        {            
            var model = _context.TblJobs
                        .AsNoTracking()
                        .Include(x=>x.CompanyProfiles)
                        .Include(x=> x.JobCategories)
                        .Include(x=>x.Location)
                        .OrderBy(x => x.Version)  
                        .ToList();           

            var jobViewModels = model.Select(JobsViewModelsMapping).ToList();
            return jobViewModels;

        }
        public JobsViewModels? GetJobById(string id)
        {
            var viewModel = new JobsViewModels();
            try
            {
                var model = _context.TblJobs.AsNoTracking()
                             .Include(x => x.CompanyProfiles)
                             .Include(x => x.JobCategories)
                             .Include(x => x.Location)
                             .FirstOrDefault(x => x.JobsId.ToString() == id
                            && x.IsDelete == false);

                if (model is null) { return null; }

                viewModel = JobsViewModelsMapping(model);
                return viewModel;

            }
            catch (Exception ex)
            {
                return null;
            }               

        }       
        public object CreateJob(JobsViewModels model)
        {
            model.Version += 1;
            model.UpdatedAt = null;

            var existingCompany = _context.TblCompanyProfiles.FirstOrDefault(x => x.CompanyName == model.CompanyName);
            var existingJobCategory = _context.TblJobCategories.FirstOrDefault(x => x.Industry == model.Industry);
            var existingLocation = _context.TblLocations.FirstOrDefault(x => x.LocationName == model.LocationName);
            
            var activeJobPostCount = updatedJobPostCounter(model.CompanyName);

            var application = _context.TblApplications.FirstOrDefault(x => x.JobsId == model.JobsId);
            var jobSkill = _context.TblJobSkills.FirstOrDefault(x => x.JobsId == model.JobsId);
            var savedJob = _context.TblSavedJobs.FirstOrDefault(x => x.JobsId == model.JobsId);

            TblJob job = Change(model);
            if (existingCompany != null)
            {
                job.CompanyProfilesId = existingCompany.CompanyProfilesId;
                job.CompanyProfiles = existingCompany;
            }
            if (existingJobCategory != null)
            {
                job.JobCategoriesId = existingJobCategory.JobCategoriesId;
                job.JobCategories = existingJobCategory;
            }
            if (existingLocation != null)
            {
                job.LocationId = existingLocation.LocationId;
                job.Location = existingLocation;
            }  
            job.Title = model.Title;
            job.Type = model.Type;
            job.Description = model.Description;
            job.Requirements = model.Requirements;
            job.NumOfPosts = activeJobPostCount+1;
            job.Salary = model.Salary;
            job.Address = model.Address;
            job.Status = model.Status;        
            
            job.CreatedAt = DateTime.UtcNow;
            job.UpdatedAt = DateTime.UtcNow;
            job.IsDelete = false;
            if(application != null)
            {
                job.TblApplications.Add(application);
            }
            if (jobSkill != null)
            {
                job.TblJobSkills.Add(jobSkill);
            }
            if (savedJob != null)
            {
                job.TblSavedJobs.Add(savedJob);
            }  
            _context.TblJobs.Add(job);
            _context.SaveChanges();
            return model;
        }
        //Still in progress....
        public JobsViewModels? PatchJob(string id, JobsViewModels model)
        {
            var item = _context.TblJobs
                       .Include(x => x.CompanyProfiles)
                       .Include(x => x.JobCategories)
                       .Include(x => x.Location)
                       .AsNoTracking()
                       .FirstOrDefault(x => x.JobsId.ToString() == id
                       && x.IsDelete == false);

            if (item is null) { return null; }

            #region Patch Method Validation Conditions
            if (!string.IsNullOrEmpty(model.CompanyName))
                item.CompanyProfiles.CompanyName = model.CompanyName;

            //if (!string.IsNullOrEmpty(model.JobCategoryId))
            //    item.JobCategoryId = model.JobCategoryId;
            if (!string.IsNullOrEmpty(model.Title))
                item.Title = model.Title;
            //if (!string.IsNullOrEmpty(model.LocationId))
            //    item.LocationId = model.LocationId;
            if (model.Type != model.Type)
                item.Type = model.Type;
            if (!string.IsNullOrEmpty(model.Description))
                item.Description = model.Description;
            if (!string.IsNullOrEmpty(model.Requirements))
                item.Requirements = model.Requirements;
            //if (item.NumberOfPosts != 0)
            //    item.NumOfPosts = model.NumberOfPosts;
            if (item.Salary != 0)
                item.Salary = model.Salary;
            if (!string.IsNullOrEmpty(model.Address))
                item.Address = model.Address;
            if (model.Status != model.Status)
                item.Status = model.Status;
            #endregion           

            
            item.Version += 1;
            item.UpdatedAt = DateTime.UtcNow;

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();

            model = JobsViewModelsMapping(item);

            return model;
        }

        public bool? DeleteJob(string id)
        {
            var item = _context.TblJobs
                      .Include(x => x.CompanyProfiles)
                      .Include(x => x.JobCategories)
                      .Include(x => x.Location)
                      .AsNoTracking()
                      .FirstOrDefault(x => x.JobsId.ToString() == id
                      && x.IsDelete == false);
            if (item is null)
            {
                return null;
            }

            item.Version += 1;
            item.UpdatedAt = DateTime.UtcNow;
            item.IsDelete = true;

            _context.Entry(item).State = EntityState.Modified;
            var result = _context.SaveChanges();

            return result > 0;
        }

        public object UpdateJob(string id, JobsViewModels models)
        {
            throw new NotImplementedException();
        }
        private int updatedJobPostCounter(string CompanyName)
        {
            int NumOfPost = _context.TblJobs.Where(x => x.CompanyProfiles.CompanyName == CompanyName && x.IsDelete == false).Count();

            NumOfPost += 1;
            //update all NumOfPosts in the database
            return NumOfPost;
        }

        private static JobsViewModels JobsViewModelsMapping(TblJob jobs)
        {
            return new JobsViewModels
            {
                JobsId = jobs.JobsId,
                Title = jobs.Title,
                CompanyName = jobs.CompanyProfiles.CompanyName,
                LocationName = jobs.Location.LocationName,
                Type = jobs.Type,
                Description = jobs.Description,
                Requirements = jobs.Requirements,
                NumOfPosts = jobs.NumOfPosts,
                Salary = jobs.Salary,
                Address = jobs.Address,
                Status = jobs.Status,
                Version = jobs.Version,
                //PostedAt = jobs.PostedAt,
                Industry = jobs.JobCategories.Industry,
                UpdatedAt = jobs.UpdatedAt,
                CreatedAt = jobs.CreatedAt
            };
        }
        private static TblJob Change(JobsViewModels jobs)
        {

            return new TblJob()
            {
                JobsId = Guid.NewGuid(),
                Title = jobs.Title,
                Type = jobs.Type,
                Description = jobs.Description,
                Requirements = jobs.Requirements,
                NumOfPosts = jobs.NumOfPosts,
                Salary = jobs.Salary,
                Address = jobs.Address,
                Status = jobs.Status,
                Version = jobs.Version,
                //PostedAt = jobs.PostedAt,                         
                UpdatedAt = jobs.UpdatedAt,
                CreatedAt = jobs.CreatedAt,
                IsDelete = false

            };
        }        
    }
}
