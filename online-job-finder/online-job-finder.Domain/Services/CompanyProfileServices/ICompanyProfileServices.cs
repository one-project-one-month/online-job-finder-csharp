using System;
using online_job_finder.Domain.Services.CompanyProfileServices.CompanyProfileResponses;

namespace online_job_finder.Domain.Services.CompanyProfileServices;

public interface ICompanyProfileServices
{
    List<CompanyProfileJobViewModel> GetCompanyJobs(Guid companyId);
    CompanyProfileJobViewModel CreateJob(CompanyProfileJobViewModel job);
    CompanyProfileJobViewModel? UpdateJob(string jobId, CompanyProfileJobViewModel job);
    bool DeleteJob(string jobId);
    List<ApplicationViewModel> GetJobApplications(string jobId);
    List<ApplicationViewModel> GetShortlistedApplications(string jobId);
    bool UpdateApplicationStatus(string applicationId, string status);
    // List<ReviewViewModel> GetCompanyReviews(Guid companyId);
    CompanyReviewsViewModel GetCompanyReviews(Guid companyId);
}
