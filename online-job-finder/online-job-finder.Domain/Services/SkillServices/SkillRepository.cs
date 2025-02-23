using Microsoft.EntityFrameworkCore;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Domain.Services.SkillServices
{
    public class SkillRepository : ISkillRepository
    {
        private readonly AppDbContext _db;

        public SkillRepository()
        {
            _db = new AppDbContext();
        }

        public SkillsViewModels CreateSkill(SkillsViewModels model)
        {
            model.Version += 1;
            model.UpdatedAt = null;

            TblSkill skill = SkillsMapping(model);

            _db.TblSkills.Add(skill);
            _db.SaveChanges();

            return model;
        }

        public List<SkillsViewModels> GetSkills()
        {
            var model = _db.TblSkills
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .OrderBy(x => x.Version)
                .ToList();

            var viewModels = model.Select(SkillsViewModelsMapping).ToList();

            return viewModels;
        }

        public SkillsViewModels? GetSkill(string id)
        {
            var model = _db.TblSkills
                .AsNoTracking()
                .FirstOrDefault(x => x.SkillsId.ToString() == id
                && x.IsDelete == false);

            if (model is null) { return null; }

            var viewModel = SkillsViewModelsMapping(model);

            return viewModel;
        }

        public SkillsViewModels? UpdateSkill(string id, SkillsViewModels models)
        {
            var item = _db.TblSkills
                .AsNoTracking()
                .FirstOrDefault(x => x.SkillsId.ToString() == id
                && x.IsDelete == false);
            if (item is null) { return null; }

            if (!string.IsNullOrEmpty(models.SkillsName))
            {
                item.SkillsName = models.SkillsName;
            }

            if (!string.IsNullOrEmpty(models.Description))
            {
                item.Description = models.Description;
            }

            item.Version += 1;
            item.UpdatedAt = DateTime.UtcNow;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            models = SkillsViewModelsMapping(item);

            return models;
        }

        public SkillsViewModels? PatchSkill(string id, SkillsViewModels models)
        {
            var item = _db.TblSkills
                .AsNoTracking()
                .FirstOrDefault(x => x.SkillsId.ToString() == id
                && x.IsDelete == false);
            if (item is null) { return null; }

            if (!string.IsNullOrEmpty(models.SkillsName))
            {
                item.SkillsName = models.SkillsName;
            }

            if (!string.IsNullOrEmpty(models.Description))
            {
                item.Description = models.Description;
            }

            item.Version += 1;
            item.UpdatedAt = DateTime.UtcNow;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            models = SkillsViewModelsMapping(item);

            return models;
        }

        public bool? DeleteSkill(string id)
        {
            var item = _db.TblSkills
                .AsNoTracking()
                .FirstOrDefault(x => x.SkillsId.ToString() == id
                && x.IsDelete == false);
            if (item is null)
            {
                return null;
            }

            item.Version += 1;
            item.UpdatedAt = DateTime.UtcNow;
            item.IsDelete = true;

            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();

            return result > 0;
        }

        //Can use for every roles
        private static TblSkill SkillsMapping(SkillsViewModels viewModels)
        {
            return new TblSkill
            {
                SkillsId = Guid.NewGuid(),
                SkillsName = viewModels.SkillsName,
                Description = viewModels.Description,
                Version = viewModels.Version,
                CreatedAt = viewModels.CreatedAt,
                UpdatedAt = viewModels.UpdatedAt,
                IsDelete = false
            };
            
        }

        private static SkillsViewModels SkillsViewModelsMapping(TblSkill table)
        {
            return new SkillsViewModels
            {
                //SkillsId = Guid.NewGuid(),
                SkillsName = table.SkillsName,
                Description = table.Description,
                Version = table.Version,
                CreatedAt = table.CreatedAt,
                UpdatedAt = table.UpdatedAt
                //IsDelete = false
            };
        }

    }
}
