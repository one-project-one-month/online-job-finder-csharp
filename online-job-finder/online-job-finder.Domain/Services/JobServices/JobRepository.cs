using Microsoft.EntityFrameworkCore;
using Mysqlx;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

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

            var company = _context.TblCompanyProfiles.FirstOrDefault(x => x.CompanyProfilesId == model.CompanyProfilesId && model.IsDelete == false);
            var location = _context.TblLocations.FirstOrDefault(x => x.LocationId == model.LocationId && model.IsDelete == false);
            var jobCategory = _context.TblJobCategories.FirstOrDefault(x => x.JobCategoriesId == model.JobCategoryId && model.IsDelete == false);
            if (company != null && location != null && jobCategory != null)
            {
                var job = Change(model);
                _context.TblJobs.Add(job);
                _context.SaveChanges();
                return JobsViewModelsMapping(job);
            }
            else
            {
                return null;
            }
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
            if (item.CompanyProfilesId != model.CompanyProfilesId)
            {
                var requestCompany = _context.TblCompanyProfiles.FirstOrDefault(x => x.CompanyProfilesId == model.CompanyProfilesId && model.IsDelete == false);

                if (requestCompany is null) { return null; }

                item.CompanyProfilesId = requestCompany.CompanyProfilesId;
                item.CompanyProfiles = requestCompany;

            }
            if (item.LocationId != model.LocationId)
            {
                var requestLocation = _context.TblLocations.FirstOrDefault(x => x.LocationId == model.LocationId && model.IsDelete == false);
                if (requestLocation is null) { return null; }
                item.LocationId = requestLocation.LocationId;
                item.Location = requestLocation;
            }


            if (item.JobCategoriesId != model.JobCategoryId)
            {
                var requestCategory = _context.TblJobCategories.FirstOrDefault(x => x.JobCategoriesId == model.JobCategoryId && model.IsDelete == false);

                if (requestCategory is null) { return null; }
                item.JobCategoriesId = requestCategory.JobCategoriesId;
                item.JobCategories = requestCategory;

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
            if (item.CompanyProfilesId != model.CompanyProfilesId)
            {
                var requestCompany = _context.TblCompanyProfiles.FirstOrDefault(x => x.CompanyProfilesId == model.CompanyProfilesId && model.IsDelete == false);
                if (requestCompany != null)
                {
                    item.CompanyProfilesId = requestCompany.CompanyProfilesId;
                    item.CompanyProfiles = requestCompany;
                }
                else { return null; }

            }
            if (item.LocationId != model.LocationId)
            {
                var requestLocation = _context.TblLocations.FirstOrDefault(x => x.LocationId == model.LocationId && model.IsDelete == false);
                if (requestLocation != null)
                {
                    item.LocationId = requestLocation.LocationId;
                    item.Location = requestLocation;
                }
                else { return null; }
            }
            if (item.JobCategoriesId != model.JobCategoryId)
            {
                var requestCategory = _context.TblJobCategories.FirstOrDefault(x => x.JobCategoriesId == model.JobCategoryId && model.IsDelete == false);
                if (requestCategory != null)
                {
                    item.JobCategoriesId = requestCategory.JobCategoriesId;
                    item.JobCategories = requestCategory;
                }
                else { return null; }
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
        public List<JobsViewModels> GetJobsAsync(JobSearchParameters searchParam)
        {
            var jobs = _context.TblJobs
                       .AsNoTracking()
                       .Include(x => x.CompanyProfiles)
                       .Include(x => x.JobCategories)
                       .Include(x => x.Location)
                       .Where(x => x.Status == "Open" && x.IsDelete == false)
                       .OrderBy(x => x.Version).AsQueryable();

            if (!string.IsNullOrEmpty(searchParam.Title))
            {
                jobs = jobs.Where(x => x.Title.Contains(searchParam.Title));
            }
            if (!string.IsNullOrEmpty(searchParam.Location))
            {
                jobs = jobs.Where(x => x.Location.LocationName.Contains(searchParam.Location));
            }
            if (!string.IsNullOrEmpty(searchParam.Industry))
            {
                jobs = jobs.Where(x => x.JobCategories.Industry.Contains(searchParam.Industry));
            }
            if (!string.IsNullOrEmpty(searchParam.Type))
            {
                jobs = jobs.Where(x => x.Type.Contains(searchParam.Type));
            }
            var searchResult = jobs.ToList();
            if (searchResult.Count < 0) { return null; }
            var resultJobs = searchResult.Select(JobsViewModelsMapping).ToList();
            return resultJobs;
        }

        public bool? applyJob(ApplyJobViewModels requestModel)
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
                return null;    //already appied
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
                return null;
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
            return result > 0;          

        }

        public bool? saveJob(SavedJobViewModel requestModel)
        {
            var checkSavedJob = _context.TblSavedJobs.AsNoTracking()
                                .FirstOrDefault(x => x.JobsId == requestModel.JobsId
                                && x.ApplicantProfilesId == requestModel.ApplicantProfilesId
                                && x.IsDelete == false);
            if (checkSavedJob != null)
            {
                return null; //already savedJob
            }
            requestModel.Version += 1;
            requestModel.UpdatedAt = null;
            var requestJob = _context.TblJobs.AsNoTracking()
                             .Include(x => x.CompanyProfiles)
                             .Include(x => x.JobCategories)
                             .Include(x => x.Location)
                             .FirstOrDefault(x => x.JobsId == requestModel.JobsId
                             && x.Status == "Open" && x.IsDelete == false);
            var requestApplicant = _context.TblApplicantProfiles.AsNoTracking()
                                   .FirstOrDefault(x => x.ApplicantProfilesId == requestModel.ApplicantProfilesId
                                   && x.IsDelete == false);
            if(requestApplicant == null || requestJob == null)
            {
                return null;
            }
            var savedJob = new TblSavedJob
            {
                SavedJobsId = Guid.NewGuid(),
                JobsId = requestJob.JobsId,
                ApplicantProfilesId = requestApplicant.ApplicantProfilesId,  
                Status = requestModel.Status,
                Version = requestModel.Version,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDelete = false
            };

            _context.TblSavedJobs.Add(savedJob);
            var result = _context.SaveChanges();
            return result > 0; 
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

        public List<ApplyJobViewModels> GetAppliedJobs(string profileId)
        {
            var appliedJobs = _context.TblApplications
                              .AsNoTracking()
                              .Include(x => x.Jobs)
                              .Include(x => x.ApplicantProfiles)
                              .Include(x => x.Resumes)
                              .Where(x => x.ApplicantProfilesId.ToString() == profileId
                              && x.IsDelete == false)
                              .OrderBy(x => x.Version)
                              .ToList();
            var result = appliedJobs.Select(ApplyJobViewModelsMapping).ToList();
            return result;
        }

        public List<SavedJobViewModel> GetSavedJobs(string profileId)
        {
            var savedJobs = _context.TblSavedJobs
                            .AsNoTracking()
                            .Where(x => x.ApplicantProfilesId.ToString() == profileId
                            && x.IsDelete == false)
                            .OrderBy(x => x.Version)
                            .ToList();
            var result = savedJobs.Select(SavedJobViewModelMapping).ToList();
            return result;
        }

        private static JobsViewModels JobsViewModelsMapping(TblJob jobs)
        {
            return new JobsViewModels
            {
                JobsId = jobs.JobsId,
                Title = jobs.Title,
                CompanyProfilesId = jobs.CompanyProfilesId,
                LocationId = jobs.LocationId,
                JobCategoryId = jobs.JobCategoriesId,
                Type = jobs.Type,
                Description = jobs.Description,
                Requirements = jobs.Requirements,
                NumOfPosts = jobs.NumOfPosts,
                Salary = jobs.Salary,
                Address = jobs.Address,
                Status = jobs.Status,
                Version = jobs.Version,
                Industry = jobs.JobCategories.Industry,
                IsDelete = jobs.IsDelete,
                CreatedAt = jobs.CreatedAt,
                UpdatedAt = jobs.UpdatedAt
            };
        }
        private static TblJob Change(JobsViewModels jobs)
        {

            return new TblJob()
            {
                JobsId = Guid.NewGuid(),
                CompanyProfilesId = jobs.CompanyProfilesId,
                LocationId = jobs.LocationId,
                JobCategoriesId = jobs.JobCategoryId,
                Title = jobs.Title,
                Type = jobs.Type,
                Description = jobs.Description,
                Requirements = jobs.Requirements,
                NumOfPosts = jobs.NumOfPosts,
                Salary = jobs.Salary,
                Address = jobs.Address,
                Status = jobs.Status,
                Version = jobs.Version,
                UpdatedAt = jobs.UpdatedAt,
                CreatedAt = jobs.CreatedAt,
                IsDelete = false

            };
        }
        private ApplyJobViewModels ApplyJobViewModelsMapping(TblApplication application)
        {
            return new ApplyJobViewModels
            {
                ApplicationsId = application.ApplicationsId,
                JobsId = application.JobsId,
                ApplicantProfilesId = application.ApplicantProfilesId,
                ResumesId = application.ResumesId,                
                Version = application.Version,
                CreatedAt = application.CreatedAt,
                UpdatedAt = application.UpdatedAt,
                IsDelete = application.IsDelete
            };
        }
        private SavedJobViewModel SavedJobViewModelMapping(TblSavedJob job)
        {
            return new SavedJobViewModel
            {
                SavedJobsId = job.SavedJobsId,
                JobsId = job.JobsId,
                ApplicantProfilesId = job.ApplicantProfilesId,
                Status = job.Status,
                Version = job.Version,
                CreatedAt = job.CreatedAt,
                UpdatedAt = job.UpdatedAt,
                IsDelete = job.IsDelete
            };
        }       
    }
}
