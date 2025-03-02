using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels.JobCategories;
using online_job_finder.Domain.ViewModels.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.Services.SkillServices
{
    public interface ISkillRepository
    {
        TblSkill CreateSkill(SkillsViewModels jobcateogry);

        List<SkillsViewModels> GetSkills();

        SkillsViewModels? GetSkill(string id);

        SkillsViewModels? UpdateSkill(string id, SkillsViewModels skills);

        SkillsViewModels? PatchSkill(string id, SkillsViewModels skills);

        bool? DeleteSkill(string id);
    }
}
