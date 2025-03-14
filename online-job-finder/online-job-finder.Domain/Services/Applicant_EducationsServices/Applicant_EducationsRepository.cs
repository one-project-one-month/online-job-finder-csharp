namespace online_job_finder.Domain.Services.Applicant_EducationsServices;

public class Applicant_EducationsRepository : IApplicant_EducationsRepository
{
    private readonly AppDbContext _db;

    public Applicant_EducationsRepository()
    {
        _db = new AppDbContext();
    }

    public async Task<EducationsViewModels> CreateApplicant_Education(EducationsViewModels model, string id)
    {
        var idcheck = await _db.TblApplicantProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId.ToString() == id);

        if (idcheck == null) return null;

        model.ApplicantProfilesId = idcheck.ApplicantProfilesId;
        model.Version += 1;
        model.UpdatedAt = null;

        TblApplicantEducation items = Applicant_EducationsMapping(model);

        await _db.TblApplicantEducations.AddAsync(items);
        await _db.SaveChangesAsync();

        return model;
    }

    public async Task<List<EducationsViewModels>> GetApplicant_Educations()
    {
        var model = await _db.TblApplicantEducations
            .AsNoTracking()
            .Where(x => x.IsDelete == false)
            .OrderBy(x => x.Version)
            .ToListAsync();

        var viewModels = model.Select(Applicant_EducationsViewModelsMapping).ToList();

        return viewModels;
    }

    public async Task<EducationsViewModels?> GetApplicant_Education(string id)
    {
        var model = await _db.TblApplicantEducations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ApplicantEducationsId.ToString() == id
            && x.IsDelete == false);

        if (model is null) { return null; }

        var viewModel = Applicant_EducationsViewModelsMapping(model);

        return viewModel;
    }

    public async Task<EducationsViewModels?> UpdateApplicant_Education(string id, EducationsViewModels models)
    {
        var item = await _db.TblApplicantEducations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ApplicantEducationsId.ToString() == id
            && x.IsDelete == false);

        if (item is null) { return null; }

        if (!string.IsNullOrEmpty(models.SchoolName))
        {
            item.SchoolName = models.SchoolName;
        }
        if (!string.IsNullOrEmpty(models.Degree))
        {
            item.Degree = models.Degree;
        }
        if (!string.IsNullOrEmpty(models.FieldOfStudy))
        {
            item.FieldOfStudy = models.FieldOfStudy;
        }
        if (!string.IsNullOrEmpty(models.StartDate.ToString()))
        {
            item.StartDate = models.StartDate;
        }
        if (!string.IsNullOrEmpty(models.EndDate.ToString()))
        {
            item.EndDate = models.EndDate;
        }
        if (!string.IsNullOrEmpty(models.StillAttending.ToString()))
        {
            item.StillAttending = models.StillAttending;
        }
        if (!string.IsNullOrEmpty(models.Description))
        {
            item.Description = models.Description;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        models = Applicant_EducationsViewModelsMapping(item);

        return models;
    }

    public async Task<EducationsViewModels?> PatchApplicant_Education(string id, EducationsViewModels models)
    {
        var item = await _db.TblApplicantEducations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ApplicantEducationsId.ToString() == id
            && x.IsDelete == false);

        if (item is null) { return null; }

        if (!string.IsNullOrEmpty(models.SchoolName))
        {
            item.SchoolName = models.SchoolName;
        }
        if (!string.IsNullOrEmpty(models.Degree))
        {
            item.Degree = models.Degree;
        }
        if (!string.IsNullOrEmpty(models.FieldOfStudy))
        {
            item.FieldOfStudy = models.FieldOfStudy;
        }
        if (!string.IsNullOrEmpty(models.StartDate.ToString()))
        {
            item.StartDate = models.StartDate;
        }
        if (!string.IsNullOrEmpty(models.EndDate.ToString()))
        {
            item.EndDate = models.EndDate;
        }
        if (!string.IsNullOrEmpty(models.StillAttending.ToString()))
        {
            item.StillAttending = models.StillAttending;
        }
        if (!string.IsNullOrEmpty(models.Description))
        {
            item.Description = models.Description;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        models = Applicant_EducationsViewModelsMapping(item);

        return models;
    }

    public async Task<bool?> DeleteApplicant_Education(string id)
    {
        var item = await _db.TblApplicantEducations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ApplicantEducationsId.ToString() == id
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

    private static TblApplicantEducation Applicant_EducationsMapping(EducationsViewModels models)
    {
        return new TblApplicantEducation
        {
            ApplicantEducationsId = Guid.NewGuid(),
            ApplicantProfilesId = models.ApplicantProfilesId,
            SchoolName = models.SchoolName,
            Degree = models.Degree,
            FieldOfStudy = models.FieldOfStudy,
            StartDate = models.StartDate,
            EndDate = models.EndDate,
            StillAttending = models.StillAttending,
            Description = models.Description,
            Version = models.Version,
            CreatedAt = models.CreatedAt,
            UpdatedAt = models.UpdatedAt,
            IsDelete = false
        };
    }

    private static EducationsViewModels Applicant_EducationsViewModelsMapping(TblApplicantEducation table)
    {
        return new EducationsViewModels
        {
            //ApplicantEducationsId = Guid.NewGuid(),
            ApplicantProfilesId = table.ApplicantProfilesId,
            SchoolName = table.SchoolName,
            Degree = table.Degree,
            FieldOfStudy = table.FieldOfStudy,
            StartDate = table.StartDate,
            EndDate = table.EndDate,
            StillAttending = table.StillAttending,
            Description = table.Description,
            Version = table.Version,
            CreatedAt = table.CreatedAt,
            UpdatedAt = table.UpdatedAt,
            //IsDelete = false
        };
    }
}
