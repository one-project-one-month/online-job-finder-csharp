using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Domain.Services.SkillServices;

public interface ISkillRepository
{
    SkillsViewModels CreateSkill(SkillsViewModels Skills);

    List<SkillsViewModels> GetSkills();

    SkillsViewModels? GetSkill(string id);

    SkillsViewModels? UpdateSkill(string id, SkillsViewModels Skills);

    SkillsViewModels? PatchSkill(string id, SkillsViewModels Skills);

    bool? DeleteSkill(string id);
}
