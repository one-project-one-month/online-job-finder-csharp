namespace online_job_finder.Domain.Services.Applicant_JobCategoryServices;

public class Applicant_JobCategoryRepository : IApplicant_JobCategoryRepository
{
    private readonly AppDbContext _db;

    public Applicant_JobCategoryRepository()
    {
        _db = new AppDbContext();
    }

    public async Task<Applicant_JobCategoryViewModels> CreateApplicant_JobCategory(Applicant_JobCategoryViewModels models, string userid)
    {
        var usercheck = await _db.TblApplicantProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IsDelete == false
            && x.UserId.ToString() == userid);

        if (usercheck == null) return null;

        models.ApplicantProfilesId = usercheck.ApplicantProfilesId;
        models.Version += 1;
        models.UpdatedAt = null;

        var item = new TblApplicantJobCategory();
        item.ApplicantProfilesId = models.ApplicantProfilesId;
        item.Reasons = models.Reasons;
        item.Version = models.Version;
        item.CreatedAt = models.CreatedAt;
        item.IsDelete = false;

        for (int i = 0; i < models.JobCategoriesIds.Count; i++)
        {
            item.ApplicantJobCategoriesId = Guid.NewGuid();
            item.JobCategoriesId = models.JobCategoriesIds[i];
            await _db.TblApplicantJobCategories.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        return models;
    }

    public async Task<List<Applicant_JobCategoryViewModels>> GetApplicant_JobCategories(string userid)
    {
        var usercheck = await _db.TblApplicantProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IsDelete == false
            && x.UserId.ToString() == userid);

        if (usercheck == null) return null;

        var skills = await _db.TblApplicantJobCategories
            .AsNoTracking()
            .Where(x => x.IsDelete == false
            && x.ApplicantProfilesId == usercheck.ApplicantProfilesId)
            .OrderBy(x => x.Version)
            .ToListAsync();


        //var skillIds = skills.Select(x => x.SkillsId).ToList();

        //var viewModels = skills.Select(Applicant_SkillsViewModelsMapping).ToList();

        var groupedSkills = skills
            .GroupBy(x => x.ApplicantProfilesId)  // Group by ApplicantProfilesId
            .Select(g => new Applicant_JobCategoryViewModels
            {
                JobCategoriesIds = g.Select(x => x.JobCategoriesId)  // Collect all SkillsId for the applicant
                                .Distinct()  // Ensure no duplicates
                                .ToList(),
                ApplicantProfilesId = g.Key,  // The ApplicantProfilesId for the group
                Reasons = g.FirstOrDefault()?.Reasons,
                Version = g.FirstOrDefault()!.Version,
                CreatedAt = g.FirstOrDefault()!.CreatedAt,
                UpdatedAt = g.FirstOrDefault()?.UpdatedAt
            })
            .ToList();

        return groupedSkills;
    }

    //public async Task<ExperiencesViewModels?> GetApplicant_Experience(string id)
    //{
    //    var model = await _db.TblApplicantExperiences
    //        .AsNoTracking()
    //        .FirstOrDefaultAsync(x => x.ApplicantExperiencesId.ToString() == id
    //        && x.IsDelete == false);

    //    if (model is null) { return null; }

    //    var viewModel = Applicant_ExperiencesViewModelsMapping(model);

    //    return viewModel;
    //}

    //public async Task<ExperiencesViewModels?> UpdateApplicant_Experience(string id, ExperiencesViewModels models)
    //{
    //    var item = await _db.TblApplicantExperiences
    //        .AsNoTracking()
    //        .FirstOrDefaultAsync(x => x.ApplicantExperiencesId.ToString() == id
    //        && x.IsDelete == false);

    //    if (item is null) { return null; }

    //    if (!string.IsNullOrEmpty(models.CompanyName))
    //    {
    //        item.CompanyName = models.CompanyName;
    //    }
    //    if (!string.IsNullOrEmpty(models.Location))
    //    {
    //        item.Location = models.Location;
    //    }
    //    if (!string.IsNullOrEmpty(models.Title))
    //    {
    //        item.Title = models.Title;
    //    }
    //    if (!string.IsNullOrEmpty(models.JobType))
    //    {
    //        item.JobType = models.JobType;
    //    }
    //    if (!string.IsNullOrEmpty(models.StartDate.ToString()))
    //    {
    //        item.StartDate = models.StartDate;
    //    }
    //    if (!string.IsNullOrEmpty(models.EndDate.ToString()))
    //    {
    //        item.EndDate = models.EndDate;
    //    }
    //    if (!string.IsNullOrEmpty(models.CurrentlyWorking.ToString()))
    //    {
    //        item.CurrentlyWorking = models.CurrentlyWorking;
    //    }
    //    if (!string.IsNullOrEmpty(models.Description))
    //    {
    //        item.Description = models.Description;
    //    }

    //    item.Version += 1;
    //    item.UpdatedAt = DateTime.UtcNow;

    //    _db.Entry(item).State = EntityState.Modified;
    //    await _db.SaveChangesAsync();

    //    models = Applicant_ExperiencesViewModelsMapping(item);

    //    return models;
    //}

    //public async Task<ExperiencesViewModels?> PatchApplicant_Experience(string id, ExperiencesViewModels models)
    //{
    //    var item = await _db.TblApplicantExperiences
    //        .AsNoTracking()
    //        .FirstOrDefaultAsync(x => x.ApplicantExperiencesId.ToString() == id
    //        && x.IsDelete == false);

    //    if (item is null) { return null; }

    //    if (!string.IsNullOrEmpty(models.CompanyName))
    //    {
    //        item.CompanyName = models.CompanyName;
    //    }
    //    if (!string.IsNullOrEmpty(models.Location))
    //    {
    //        item.Location = models.Location;
    //    }
    //    if (!string.IsNullOrEmpty(models.Title))
    //    {
    //        item.Title = models.Title;
    //    }
    //    if (!string.IsNullOrEmpty(models.JobType))
    //    {
    //        item.JobType = models.JobType;
    //    }
    //    if (!string.IsNullOrEmpty(models.StartDate.ToString()))
    //    {
    //        item.StartDate = models.StartDate;
    //    }
    //    if (!string.IsNullOrEmpty(models.EndDate.ToString()))
    //    {
    //        item.EndDate = models.EndDate;
    //    }
    //    if (!string.IsNullOrEmpty(models.CurrentlyWorking.ToString()))
    //    {
    //        item.CurrentlyWorking = models.CurrentlyWorking;
    //    }
    //    if (!string.IsNullOrEmpty(models.Description))
    //    {
    //        item.Description = models.Description;
    //    }

    //    item.Version += 1;
    //    item.UpdatedAt = DateTime.UtcNow;

    //    _db.Entry(item).State = EntityState.Modified;
    //    await _db.SaveChangesAsync();

    //    models = Applicant_ExperiencesViewModelsMapping(item);

    //    return models;
    //}

    //public async Task<bool?> DeleteApplicant_Experience(string id)
    //{
    //    var item = await _db.TblApplicantExperiences
    //        .AsNoTracking()
    //        .FirstOrDefaultAsync(x => x.ApplicantExperiencesId.ToString() == id
    //        && x.IsDelete == false);
    //    if (item is null)
    //    {
    //        return null;
    //    }

    //    item.Version += 1;
    //    item.UpdatedAt = DateTime.UtcNow;
    //    item.IsDelete = true;

    //    _db.Entry(item).State = EntityState.Modified;
    //    var result = await _db.SaveChangesAsync();

    //    return result > 0;
    //}

    //private static TblApplicantExperience Applicant_ExperiencesMapping(ExperiencesViewModels models)
    //{
    //    return new TblApplicantExperience
    //    {
    //        ApplicantExperiencesId = Guid.NewGuid(),
    //        ApplicantProfilesId = models.ApplicantProfilesId,
    //        CompanyName = models.CompanyName,
    //        Location = models.Location,
    //        Title = models.Title,
    //        JobType = models.JobType,
    //        StartDate = models.StartDate,
    //        EndDate = models.EndDate,
    //        CurrentlyWorking = models.CurrentlyWorking,
    //        Description = models.Description,
    //        Version = models.Version,
    //        CreatedAt = models.CreatedAt,
    //        UpdatedAt = models.UpdatedAt,
    //        IsDelete = false
    //    };
    //}

    //private static ExperiencesViewModels Applicant_ExperiencesViewModelsMapping(TblApplicantExperience table)
    //{
    //    return new ExperiencesViewModels
    //    {
    //        //ApplicantExperiencesId = Guid.NewGuid(),
    //        ApplicantProfilesId = table.ApplicantProfilesId,
    //        CompanyName = table.CompanyName,
    //        Location = table.Location,
    //        Title = table.Title,
    //        JobType = table.JobType,
    //        StartDate = table.StartDate,
    //        EndDate = table.EndDate,
    //        CurrentlyWorking = table.CurrentlyWorking,
    //        Description = table.Description,
    //        Version = table.Version,
    //        CreatedAt = table.CreatedAt,
    //        UpdatedAt = table.UpdatedAt,
    //        //IsDelete = false
    //    };
    //}
}
