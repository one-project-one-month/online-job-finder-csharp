namespace online_job_finder.Domain.Services.Applicant_ExperiencesServices;

public class Applicant_ExperiencesRepository : IApplicant_ExperiencesRepository
{
    private readonly AppDbContext _db;

    public Applicant_ExperiencesRepository()
    {
        _db = new AppDbContext();
    }

    public async Task<ExperiencesViewModels> CreateApplicant_Experience(ExperiencesViewModels model, string id)
    {
        var idcheck = await _db.TblApplicantProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId.ToString() == id);

        if (idcheck == null) return null;

        model.ApplicantProfilesId = idcheck.ApplicantProfilesId;
        model.Version += 1;
        model.UpdatedAt = null;

        TblApplicantExperience items = Applicant_ExperiencesMapping(model);

        await _db.TblApplicantExperiences.AddAsync(items);
        await _db.SaveChangesAsync();

        return model;
    }

    public async Task<List<ExperiencesViewModels>> GetApplicant_Experiences()
    {
        var model = await _db.TblApplicantExperiences
            .AsNoTracking()
            .Where(x => x.IsDelete == false)
            .OrderBy(x => x.Version)
            .ToListAsync();

        var viewModels = model.Select(Applicant_ExperiencesViewModelsMapping).ToList();

        return viewModels;
    }

    public async Task<ExperiencesViewModels?> GetApplicant_Experience(string id)
    {
        var model = await _db.TblApplicantExperiences
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ApplicantExperiencesId.ToString() == id
            && x.IsDelete == false);

        if (model is null) { return null; }

        var viewModel = Applicant_ExperiencesViewModelsMapping(model);

        return viewModel;
    }

    public async Task<ExperiencesViewModels?> UpdateApplicant_Experience(string id, ExperiencesViewModels models)
    {
        var item = await _db.TblApplicantExperiences
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ApplicantExperiencesId.ToString() == id
            && x.IsDelete == false);

        if (item is null) { return null; }

        if (!string.IsNullOrEmpty(models.CompanyName))
        {
            item.CompanyName = models.CompanyName;
        }
        if (!string.IsNullOrEmpty(models.Location))
        {
            item.Location = models.Location;
        }
        if (!string.IsNullOrEmpty(models.Title))
        {
            item.Title = models.Title;
        }
        if (!string.IsNullOrEmpty(models.JobType))
        {
            item.JobType = models.JobType;
        }
        if (!string.IsNullOrEmpty(models.StartDate.ToString()))
        {
            item.StartDate = models.StartDate;
        }
        if (!string.IsNullOrEmpty(models.EndDate.ToString()))
        {
            item.EndDate = models.EndDate;
        }
        if (!string.IsNullOrEmpty(models.CurrentlyWorking.ToString()))
        {
            item.CurrentlyWorking = models.CurrentlyWorking;
        }
        if (!string.IsNullOrEmpty(models.Description))
        {
            item.Description = models.Description;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        models = Applicant_ExperiencesViewModelsMapping(item);

        return models;
    }

    public async Task<ExperiencesViewModels?> PatchApplicant_Experience(string id, ExperiencesViewModels models)
    {
        var item = await _db.TblApplicantExperiences
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ApplicantExperiencesId.ToString() == id
            && x.IsDelete == false);

        if (item is null) { return null; }

        if (!string.IsNullOrEmpty(models.CompanyName))
        {
            item.CompanyName = models.CompanyName;
        }
        if (!string.IsNullOrEmpty(models.Location))
        {
            item.Location = models.Location;
        }
        if (!string.IsNullOrEmpty(models.Title))
        {
            item.Title = models.Title;
        }
        if (!string.IsNullOrEmpty(models.JobType))
        {
            item.JobType = models.JobType;
        }
        if (!string.IsNullOrEmpty(models.StartDate.ToString()))
        {
            item.StartDate = models.StartDate;
        }
        if (!string.IsNullOrEmpty(models.EndDate.ToString()))
        {
            item.EndDate = models.EndDate;
        }
        if (!string.IsNullOrEmpty(models.CurrentlyWorking.ToString()))
        {
            item.CurrentlyWorking = models.CurrentlyWorking;
        }
        if (!string.IsNullOrEmpty(models.Description))
        {
            item.Description = models.Description;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        models = Applicant_ExperiencesViewModelsMapping(item);

        return models;
    }

    public async Task<bool?> DeleteApplicant_Experience(string id)
    {
        var item = await _db.TblApplicantExperiences
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ApplicantExperiencesId.ToString() == id
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

    private static TblApplicantExperience Applicant_ExperiencesMapping(ExperiencesViewModels models)
    {
        return new TblApplicantExperience
        {
            ApplicantExperiencesId = Guid.NewGuid(),
            ApplicantProfilesId = models.ApplicantProfilesId,
            CompanyName = models.CompanyName,
            Location = models.Location,
            Title = models.Title,
            JobType = models.JobType,
            StartDate = models.StartDate,
            EndDate = models.EndDate,
            CurrentlyWorking = models.CurrentlyWorking,
            Description = models.Description,
            Version = models.Version,
            CreatedAt = models.CreatedAt,
            UpdatedAt = models.UpdatedAt,
            IsDelete = false
        };
    }

    private static ExperiencesViewModels Applicant_ExperiencesViewModelsMapping(TblApplicantExperience table)
    {
        return new ExperiencesViewModels
        {
            //ApplicantExperiencesId = Guid.NewGuid(),
            ApplicantProfilesId = table.ApplicantProfilesId,
            CompanyName = table.CompanyName,
            Location = table.Location,
            Title = table.Title,
            JobType = table.JobType,
            StartDate = table.StartDate,
            EndDate = table.EndDate,
            CurrentlyWorking = table.CurrentlyWorking,
            Description = table.Description,
            Version = table.Version,
            CreatedAt = table.CreatedAt,
            UpdatedAt = table.UpdatedAt,
            //IsDelete = false
        };
    }
}
