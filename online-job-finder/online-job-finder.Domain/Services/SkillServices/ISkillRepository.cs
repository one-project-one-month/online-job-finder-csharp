namespace online_job_finder.Domain.Services.SkillServices;

public interface ISkillRepository
{
    Task<SkillsViewModels> CreateSkill(SkillsViewModels Skills);

    Task<List<SkillsViewModels>> GetSkills();

    Task<SkillsViewModels?> GetSkill(string id);

    Task<SkillsViewModels?> UpdateSkill(string id, SkillsViewModels Skills);

    Task<SkillsViewModels?> PatchSkill(string id, SkillsViewModels Skills);

    Task<bool?> DeleteSkill(string id);
}
