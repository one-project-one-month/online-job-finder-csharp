namespace online_job_finder.Domain.Services.JobCategoryServices;

public class JobCategoryRepository : IJobCategoryRepository
{
    private readonly AppDbContext _db;

    public JobCategoryRepository()
    {
        _db = new AppDbContext();
    }

    public async Task<JobCategoryViewModels> CreateJobCategory(JobCategoryViewModels model)
    {
        model.Version += 1;
        model.UpdatedAt = null;

        TblJobCategory jobCategory = JobCategoryMapping(model);

        await _db.TblJobCategories.AddAsync(jobCategory);
        await _db.SaveChangesAsync();

        return model;
    }

    public async Task<List<JobCategoryViewModels>> GetJobCategories()
    {
        var model = await _db.TblJobCategories
            .AsNoTracking()
            .Where(x => x.IsDelete == false)
            .OrderBy(x => x.Version)
            .ToListAsync();

        var viewModels = model.Select(JobCategoryViewModelsMapping).ToList();

        return viewModels;
    }

    public async Task<JobCategoryViewModels?> GetJobCategory(string id)
    {
        var model = await _db.TblJobCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.JobCategoriesId.ToString() == id
            && x.IsDelete == false);

        if (model is null) { return null; }

        var viewModel = JobCategoryViewModelsMapping(model);

        return viewModel;
    }

    public async Task<JobCategoryViewModels?> UpdateJobCategory(string id, JobCategoryViewModels models)
    {
        var item = await _db.TblJobCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.JobCategoriesId.ToString() == id
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
        await _db.SaveChangesAsync();

        models = JobCategoryViewModelsMapping(item);

        return models;
    }

    public async Task<JobCategoryViewModels?> PatchJobCategory(string id, JobCategoryViewModels models)
    {
        var item = await _db.TblJobCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.JobCategoriesId.ToString() == id
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
        await _db.SaveChangesAsync();

        models = JobCategoryViewModelsMapping(item);

        return models;
    }

    public async Task<bool?> DeleteJobCategory(string id)
    {
        var item = await _db.TblJobCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.JobCategoriesId.ToString() == id
            && x.IsDelete == false);
        if (item is null)
        {
            return null;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;
        item.IsDelete = true;

        _db.Entry(item).State = EntityState.Modified;
        var result = await _db.SaveChangesAsync();

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
