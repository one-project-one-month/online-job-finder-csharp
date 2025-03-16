namespace online_job_finder.Domain.Services.ApplicationServies;

public interface IApplicationRepository
{
    List<ApplicantApplicationViewModel> GetApplications(string applicantId);

    ApplicantApplicationViewModel GetApplication(string applicantId, string applicationId);

    ApplicantApplicationViewModel CreateApplication(ApplicantApplicationViewModel requestApplication);

    ApplicantApplicationViewModel UpdateApplication(string applicantId, string applicationId, ApplicantApplicationViewModel requestApplication);

    bool DeleteApplication(string applicantId, string applicationId);

    SavedJobViewModel SavedJob(string applicantId, SavedJobViewModel requestSavedJob);
}
