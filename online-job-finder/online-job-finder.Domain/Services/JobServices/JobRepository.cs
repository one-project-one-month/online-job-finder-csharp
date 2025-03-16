using Microsoft.EntityFrameworkCore;
using Mysqlx;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.Services.JobServices
{
    public class JobRepository
    {
        private readonly AppDbContext _context;
        public JobRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<JobsViewModels> GetJobsAsync()
        {
            var model = _context.TblJobs
                        .AsNoTracking()
                        .Include(x => x.CompanyProfiles)
                        .Include(x => x.JobCategories)
                        .Include(x => x.Location)
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
        public JobsViewModels CreateJob(JobsViewModels model)
        {
            model.Version += 1;
            model.UpdatedAt = null;

            var existingCompany = _context.TblCompanyProfiles.FirstOrDefault(x => x.CompanyName == model.CompanyName);
            var existingJobCategory = _context.TblJobCategories.FirstOrDefault(x => x.Industry == model.Industry);
            var existingLocation = _context.TblLocations.FirstOrDefault(x => x.LocationName == model.LocationName);            
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
            job.Salary = model.Salary;
            job.Address = model.Address;
            job.Status = model.Status;

            job.CreatedAt = DateTime.UtcNow;
            job.UpdatedAt = DateTime.UtcNow;
            job.IsDelete = false;

            _context.TblJobs.Add(job);
            _context.SaveChanges();

            return JobsViewModelsMapping(job);
        }
        public JobsViewModels? UpdateJob(string id, JobsViewModels model)
        {
            var item = _context.TblJobs
                       .Include(x => x.CompanyProfiles)
                       .Include(x => x.JobCategories)
                       .Include(x => x.Location)
                       .AsNoTracking()
                       .FirstOrDefault(x => x.JobsId.ToString() == id);

            if (item is null) { return null; }

            #region Modified data Validation Conditions
            //Assume CompanyName,LocationName are already registered!
            if (!string.IsNullOrEmpty(model.CompanyName))
            {
                var requestCompany = _context.TblCompanyProfiles.FirstOrDefault(x => x.CompanyName == model.CompanyName && model.IsDelete == false);
                if (requestCompany != null)
                {
                    item.CompanyProfilesId = requestCompany.CompanyProfilesId;
                    item.CompanyProfiles = requestCompany;
                }

            }
            if (!string.IsNullOrEmpty(model.LocationName))
            {
                var requestLocation = _context.TblLocations.FirstOrDefault(x => x.LocationName == model.LocationName && model.IsDelete == false);
                if (requestLocation != null)
                {
                    item.LocationId = requestLocation.LocationId;
                    item.Location = requestLocation;
                }
            }

            if (item.Title != model.Title)
                item.Title = model.Title;
            if (item.Type != model.Type)
                item.Type = model.Type;
            if (!string.IsNullOrEmpty(model.Description))
                item.Description = model.Description;
            if (!string.IsNullOrEmpty(model.Requirements))
                item.Requirements = model.Requirements;
            if (item.Salary != model.Salary)
                item.Salary = model.Salary;
            if (!string.IsNullOrEmpty(model.Address))
                item.Address = model.Address;
            if (item.Status != model.Status)
                item.Status = model.Status;
            if (item.IsDelete != model.IsDelete)
                item.IsDelete = model.IsDelete;
            #endregion

            item.Version += 1;
            item.UpdatedAt = DateTime.UtcNow;

            _context.Attach(item);
            // Set the entity state to Modified
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();

            model = JobsViewModelsMapping(item);

            return model;
        }


        public JobsViewModels? PatchJob(string id, JobsViewModels model)
        {
            var item = _context.TblJobs
                      .Include(x => x.CompanyProfiles)
                      .Include(x => x.JobCategories)
                      .Include(x => x.Location)
                      .AsNoTracking()
                      .FirstOrDefault(x => x.JobsId.ToString() == id);

            if (item is null) { return null; }

            #region Modified data Validation Conditions
            //Assume CompanyName,LocationName are already registered!
            if (!string.IsNullOrEmpty(model.CompanyName))
            {
                var requestCompany = _context.TblCompanyProfiles.FirstOrDefault(x => x.CompanyName == model.CompanyName && model.IsDelete == false);
                if (requestCompany != null)
                {
                    item.CompanyProfilesId = requestCompany.CompanyProfilesId;
                    item.CompanyProfiles = requestCompany;
                }

            }
            if (!string.IsNullOrEmpty(model.LocationName))
            {
                var requestLocation = _context.TblLocations.FirstOrDefault(x => x.LocationName == model.LocationName && model.IsDelete == false);
                if (requestLocation != null)
                {
                    item.LocationId = requestLocation.LocationId;
                    item.Location = requestLocation;
                }
            }

            if (item.Title != model.Title)
                item.Title = model.Title;
            if (item.Type != model.Type)
                item.Type = model.Type;
            if (!string.IsNullOrEmpty(model.Description))
                item.Description = model.Description;
            if (!string.IsNullOrEmpty(model.Requirements))
                item.Requirements = model.Requirements;
            if (item.Salary != model.Salary)
                item.Salary = model.Salary;
            if (!string.IsNullOrEmpty(model.Address))
                item.Address = model.Address;
            if (item.Status != model.Status)
                item.Status = model.Status;
            if (item.IsDelete != model.IsDelete)
                item.IsDelete = model.IsDelete;
            #endregion

            item.Version += 1;
            item.UpdatedAt = DateTime.UtcNow;

            // Attach the entity to the context
            _context.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();

            model = JobsViewModelsMapping(item);

            return model;
        }
        public List<JobsViewModels> GetJobsAsync(JobSearchParameters requestJob)
        {
            var jobs = _context.TblJobs
                .AsNoTracking()
                .Include(x => x.CompanyProfiles)
                .Include(x => x.JobCategories)
                .Include(x => x.Location)
                .OrderBy(x => x.Version)
                .ToList();

            var filteredJobs = jobs.FindAll(job =>
                (requestJob.Q == null || requestJob.Q.Length == 0 || string.IsNullOrWhiteSpace(requestJob.Q[0]) || requestJob.Q.Contains(job.Title)) &&
                (requestJob.Location == null || requestJob.Location.Length == 0 || string.IsNullOrWhiteSpace(requestJob.Location[0]) || requestJob.Location.Contains(job.Location.LocationName)) &&
                (requestJob.Category == null || requestJob.Category.Length == 0 || string.IsNullOrWhiteSpace(requestJob.Category[0]) || requestJob.Category.Contains(job.JobCategories.Industry)) &&
                (requestJob.Type == null || requestJob.Type.Length == 0 || string.IsNullOrWhiteSpace(requestJob.Type[0]) || requestJob.Type.Contains(job.Type))
            );

            var jobViewModels = filteredJobs.Select(JobsViewModelsMapping).ToList();
            return jobViewModels;
        }
        public object applyJob(ApplyJobViewModels requestModel)
        {            
            requestModel.Version += 1;
            requestModel.UpdatedAt = null;

            var checkApplication = _context.TblApplications.AsNoTracking()
                                   .Include(x => x.ApplicantProfiles)
                                   .Include(x => x.Jobs)
                                   .Include(x => x.Resumes)
                                   .FirstOrDefault(x => x.JobsId == requestModel.JobsId
                                   && x.ApplicantProfilesId == requestModel.ApplicantProfilesId
                                   && x.IsDelete == false);
            if (checkApplication != null)
            {
                //already appied
                return null;
            }

            var requestJob = _context.TblJobs.AsNoTracking()
                                        .Include(x => x.CompanyProfiles)
                                        .Include(x => x.JobCategories)
                                        .Include(x => x.Location)
                                        .FirstOrDefault(x => x.JobsId == requestModel.JobsId
                                        && x.Status == "Open" && x.IsDelete == false);
            var requestApplicant = _context.TblApplicantProfiles.AsNoTracking()
                                   .FirstOrDefault(x => x.ApplicantProfilesId == requestModel.ApplicantProfilesId
                                   && x.IsDelete == false);
            var requestResume = _context.TblResumes.AsNoTracking()
                                .FirstOrDefault(x => x.ResumesId == requestModel.ResumesId
                                && x.IsDelete == false);

            if (requestJob == null)
            {
                return null;
            }
            if (requestApplicant == null || requestResume == null)
            {
                return null; // Or handle the error as needed
            }

            //apply job
            var jobApplication = new TblApplication
            {
                ApplicationsId = Guid.NewGuid(),
                JobsId = requestModel.JobsId,
                ApplicantProfilesId = requestModel.ApplicantProfilesId,
                ResumesId = requestModel.ResumesId,
                Status = requestModel.Status,
                Version = requestModel.Version,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDelete = false
            };
            // Attach existing entities to avoid duplicate ID error
            _context.Entry(requestApplicant).State = EntityState.Unchanged;
            _context.Entry(requestJob).State = EntityState.Unchanged;
            _context.Entry(requestResume).State = EntityState.Unchanged;

            // Set navigation properties
            jobApplication.ApplicantProfiles = requestApplicant;
            jobApplication.Jobs = requestJob;
            jobApplication.Resumes = requestResume;
            _context.TblApplications.Add(jobApplication);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return jobApplication;
            }
            else
            {
                // Log or handle the case where no changes were made
                return null;
            }

        }

        public object saveJob(SavedJobViewModels requestModel)
        {
            var checkSavedJob = _context.TblSavedJobs.AsNoTracking()
                                .FirstOrDefault(x => x.JobsId == requestModel.JobsId
                                && x.ApplicantProfilesId == requestModel.ApplicantProfilesId
                                && x.IsDelete == false);
            if (checkSavedJob != null)
            {
                return checkSavedJob;
            }
            else
            {
                requestModel.Version += 1;
                requestModel.UpdatedAt = null;
                var requestJob = _context.TblJobs.AsNoTracking()
                                 .Include(x => x.CompanyProfiles)
                                 .Include(x => x.JobCategories)
                                 .Include(x => x.Location)
                                 .FirstOrDefault(x => x.JobsId == requestModel.JobsId
                                 && x.Status == "Active" && x.IsDelete == false);
                var requestApplicant = _context.TblApplicantProfiles.AsNoTracking()
                                       .FirstOrDefault(x => x.ApplicantProfilesId == requestModel.ApplicantProfilesId
                                       && x.IsDelete == false);
                // Example logic to save the job to the database
                var savedJob = new TblSavedJob
                {
                    SavedJobsId = Guid.NewGuid(),
                    JobsId = requestModel.JobsId,
                    ApplicantProfilesId = requestModel.ApplicantProfilesId,
                    Status = requestModel.Status,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = requestModel.UpdatedAt,
                    IsDelete = false
                };

                _context.TblSavedJobs.Add(savedJob);
                _context.SaveChanges();

                return savedJob;
            }

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
            if (item is not null)
            {
                item.Version += 1;
                item.UpdatedAt = DateTime.UtcNow;
                item.IsDelete = true;               

                _context.Entry(item).State = EntityState.Modified;
                var result = _context.SaveChanges();

                return result > 0;
            }
            else { return null; }
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
                IsDelete = jobs.IsDelete,
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
