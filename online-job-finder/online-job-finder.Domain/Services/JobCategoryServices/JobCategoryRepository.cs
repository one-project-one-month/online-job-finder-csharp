using Microsoft.EntityFrameworkCore;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Domain.Services.JobCategoryServices
{
    public class JobCategoryRepository : IJobCategoryRepository
    {
        private readonly AppDbContext _db;

        public JobCategoryRepository()
        {
            _db = new AppDbContext();
        }

        public JobCategoryViewModels CreateJobCategory(JobCategoryViewModels model)
        {
            model.Version += 1;
            model.UpdatedAt = null;

            TblJobCategory jobCategory = JobCategoryMapping(model);

            _db.TblJobCategories.Add(jobCategory);
            _db.SaveChanges();

            return model;
        }

        public List<JobCategoryViewModels> GetJobCategories()
        {
            var model = _db.TblJobCategories
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .OrderBy(x => x.Version)
                .ToList();

            var viewModels = model.Select(JobCategoryViewModelsMapping).ToList();

            return viewModels;
        }

        public JobCategoryViewModels? GetJobCategory(string id)
        {
            var model = _db.TblJobCategories
                .AsNoTracking()
                .FirstOrDefault(x => x.JobCategoriesId.ToString() == id
                && x.IsDelete == false);

            if (model is null) { return null; }

            var viewModel = JobCategoryViewModelsMapping(model);

            return viewModel;
        }

        public JobCategoryViewModels? UpdateJobCategory(string id, JobCategoryViewModels models)
        {
            var item = _db.TblJobCategories
                .AsNoTracking()
                .FirstOrDefault(x => x.JobCategoriesId.ToString() == id
                && x.IsDelete == false);
            if (item is null) { return null; }

            if (!string.IsNullOrEmpty(models.Industry))
            {
                item.Industry = models.Industry;
            }

            if (!string.IsNullOrEmpty(models.Description))
            {
                item.Description = models.Description;
            }

            item.Version += 1;
            item.UpdatedAt = DateTime.UtcNow;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            models = JobCategoryViewModelsMapping(item);

            return models;
        }

        public JobCategoryViewModels? PatchJobCategory(string id, JobCategoryViewModels models)
        {
            var item = _db.TblJobCategories
                .AsNoTracking()
                .FirstOrDefault(x => x.JobCategoriesId.ToString() == id
                && x.IsDelete == false);
            if (item is null) { return null; }

            if (!string.IsNullOrEmpty(models.Industry))
            {
                item.Industry = models.Industry;
            }

            if (!string.IsNullOrEmpty(models.Description))
            {
                item.Description = models.Description;
            }

            item.Version += 1;
            item.UpdatedAt = DateTime.UtcNow;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            models = JobCategoryViewModelsMapping(item);

            return models;
        }

        public bool? DeleteJobCategory(string id)
        {
            var item = _db.TblJobCategories
                .AsNoTracking()
                .FirstOrDefault(x => x.JobCategoriesId.ToString() == id
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

        private static TblJobCategory JobCategoryMapping(JobCategoryViewModels models)
        {
            return new TblJobCategory
            {
                JobCategoriesId = Guid.NewGuid(),
                Industry = models.Industry,
                Description = models.Description,
                Version = models.Version,
                CreatedAt = models.CreatedAt,
                UpdatedAt = models.UpdatedAt,
                IsDelete = false
            };
        }

        private static JobCategoryViewModels JobCategoryViewModelsMapping(TblJobCategory table)
        {
            return new JobCategoryViewModels
            {
                //JobCategoriesId = Guid.NewGuid(),
                Industry = table.Industry,
                Description = table.Description,
                Version = table.Version,
                CreatedAt = table.CreatedAt,
                UpdatedAt = table.UpdatedAt,
                //IsDelete = false
            };
        }
    }
}
