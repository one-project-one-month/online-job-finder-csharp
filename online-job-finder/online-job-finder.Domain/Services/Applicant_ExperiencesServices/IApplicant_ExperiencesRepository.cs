namespace online_job_finder.Domain.Services.Applicant_ExperiencesServices;

public interface IApplicant_ExperiencesRepository
{
    Task<ExperiencesViewModels> CreateApplicant_Experience(ExperiencesViewModels model, string id);
    Task<bool?> DeleteApplicant_Experience(string id);
    Task<ExperiencesViewModels?> GetApplicant_Experience(string id);
    Task<List<ExperiencesViewModels>> GetApplicant_Experiences();
    Task<ExperiencesViewModels?> PatchApplicant_Experience(string id, ExperiencesViewModels models);
    Task<ExperiencesViewModels?> UpdateApplicant_Experience(string id, ExperiencesViewModels models);
}