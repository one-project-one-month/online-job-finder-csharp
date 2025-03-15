namespace online_job_finder.Domain.Services.Applicant_SkillsServices;

public class Applicant_SkillsRepository : IApplicant_SkillsRepository
{
    private readonly AppDbContext _db;

    public Applicant_SkillsRepository()
    {
        _db = new AppDbContext();
    }

    public async Task<Applicant_SkillsViewModels> CreateApplicant_Skills(Applicant_SkillsViewModels models, string userid)
    {
        var usercheck = await _db.TblApplicantProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IsDelete == false
            && x.UserId.ToString() == userid);

        if (usercheck == null) return null;

        models.ApplicantProfilesId = usercheck.ApplicantProfilesId;
        models.Version += 1;
        models.UpdatedAt = null;

        var item = new TblApplicantSkill();
        item.ApplicantProfilesId = models.ApplicantProfilesId;
        item.ExtraSkills = models.ExtraSkills;
        item.Version = models.Version;
        item.CreatedAt = models.CreatedAt;
        item.IsDelete = false;

        for (int i = 0; i < models.SkillsIds.Count; i++)
        {
            item.ApplicantSkillsId = Guid.NewGuid();
            item.SkillsId = models.SkillsIds[i];
            await _db.TblApplicantSkills.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        return models;
    }

    public async Task<List<Applicant_SkillsViewModels>> GetApplicant_Skills(string userid)
    {
        var usercheck = await _db.TblApplicantProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IsDelete == false
            && x.UserId.ToString() == userid);

        if (usercheck == null) return null;

        var skills = await _db.TblApplicantSkills
            .AsNoTracking()
            .Where(x => x.IsDelete == false
            && x.ApplicantProfilesId == usercheck.ApplicantProfilesId)
            .OrderBy(x => x.Version)
            .ToListAsync();


        //var skillIds = skills.Select(x => x.SkillsId).ToList();

        //var viewModels = skills.Select(Applicant_SkillsViewModelsMapping).ToList();

        var groupedSkills = skills
            .GroupBy(x => x.ApplicantProfilesId)  // Group by ApplicantProfilesId
            .Select(g => new Applicant_SkillsViewModels
            {
                SkillsIds = g.Select(x => x.SkillsId)  // Collect all SkillsId for the applicant
                                .Distinct()  // Ensure no duplicates
                                .ToList(),
                ApplicantProfilesId = g.Key,  // The ApplicantProfilesId for the group
                ExtraSkills = g.FirstOrDefault()?.ExtraSkills,
                Version = g.FirstOrDefault()!.Version,
                CreatedAt = g.FirstOrDefault()!.CreatedAt,
                UpdatedAt = g.FirstOrDefault()?.UpdatedAt
            })
            .ToList();

        return groupedSkills;
    }

    //private static TblApplicantSkill Applicant_SkillssMapping(Applicant_SkillsViewModels models)
    //{

    //    return new TblApplicantSkill
    //    {

    //    };
    //}

    //private static Applicant_SkillsViewModels Applicant_SkillsViewModelsMapping(TblApplicantSkill table)
    //{
    //    return new Applicant_SkillsViewModels
    //    {
    //        //ApplicantSkillsId = Guid.NewGuid(),
    //        SkillsIds = new List<Guid> { table.SkillsId },
    //        ApplicantProfilesId = table.ApplicantProfilesId,
    //        ExtraSkills = table.ExtraSkills,
    //        Version = table.Version,
    //        CreatedAt = table.CreatedAt,
    //        UpdatedAt = table.UpdatedAt,
    //        //IsDelete = false
    //    };
    //}
}
