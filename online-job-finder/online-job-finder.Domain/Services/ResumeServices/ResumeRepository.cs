using online_job_finder.Domain.Services.UploadImage;

namespace online_job_finder.Domain.Services.ResumeServices;

public class ResumeRepository : IResumeRepository
{
    private readonly AppDbContext _db;
    private readonly IUploadImageRepository _uploadImageRepository;

    public ResumeRepository(AppDbContext db, IUploadImageRepository uploadImageRepository)
    {
        _db = db;
        _uploadImageRepository = uploadImageRepository;
    }

    public ResumeViewModel CreateResume(ResumeViewModel requestResume)
    {
        var filepath = _uploadImageRepository.UploadImage(requestResume.resumeFile);
        requestResume.FilePath = filepath;

        var resume = ResumeMapping(requestResume);
        resume.Version += 1;

        _db.TblResumes.Add(resume);
        _db.SaveChanges();

        var responseResume = ResumeViewModelMapping(resume);

        return responseResume;
    }

    public List<ResumeViewModel> GetResumes(string applicantId)
    {
        var resume = _db.TblResumes
            .AsNoTracking()
            .Where(x => x.UserId.ToString() == applicantId
            && x.IsDelete == false).ToList();

        if (resume is null) return null!;

        var resumeResponse = new List<ResumeViewModel>();

        foreach (var r in resume)
        {
            var mappedResume = ResumeViewModelMapping(r);
            resumeResponse.Add(mappedResume);
        }

        return resumeResponse;
    }

    public ResumeViewModel GetResume(string applicantId, string resumeId)
    {
        var resume = _db.TblResumes
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId.ToString() == applicantId
            && x.ResumesId.ToString() == resumeId
            && x.IsDelete == false);

        if (resume is null) return null!;

        var resumeResponse = ResumeViewModelMapping(resume);

        return resumeResponse;
    }

    public ResumeViewModel UpdateResume(string applicantId, string resumeId, ResumeViewModel requestResume)
    {
        var resume = _db.TblResumes
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId.ToString() == applicantId
            && x.ResumesId.ToString() == resumeId
            && x.IsDelete == false);

        if (resume is null) return null!;

        if (!string.IsNullOrEmpty(requestResume.resumeFile.ToString()))
        {
            var filepath = _uploadImageRepository.UploadImage(requestResume.resumeFile);
            resume.FilePath = filepath;

        }

        resume.Version += 1;
        resume.UpdatedAt = DateTime.UtcNow;

        _db.Entry(resume).State = EntityState.Modified;
        _db.SaveChanges();

        var resumeResponse = ResumeViewModelMapping(resume);

        return resumeResponse;
    }

    public bool DeleteResume(string applicantId, string resumeId)
    {
        var resume = _db.TblResumes
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId.ToString() == applicantId
            && x.ResumesId.ToString() == resumeId
            && x.IsDelete == false);

        if (resume is null) return false;

        resume.Version += 1;
        resume.UpdatedAt = DateTime.UtcNow;
        resume.IsDelete = true;

        _db.Entry(resume).State = EntityState.Modified;
        var result = _db.SaveChanges();

        return result > 0;
    }

    public TblResume ResumeMapping(ResumeViewModel requestResume)
    {
        return new TblResume
        {
            ResumesId = Guid.NewGuid(),
            UserId = requestResume.UserId,
            FilePath = requestResume.FilePath,
            Version = requestResume.Version,
            CreatedAt = requestResume.CreatedAt,
            UpdatedAt = requestResume.UpdatedAt,
            IsDelete = false
        };
    }

    public ResumeViewModel ResumeViewModelMapping(TblResume table)
    {
        return new ResumeViewModel()
        {
            UserId = table.UserId,
            FilePath = table.FilePath,
            Version = table.Version,
            CreatedAt = table.CreatedAt,
            UpdatedAt = table.UpdatedAt
        };
    }
}