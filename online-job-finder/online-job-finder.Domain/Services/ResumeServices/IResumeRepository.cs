namespace online_job_finder.Domain.Services.ResumeServices;

public interface IResumeRepository
{
    List<ResumeViewModel> GetResumes(string applicantId);

    ResumeViewModel GetResume(string applicantId, string resumeId);

    ResumeViewModel CreateResume(ResumeViewModel requestResume);

    ResumeViewModel UpdateResume(string applicantId, string resumeId, ResumeViewModel requestResume);

    bool DeleteResume(string applicantId, string resumeId);
}
