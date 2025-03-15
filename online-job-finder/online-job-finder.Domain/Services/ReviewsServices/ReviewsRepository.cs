namespace online_job_finder.Domain.Services.ReviewsServices;

public class ReviewsRepository : IReviewsRepository
{
    private readonly AppDbContext _db;

    public ReviewsRepository()
    {
        _db = new AppDbContext();
    }

    public async Task<ReviewViewModels> CreateReview(ReviewViewModels model, string userid)
    {
        var usercheck = await _db.TblApplicantProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IsDelete == false
            && x.UserId.ToString() == userid);

        if (usercheck == null) return null;

        model.ApplicantProfilesId = usercheck.ApplicantProfilesId;
        model.Version += 1;
        model.UpdatedAt = null;

        TblReview items = ReviewsMapping(model);

        await _db.TblReviews.AddAsync(items);
        await _db.SaveChangesAsync();

        return model;
    }

    public async Task<List<ReviewViewModels>> GetReviews(string userid)
    {
        var usercheck = await _db.TblApplicantProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IsDelete == false
            && x.UserId.ToString() == userid);

        if (usercheck == null) return null;

        var model = await _db.TblReviews
            .AsNoTracking()
            .Where(x => x.IsDelete == false 
            && x.ApplicantProfilesId == usercheck.ApplicantProfilesId)
            .OrderBy(x => x.Version)
            .ToListAsync();

        var viewModels = model.Select(ReviewsViewModelsMapping).ToList();

        return viewModels;
    }

    public async Task<ReviewViewModels?> GetReview(string id, string userid)
    {
        var usercheck = await _db.TblApplicantProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IsDelete == false
            && x.UserId.ToString() == userid);

        if (usercheck == null) return null;

        var model = await _db.TblReviews
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ReviewsId.ToString() == id
            && x.IsDelete == false
            && x.ApplicantProfilesId == usercheck.ApplicantProfilesId);

        if (model is null) { return null; }

        var viewModel = ReviewsViewModelsMapping(model);

        return viewModel;
    }

    public async Task<ReviewViewModels?> UpdateReview(string id, ReviewViewModels models, string userid)
    {
        var usercheck = await _db.TblApplicantProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IsDelete == false
            && x.UserId.ToString() == userid);

        if (usercheck == null) return null;

        var item = await _db.TblReviews
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ReviewsId.ToString() == id
            && x.IsDelete == false 
            && x.ApplicantProfilesId == usercheck.ApplicantProfilesId);

        if (item is null) { return null; }

        
        item.Ratings = models.Ratings;
        
        if (!string.IsNullOrEmpty(models.Comments))
        {
            item.Comments = models.Comments;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        models = ReviewsViewModelsMapping(item);

        return models;
    }

    public async Task<ReviewViewModels?> PatchReview(string id, ReviewViewModels models, string userid)
    {
        var usercheck = await _db.TblApplicantProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IsDelete == false
            && x.UserId.ToString() == userid);

        if (usercheck == null) return null;

        var item = await _db.TblReviews
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ReviewsId.ToString() == id
            && x.IsDelete == false 
            && x.ApplicantProfilesId == usercheck.ApplicantProfilesId);

        if (item is null) { return null; }

        if (item.Ratings != 0)
        {
            item.Ratings = models.Ratings;
        }
        if (!string.IsNullOrEmpty(models.Comments))
        {
            item.Comments = models.Comments;
        }
        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        models = ReviewsViewModelsMapping(item);

        return models;
    }

    public async Task<bool?> DeleteReview(string id, string userid)
    {
        var usercheck = await _db.TblApplicantProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IsDelete == false
            && x.UserId.ToString() == userid);

        if (usercheck == null) return null;

        var item = await _db.TblReviews
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ReviewsId.ToString() == id
            && x.IsDelete == false 
            && x.ApplicantProfilesId == usercheck.ApplicantProfilesId);

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

    private static TblReview ReviewsMapping(ReviewViewModels models)
    {
        return new TblReview
        {
            ReviewsId = Guid.NewGuid(),
            CompanyProfilesId = models.CompanyProfilesId,
            ApplicantProfilesId = models.ApplicantProfilesId,
            Ratings = models.Ratings,
            Comments = models.Comments,
            Version = models.Version,
            CreatedAt = models.CreatedAt,
            UpdatedAt = models.UpdatedAt,
            IsDelete = false
        };
    }

    private static ReviewViewModels ReviewsViewModelsMapping(TblReview table)
    {
        return new ReviewViewModels
        {
            //ApplicantEducationsId = Guid.NewGuid(),
            CompanyProfilesId = table.CompanyProfilesId,
            ApplicantProfilesId = table.ApplicantProfilesId,
            Ratings = table.Ratings,
            Comments = table.Comments,
            Version = table.Version,
            CreatedAt = table.CreatedAt,
            UpdatedAt = table.UpdatedAt,
            //IsDelete = false
        };
    }

}
