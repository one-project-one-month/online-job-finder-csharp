using System;
using Microsoft.EntityFrameworkCore;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.Services.CompanyProfileServices.CompanyProfileResponses;
using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Domain.Services.CompanyProfileServices;

public class CompanyProfileServices : ICompanyProfileServices
{

    private readonly AppDbContext _db;

    public CompanyProfileServices(AppDbContext db)
    {
        _db = db;
    }

    private (bool IsValid, string Message) ValidateJobRelationships(CompanyProfileJobViewModel job)
    {
        // Check Company Profile
        var companyExists = _db.TblCompanyProfiles
            .Any(c => c.CompanyProfilesId == job.CompanyProfilesId && !c.IsDelete);
        if (!companyExists)
            return (false, "Company profile not found");

        // Check Job Category
        var categoryExists = _db.TblJobCategories
            .Any(c => c.JobCategoriesId == job.JobCategoriesId && !c.IsDelete);
        if (!categoryExists)
            return (false, "Job category not found");

        // Check Location
        var locationExists = _db.TblLocations
            .Any(l => l.LocationId == job.LocationId && !l.IsDelete);
        if (!locationExists)
            return (false, "Location not found");

        return (true, string.Empty);
    }

    public CompanyProfileJobViewModel CreateJob(CompanyProfileJobViewModel job)
    {
        // var companyExist = _db.TblCompanyProfiles.Any(c => c.CompanyProfilesId == job.CompanyProfilesId && !c.IsDelete);
        // if (!companyExist) throw new InvalidOperationException("Company does not exist");

        // Validate all required relationships exist
        var validationResult = ValidateJobRelationships(job);
        if (!validationResult.IsValid)
        {
            throw new InvalidOperationException(validationResult.Message);
        }

        var newJob = new TblJob
        {
            JobsId = Guid.NewGuid(),
            CompanyProfilesId = job.CompanyProfilesId,
            JobCategoriesId = job.JobCategoriesId,
            LocationId = job.LocationId,
            Title = job.Title,
            Type = JobType.GetValidType(job.Type),
            Description = job.Description,
            Requirements = job.Requirements,
            NumOfPosts = job.NumOfPosts,
            Salary = job.Salary,
            Address = job.Address,
            Status = JobStatus.GetValidStatus(job.Status),
            Version = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDelete = false

        };

        _db.TblJobs.Add(newJob);
        _db.SaveChanges();

        job.JobsId = newJob.JobsId;
        job.Version = newJob.Version;
        job.CreatedAt = newJob.CreatedAt;
        job.Status = newJob.Status;

        return job;
    }

    public bool DeleteJob(string jobId)
    {
        var job = _db.TblJobs
            .FirstOrDefault(x => x.JobsId == Guid.Parse(jobId) && !x.IsDelete);
        if (job is null) return false;

        job.IsDelete = true;
        job.Version += 1;
        job.UpdatedAt = DateTime.UtcNow;

        _db.Entry(job).State = EntityState.Modified;
        return _db.SaveChanges() > 0;
    }

    public List<CompanyProfileJobViewModel> GetCompanyJobs(Guid companyId)
    {
        var jobs = _db.TblJobs
            .AsNoTracking()
            .Where(j => j.CompanyProfilesId == companyId && !j.IsDelete)
            .OrderByDescending(j => j.CreatedAt)
            .Select(job => new CompanyProfileJobViewModel
            {
                JobsId = job.JobsId,
                CompanyProfilesId = job.CompanyProfilesId,
                JobCategoriesId = job.JobCategoriesId,
                LocationId = job.LocationId,
                Title = job.Title,
                Type = job.Type,
                Description = job.Description,
                Requirements = job.Requirements,
                NumOfPosts = job.NumOfPosts,
                Salary = job.Salary,
                Address = job.Address,
                Status = job.Status,
                Version = job.Version,
                CreatedAt = job.CreatedAt,
                UpdatedAt = job.UpdatedAt
            })
            .ToList();

        return jobs;
    }

    public List<ReviewViewModel> GetCompanyReviews(Guid companyId)
    {
        var reviews = _db.TblReviews
            .AsNoTracking()
            .Where(x => x.CompanyProfilesId == companyId)
            .Select(review => new ReviewViewModel
            {
                ReviewsId = review.ReviewsId,
                CompanyProfilesId = review.CompanyProfilesId,
                ApplicantProfilesId = review.ApplicantProfilesId,
                Ratings = review.Ratings,
                Comments = review.Comments,
                CreatedAt = review.CreatedAt,
                UpdatedAt = review.UpdatedAt
            })
            .OrderByDescending(x => x.CreatedAt)
            .ToList();

        return reviews;
    }

    public List<ApplicationViewModel> GetJobApplications(string jobId)
    {
        var applications = _db.TblApplications
            .AsNoTracking()
            .OrderByDescending(a => a.CreatedAt)
            .Where(a => a.JobsId == Guid.Parse(jobId) && !a.IsDelete)
            .Select(app => new ApplicationViewModel
            {
                ApplicationsId = app.ApplicationsId,
                ApplicantProfilesId = app.ApplicantProfilesId,
                JobsId = app.JobsId,
                ResumesId = app.ResumesId,
                Status = app.Status,
                CreatedAt = app.CreatedAt,
                UpdatedAt = app.UpdatedAt
            })
            .ToList();

        // foreach (var application in applications)
        // {
        //     var job = _db.TblJobs
        //     .AsNoTracking()
        //     .Where(j => j.JobsId == application.JobsId)
        //     .Select(job => new CompanyProfileJobViewModel
        //     {
        //         JobsId = job.JobsId,
        //         CompanyProfilesId = job.CompanyProfilesId,
        //         JobCategoriesId = job.JobCategoriesId,
        //         LocationId = job.LocationId,
        //         Title = job.Title,
        //         Type = job.Type,
        //         Description = job.Description,
        //         Requirements = job.Requirements,
        //         NumOfPosts = job.NumOfPosts,
        //         Salary = job.Salary,
        //         Address = job.Address,
        //         Status = job.Status,
        //         Version = job.Version,
        //         CreatedAt = job.CreatedAt,
        //         UpdatedAt = job.UpdatedAt
        //     })
        //     ;

        //     var resume = _db.TblResumes
        //     .AsNoTracking()
        //     .Where(r => r.ResumesId == application.ResumesId)
        //     .Select(r => new TblResume
        //     {
        //         ResumesId = r.ResumesId,
        //         UserId = r.UserId,
        //         FilePath = r.FilePath,
        //         Version = r.Version,
        //         CreatedAt = r.CreatedAt,
        //         UpdatedAt = r.UpdatedAt
        //     });

        //     var applicant = _db.TblApplicantProfiles
        //     .AsNoTracking()
        //     .Where(ap => ap.ApplicantProfilesId == application.ApplicantProfilesId)
        //     .Select(ap => new TblApplicantProfile
        //     {
        //         ApplicantProfilesId = ap.ApplicantProfilesId,
        //         UserId = ap.UserId,
        //         FullName = ap.FullName,
        //         Phone = ap.Phone,
        //         Address = ap.Address,
        //         Description = ap.Description,
        //     });
        // }

        return applications;
    }

    public List<ApplicationViewModel> GetShortlistedApplications(string jobId)
    {
        var shortListedApplications = _db.TblApplications
             .AsNoTracking()
             .Where(x => x.JobsId == Guid.Parse(jobId)
             && x.Status == "Pending" // can change
             && !x.IsDelete)
             .Select(app => new ApplicationViewModel
             {
                 ApplicationsId = app.ApplicationsId,
                 ApplicantProfilesId = app.ApplicantProfilesId,
                 JobsId = app.JobsId,
                 ResumesId = app.ResumesId,
                 Status = app.Status,
                 CreatedAt = app.CreatedAt,
                 UpdatedAt = app.UpdatedAt
             })
             .ToList();

        return shortListedApplications;
    }

    public bool UpdateApplicationStatus(string applicationId, string status)
    {
        var application = _db.TblApplications
            .FirstOrDefault(x => x.ApplicationsId == Guid.Parse(applicationId));

        if (application == null)
        {
            return false;
        }

        application.Status = ApplicationStatus.GetValidStatus(status);
        application.UpdatedAt = DateTime.UtcNow;
        application.Version += 1;

        _db.Entry(application).State = EntityState.Modified;
        return _db.SaveChanges() > 0;
    }

    public CompanyProfileJobViewModel? UpdateJob(string jobId, CompanyProfileJobViewModel job)
    {
        var existingJob = _db.TblJobs
             .FirstOrDefault(x => x.JobsId == Guid.Parse(jobId) && !x.IsDelete);

        if (existingJob is null) return null;

        existingJob.Title = job.Title;
        existingJob.Type = JobType.GetValidType(job.Type);
        existingJob.Description = job.Description;
        existingJob.Requirements = job.Requirements;
        existingJob.NumOfPosts = job.NumOfPosts;
        existingJob.Salary = job.Salary;
        existingJob.Address = job.Address;
        existingJob.Status = JobStatus.GetValidStatus(job.Status);
        existingJob.LocationId = job.LocationId;
        existingJob.JobCategoriesId = job.JobCategoriesId;
        existingJob.Version += 1;
        existingJob.UpdatedAt = DateTime.UtcNow;

        _db.Entry(existingJob).State = EntityState.Modified;
        _db.SaveChanges();

        return new CompanyProfileJobViewModel
        {
            JobsId = existingJob.JobsId,
            CompanyProfilesId = existingJob.CompanyProfilesId,
            JobCategoriesId = existingJob.JobCategoriesId,
            LocationId = existingJob.LocationId,
            Title = existingJob.Title,
            Type = existingJob.Type,
            Description = existingJob.Description,
            Requirements = existingJob.Requirements,
            NumOfPosts = existingJob.NumOfPosts,
            Salary = existingJob.Salary,
            Address = existingJob.Address,
            Status = existingJob.Status,
            Version = existingJob.Version,
            CreatedAt = existingJob.CreatedAt,
            UpdatedAt = existingJob.UpdatedAt
        };
    }
}
