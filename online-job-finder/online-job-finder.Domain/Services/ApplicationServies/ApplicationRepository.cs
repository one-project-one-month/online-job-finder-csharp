using online_job_finder.DataBase.Models;
using online_job_finder.Domain.Services.UploadImage;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace online_job_finder.Domain.Services.ApplicationServies;

public class ApplicationRepository : IApplicationRepository
{
    private readonly AppDbContext _db;

    public ApplicationRepository(AppDbContext db)
    {
        _db = db;
    }

    public ApplicantApplicationViewModel CreateApplication(ApplicantApplicationViewModel requestApplication)
    {

        var application = ApplicationMapping(requestApplication);
        application.Version += 1;

        _db.TblApplications.Add(application);
        _db.SaveChanges();

        var responseApplication = ApplicationViewModelMapping(application);

        return responseApplication;
    }

    public List<ApplicantApplicationViewModel> GetApplications(string applicantId)
    {
        var application = _db.TblApplications
            .AsNoTracking()
            .Where(x => x.ApplicantProfilesId.ToString() == applicantId
            && x.IsDelete == false).ToList();

        if (application is null) return null!;

        var applicationResponse = new List<ApplicantApplicationViewModel>();

        foreach (var a in application)
        {
            var mappedApplication = ApplicationViewModelMapping(a);
            applicationResponse.Add(mappedApplication);
        }

        return applicationResponse;
    }

    public ApplicantApplicationViewModel GetApplication(string applicantId, string applicationId)
    {
        var application = _db.TblApplications
            .AsNoTracking()
            .FirstOrDefault(x => x.ApplicantProfilesId.ToString() == applicantId
            && x.ApplicationsId.ToString() == applicationId
            && x.IsDelete == false);

        if (application is null) return null!;

        var applicationResponse = ApplicationViewModelMapping(application);

        return applicationResponse;
    }

    public ApplicantApplicationViewModel UpdateApplication(string applicantId, string applicationId, ApplicantApplicationViewModel requestApplication)
    {
        var application = _db.TblApplications
            .AsNoTracking()
            .FirstOrDefault(x => x.ApplicantProfilesId.ToString() == applicantId
            && x.ApplicationsId.ToString() == applicationId
            && x.IsDelete == false);

        if (application is null) return null!;

        if (!string.IsNullOrEmpty(requestApplication.JobsId.ToString()))
        {
            application.JobsId = requestApplication.JobsId;
        }

        if (!string.IsNullOrEmpty(requestApplication.ResumesId.ToString()))
        {
            application.ResumesId = requestApplication.ResumesId;
        }

        if (!string.IsNullOrEmpty(requestApplication.Status))
        {
            application.Status = requestApplication.Status;
        }

        application.Version += 1;
        application.UpdatedAt = DateTime.UtcNow;

        _db.Entry(application).State = EntityState.Modified;
        _db.SaveChanges();

        var resumeResponse = ApplicationViewModelMapping(application);

        return resumeResponse;
    }

    public bool DeleteApplication(string applicantId, string applicationId)
    {
        var application = _db.TblApplications
            .AsNoTracking()
            .FirstOrDefault(x => x.ApplicantProfilesId.ToString() == applicantId
            && x.ApplicationsId.ToString() == applicationId
            && x.IsDelete == false);

        if (application is null) return false;

        application.Version += 1;
        application.UpdatedAt = DateTime.UtcNow;
        application.IsDelete = true;

        _db.Entry(application).State = EntityState.Modified;
        var result = _db.SaveChanges();

        return result > 0;
    }

    public SavedJobViewModel SavedJob(string applicantId, SavedJobViewModel requestSavedJob)
    {
        var savedJob = _db.TblSavedJobs
            .AsNoTracking()
            .FirstOrDefault(x => x.ApplicantProfilesId.ToString() == applicantId
            && x.JobsId.ToString() == requestSavedJob.JobsId.ToString()
            && x.IsDelete == false);

        if (savedJob is null)
        {
            savedJob = new TblSavedJob
            {
                SavedJobsId = Guid.NewGuid(),
                JobsId = requestSavedJob.JobsId,
                ApplicantProfilesId = requestSavedJob.ApplicantProfilesId,
                Status = requestSavedJob.Status,
                CreatedAt = DateTime.UtcNow,
                IsDelete = false
            };
            savedJob.Version += 1;

            _db.TblSavedJobs.Add(savedJob);
            _db.SaveChanges();
        }
        else if (savedJob is not null)
        {
            if (!string.IsNullOrEmpty(requestSavedJob.Status.ToString()))
            {
                savedJob.Status = requestSavedJob.Status;
            }

            savedJob.Version += 1;
            savedJob.UpdatedAt = DateTime.UtcNow;

            _db.Entry(savedJob).State = EntityState.Modified;
            _db.SaveChanges();
        }

        var savedJobResponse = new SavedJobViewModel
        {
            JobsId = savedJob.JobsId,
            ApplicantProfilesId = savedJob.ApplicantProfilesId,
            Status = savedJob.Status,
            Version = savedJob.Version,
            CreatedAt = savedJob.CreatedAt,
            UpdatedAt = savedJob.UpdatedAt,
            IsDelete = false
        };

        return savedJobResponse;
    }

    public TblApplication ApplicationMapping(ApplicantApplicationViewModel requestApplication)
    {
        return new TblApplication
        {
            ApplicationsId = Guid.NewGuid(),
            JobsId = requestApplication.JobsId,
            ApplicantProfilesId = requestApplication.ApplicantProfilesId,
            ResumesId = requestApplication.ResumesId,
            Status = requestApplication.Status,
            CreatedAt = requestApplication.CreatedAt,
            UpdatedAt = requestApplication.UpdatedAt,
            IsDelete = false
        };
    }

    public ApplicantApplicationViewModel ApplicationViewModelMapping(TblApplication table)
    {
        return new ApplicantApplicationViewModel()
        {
            JobsId = table.JobsId,
            ApplicantProfilesId = table.ApplicantProfilesId,
            ResumesId = table.ResumesId,
            Status = table.Status,
            CreatedAt = table.CreatedAt,
            UpdatedAt = table.UpdatedAt
        };
    }
}
