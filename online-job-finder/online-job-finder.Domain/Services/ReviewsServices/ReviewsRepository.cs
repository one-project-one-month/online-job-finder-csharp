//namespace online_job_finder.Domain.Services.ReviewsServices;

//public class ReviewsRepository
//{
//    private readonly AppDbContext _db;

//    public ReviewsRepository()
//    {
//        _db = new AppDbContext();
//    }

//    public async Task<EducationsViewModels> CreateReview(EducationsViewModels model)
//    {
//        model.Version += 1;
//        model.UpdatedAt = null;

//        TblApplicantEducation items = Applicant_EducationsMapping(model);

//        await _db.TblApplicantEducations.AddAsync(items);
//        await _db.SaveChangesAsync();

//        return model;
//    }

//    public async Task<List<EducationsViewModels>> GetReviews()
//    {
//        var model = await _db.TblApplicantEducations
//            .AsNoTracking()
//            .Where(x => x.IsDelete == false)
//            .OrderBy(x => x.Version)
//            .ToListAsync();

//        var viewModels = model.Select(Applicant_EducationsViewModelsMapping).ToList();

//        return viewModels;
//    }

//    public async Task<EducationsViewModels?> GetReview(string id)
//    {
//        var model = await _db.TblApplicantEducations
//            .AsNoTracking()
//            .FirstOrDefaultAsync(x => x.ApplicantEducationsId.ToString() == id
//            && x.IsDelete == false);

//        if (model is null) { return null; }

//        var viewModel = Applicant_EducationsViewModelsMapping(model);

//        return viewModel;
//    }

//    public async Task<EducationsViewModels?> UpdateReview(string id, EducationsViewModels models)
//    {
//        var item = await _db.TblApplicantEducations
//            .AsNoTracking()
//            .FirstOrDefaultAsync(x => x.ApplicantEducationsId.ToString() == id
//            && x.IsDelete == false);

//        if (item is null) { return null; }

//        if (!string.IsNullOrEmpty(models.School_Name))
//        {
//            item.SchoolName = models.School_Name;
//        }
//        if (!string.IsNullOrEmpty(models.Degree))
//        {
//            item.Degree = models.Degree;
//        }
//        if (!string.IsNullOrEmpty(models.Field_Of_Study))
//        {
//            item.FieldOfStudy = models.Field_Of_Study;
//        }
//        if (!string.IsNullOrEmpty(models.Start_Date.ToString()))
//        {
//            item.StartDate = models.Start_Date;
//        }
//        if (!string.IsNullOrEmpty(models.End_Date.ToString()))
//        {
//            item.EndDate = models.End_Date;
//        }
//        if (!string.IsNullOrEmpty(models.Still_Attending.ToString()))
//        {
//            item.StillAttending = models.Still_Attending;
//        }
//        if (!string.IsNullOrEmpty(models.Description))
//        {
//            item.Description = models.Description;
//        }

//        item.Version += 1;
//        item.UpdatedAt = DateTime.UtcNow;

//        _db.Entry(item).State = EntityState.Modified;
//        await _db.SaveChangesAsync();

//        models = Applicant_EducationsViewModelsMapping(item);

//        return models;
//    }

//    public async Task<EducationsViewModels?> PatchReview(string id, EducationsViewModels models)
//    {
//        var item = await _db.TblApplicantEducations
//            .AsNoTracking()
//            .FirstOrDefaultAsync(x => x.ApplicantEducationsId.ToString() == id
//            && x.IsDelete == false);

//        if (item is null) { return null; }

//        if (!string.IsNullOrEmpty(models.School_Name))
//        {
//            item.SchoolName = models.School_Name;
//        }
//        if (!string.IsNullOrEmpty(models.Degree))
//        {
//            item.Degree = models.Degree;
//        }
//        if (!string.IsNullOrEmpty(models.Field_Of_Study))
//        {
//            item.FieldOfStudy = models.Field_Of_Study;
//        }
//        if (!string.IsNullOrEmpty(models.Start_Date.ToString()))
//        {
//            item.StartDate = models.Start_Date;
//        }
//        if (!string.IsNullOrEmpty(models.End_Date.ToString()))
//        {
//            item.EndDate = models.End_Date;
//        }
//        if (!string.IsNullOrEmpty(models.Still_Attending.ToString()))
//        {
//            item.StillAttending = models.Still_Attending;
//        }
//        if (!string.IsNullOrEmpty(models.Description))
//        {
//            item.Description = models.Description;
//        }

//        item.Version += 1;
//        item.UpdatedAt = DateTime.UtcNow;

//        _db.Entry(item).State = EntityState.Modified;
//        await _db.SaveChangesAsync();

//        models = Applicant_EducationsViewModelsMapping(item);

//        return models;
//    }

//    public async Task<bool?> DeleteReview(string id)
//    {
//        var item = await _db.TblApplicantEducations
//            .AsNoTracking()
//            .FirstOrDefaultAsync(x => x.ApplicantEducationsId.ToString() == id
//            && x.IsDelete == false);
//        if (item is null)
//        {
//            return null;
//        }

//        item.Version += 1;
//        item.UpdatedAt = DateTime.UtcNow;
//        item.IsDelete = true;

//        _db.Entry(item).State = EntityState.Modified;
//        var result = await _db.SaveChangesAsync();

//        return result > 0;
//    }

//    private static TblApplicantEducation ReviewsMapping(EducationsViewModels models)
//    {
//        return new TblApplicantEducation
//        {
//            ApplicantEducationsId = Guid.NewGuid(),
//            ApplicantProfilesId = models.ApplicantProfilesId,
//            SchoolName = models.School_Name,
//            Degree = models.Degree,
//            FieldOfStudy = models.Field_Of_Study,
//            StartDate = models.Start_Date,
//            EndDate = models.End_Date,
//            StillAttending = models.Still_Attending,
//            Description = models.Description,
//            Version = models.Version,
//            CreatedAt = models.CreatedAt,
//            UpdatedAt = models.UpdatedAt,
//            IsDelete = false
//        };
//    }

//    private static EducationsViewModels ReviewsViewModelsMapping(TblApplicantEducation table)
//    {
//        return new EducationsViewModels
//        {
//            //ApplicantEducationsId = Guid.NewGuid(),
//            ApplicantProfilesId = table.ApplicantProfilesId,
//            School_Name = table.SchoolName,
//            Degree = table.Degree,
//            Field_Of_Study = table.FieldOfStudy,
//            Start_Date = table.StartDate,
//            End_Date = table.EndDate,
//            Still_Attending = table.StillAttending,
//            Description = table.Description,
//            Version = table.Version,
//            CreatedAt = table.CreatedAt,
//            UpdatedAt = table.UpdatedAt,
//            //IsDelete = false
//        };
//    }
//}
