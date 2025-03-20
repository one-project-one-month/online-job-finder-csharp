namespace online_job_finder.Domain.Services.Applicant_SkillsServices;

public interface IApplicant_SkillsRepository
{
    Task<Applicant_SkillsViewModels> CreateApplicant_Skills(Applicant_SkillsViewModels model, string userid);
    Task<List<Applicant_SkillsViewModels>> GetApplicant_Skills(string userid);
}