namespace online_job_finder.Domain.Services.ApplicantProfileServices;

public interface IApplicantProfileRepository
{
    ApplicantProfileViewModel GetApplicantProfile(string applicantId);
    ApplicantProfileViewModel CreateApplicantProfile(ApplicantProfileViewModel applicantProfile);
    ApplicantProfileViewModel UpdateApplicantProfile(string applicantId, ApplicantProfileViewModel model);
    bool DeleteApplicantProfile(string applicantId);
}
