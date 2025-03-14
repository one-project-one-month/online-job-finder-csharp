
namespace online_job_finder.Domain.Services.Applicant_EducationsServices
{
    public interface IApplicant_EducationsRepository
    {
        Task<EducationsViewModels> CreateApplicant_Education(EducationsViewModels model, string id);
        Task<bool?> DeleteApplicant_Education(string id);
        Task<EducationsViewModels?> GetApplicant_Education(string id);
        Task<List<EducationsViewModels>> GetApplicant_Educations();
        Task<EducationsViewModels?> PatchApplicant_Education(string id, EducationsViewModels models);
        Task<EducationsViewModels?> UpdateApplicant_Education(string id, EducationsViewModels models);
    }
}