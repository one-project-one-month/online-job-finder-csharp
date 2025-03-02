using Microsoft.EntityFrameworkCore;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.Services.JobCategoriesServices;
using online_job_finder.Domain.ViewModels.JobCategories;
using online_job_finder.Domain.ViewModels.Skills;
using online_job_finder.Domain.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.Services.SkillServices
{
    public class SkillRepository : ISkillRepository
    {
        private readonly AppDbContext _db;

        public SkillRepository(AppDbContext db)
        {
            _db = db;
        }

        public TblSkill CreateSkill(SkillsViewModels skills)
        {

            TblSkill skillTable = SkillsMapping(skills);



            _db.TblSkills.Add(skillTable);
            _db.SaveChanges();

            return skillTable;
        }

        public List<SkillsViewModels> GetSkills()
        {
            var model = _db.TblSkills
                .AsNoTracking()
                .ToList();

            var skillViewModels = model.Select(SkillViewModelsMapping).ToList();

            return skillViewModels;
        }

        public SkillsViewModels? GetSkill(string id)
        {
            var model = _db.TblSkills
                .AsNoTracking()
                .FirstOrDefault(x => x.SkillID.ToString() == id
                );

            if (model is null) { return null; }

            var mappingModel = SkillViewModelsMapping(model);

            return mappingModel;
        }

        public SkillsViewModels? UpdateSkill(string id, SkillsViewModels skill)
        {
            var item = _db.TblSkills
                .AsNoTracking()
                .FirstOrDefault(x => x.SkillID.ToString() == id
                );
            if (item is null) { return null; }

            if (!string.IsNullOrEmpty(skill.SkillName))
            {
                item.SkillName = skill.SkillName;
            }



            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            var model = SkillViewModelsMapping(item);

            return model;
        }

        public SkillsViewModels? PatchSkill(string id, SkillsViewModels skill)
        {
            var item = _db.TblSkills
                .AsNoTracking()
                .FirstOrDefault(x => x.SkillID.ToString() == id
                );
            if (item is null) { return null; }

            if (!string.IsNullOrEmpty(skill.SkillName))
            {
                item.SkillName = skill.SkillName;
            }



            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            var model = SkillViewModelsMapping(item);
            return skill;
        }

        public bool? DeleteSkill(string id)
        {
            var item = _db.TblSkills
                .AsNoTracking()
                .FirstOrDefault(x => x.SkillID.ToString() == id
                );
            if (item is null)
            {
                return null;
            }




            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();

            return result > 0;
        }


        private static TblSkill SkillsMapping(SkillsViewModels skills)
        {
            return new TblSkill
            {
                SkillID = Guid.NewGuid(),
                SkillName = skills.SkillName
            };
        }


        private static SkillsViewModels SkillViewModelsMapping(TblSkill skill)
        {
            return new SkillsViewModels
            {

                SkillName = skill.SkillName,

            };
        }

        public UsersViewModels CreateSkill(UsersViewModels user)
        {
            throw new NotImplementedException();
        }
    }
}
