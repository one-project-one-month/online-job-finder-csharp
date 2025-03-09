namespace online_job_finder.Domain.Services.SkillServices;

public class SkillRepository : ISkillRepository
{
    private readonly AppDbContext _db;

    public SkillRepository()
    {
        _db = new AppDbContext();
    }

    public async Task<SkillsViewModels> CreateSkill(SkillsViewModels model)
    {
        model.Version += 1;
        model.UpdatedAt = null;

        TblSkill skill = SkillsMapping(model);

        await _db.TblSkills.AddAsync(skill);
        await _db.SaveChangesAsync();

        return model;
    }

    public async Task<List<SkillsViewModels>> GetSkills()
    {
        var model = await _db.TblSkills
            .AsNoTracking()
            .Where(x => x.IsDelete == false)
            .OrderBy(x => x.Version)
            .ToListAsync();

        var viewModels = model.Select(SkillsViewModelsMapping).ToList();

        return viewModels;
    }

    public async Task<SkillsViewModels?> GetSkill(string id)
    {
        var model = await _db.TblSkills
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SkillsId.ToString() == id
            && x.IsDelete == false);

        if (model is null) { return null; }

        var viewModel = SkillsViewModelsMapping(model);

        return viewModel;
    }

    public async Task<SkillsViewModels?> UpdateSkill(string id, SkillsViewModels models)
    {
        var item = await _db.TblSkills
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SkillsId.ToString() == id
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
        await _db.SaveChangesAsync();

        models = SkillsViewModelsMapping(item);

        return models;
    }

    public async Task<SkillsViewModels?> PatchSkill(string id, SkillsViewModels models)
    {
        var item = await _db.TblSkills
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SkillsId.ToString() == id
            && x.IsDelete == false);

        if (item is null)  return null; 

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
        await _db.SaveChangesAsync();

        models = SkillsViewModelsMapping(item);

        return models;
    }

    public async Task<bool?> DeleteSkill(string id)
    {
        var item = await _db.TblSkills
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SkillsId.ToString() == id
            && x.IsDelete == false);

        if (item is null) return null;

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;
        item.IsDelete = true;

        _db.Entry(item).State = EntityState.Modified;
        var result = await _db.SaveChangesAsync();

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
