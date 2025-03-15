namespace online_job_finder.Domain.Services.ApplicantProfileServices;

public class ApplicantProfileRepository : IApplicantProfileRepository
{
    private readonly AppDbContext _db;

    public ApplicantProfileRepository(AppDbContext db)
    {
        _db = db;
    }

    public ApplicantProfileViewModel CreateApplicantProfile(ApplicantProfileViewModel applicantProfile)
    {
        var userItem = applicantProfile.User;

        var Hashedpassword = new PasswordHasher<UsersViewModels>()
        .HashPassword(userItem, userItem.PasswordHash);

        var user = UsersMapping(userItem);
        user.Version += 1;

        _db.TblUsers.Add(user);
        _db.SaveChanges();

        applicantProfile.UserId = user.UserId;

        var applicantProfileItem = ApplicantProfileMapping(applicantProfile);
        user.PasswordHash = Hashedpassword;
        applicantProfileItem.Version += 1;

        _db.TblApplicantProfiles.Add(applicantProfileItem);
        _db.SaveChanges();

        var applicantProfileResponse = ApplicantProfileViewModelMapping(applicantProfileItem);
        applicantProfileResponse.User = UserViewModels(user);

        return applicantProfileResponse;
    }

    public ApplicantProfileViewModel GetApplicantProfile(string applicantId)
    {
        var applicantProfile = _db.TblApplicantProfiles
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefault(x => x.ApplicantProfilesId.ToString() == applicantId
            && x.IsDelete == false);

        if (applicantProfile is null) return null!;

        var applicantProfileResponse = ApplicantProfileViewModelMapping(applicantProfile);
        applicantProfileResponse.User = UserViewModels(applicantProfile.User);

        return applicantProfileResponse;
    }

    public ApplicantProfileViewModel UpdateApplicantProfile(string applicantId, ApplicantProfileViewModel model)
    {
        var applicantProfile = _db.TblApplicantProfiles
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefault(x => x.ApplicantProfilesId.ToString() == applicantId
            && x.IsDelete == false);

        if(applicantProfile is null) return null!;

        var user = applicantProfile.User;

        if (!string.IsNullOrEmpty(model.User.RoleId.ToString()))
        {
            user.RoleId = model.User.RoleId;
        }

        if (!string.IsNullOrEmpty(model.User.Username))
        {
            user.Username = model.User.Username;
        }

        if (!string.IsNullOrEmpty(model.User.ProfilePhoto))
        {
            user.ProfilePhoto = model.User.ProfilePhoto;
        }

        if (!string.IsNullOrEmpty(model.User.Email))
        {
            user.Email = model.User.Email;
        }

        if (!string.IsNullOrEmpty(model.User.PasswordHash))
        {
            user.PasswordHash = model.User.PasswordHash;
        }

        if (!string.IsNullOrEmpty(model.User.IsInformationCompleted.ToString()))
        {
            user.IsInformationCompleted = model.User.IsInformationCompleted;
        }

        user.Version += 1;
        user.UpdatedAt = DateTime.UtcNow;

        if (!string.IsNullOrEmpty(model.LocationId.ToString()))
        {
            applicantProfile.LocationId = model.LocationId;
        }

        if (!string.IsNullOrEmpty(model.FullName))
        {
            applicantProfile.FullName = model.FullName;
        }

        if (!string.IsNullOrEmpty(model.Phone))
        {
            applicantProfile.Phone = model.Phone;
        }

        if (!string.IsNullOrEmpty(model.Address))
        {
            applicantProfile.Address = model.Address;
        }

        if (!string.IsNullOrEmpty(model.Description))
        {
            applicantProfile.Description = model.Description;
        }

        applicantProfile.Version += 1;
        applicantProfile.UpdatedAt = DateTime.UtcNow;

        _db.Entry(user).State = EntityState.Modified;
        _db.Entry(applicantProfile).State = EntityState.Modified;
        _db.SaveChanges();

        var applicantProfileResponse = ApplicantProfileViewModelMapping(applicantProfile);
        applicantProfileResponse.User = UserViewModels(user);

        return applicantProfileResponse;
    }

    public bool DeleteApplicantProfile(string applicantId)
    {
        var applicantProfile = _db.TblApplicantProfiles
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefault(x => x.ApplicantProfilesId.ToString() == applicantId
            && x.IsDelete == false);

        if (applicantProfile is null) return false;

        applicantProfile.Version += 1;
        applicantProfile.UpdatedAt = DateTime.UtcNow;
        applicantProfile.IsDelete = true;

        applicantProfile.User.Version += 1;
        applicantProfile.User.UpdatedAt = DateTime.UtcNow;
        applicantProfile.User.IsDelete = true;

        _db.Entry(applicantProfile).State = EntityState.Modified;
        _db.Entry(applicantProfile.User).State = EntityState.Modified;
        var result = _db.SaveChanges();

        return result > 0;
    }

    public TblApplicantProfile ApplicantProfileMapping(ApplicantProfileViewModel applicantProfile)
    {
        return new TblApplicantProfile
        {
            ApplicantProfilesId = Guid.NewGuid(),
            UserId = applicantProfile.UserId,
            LocationId = applicantProfile.LocationId,
            FullName = applicantProfile.FullName,
            Phone = applicantProfile.Phone,
            Address = applicantProfile.Address,
            Description = applicantProfile.Description,
            Version = applicantProfile.Version,
            CreatedAt = applicantProfile.CreatedAt,
            UpdatedAt = applicantProfile.UpdatedAt,
            IsDelete = false
        };
    }

    public ApplicantProfileViewModel ApplicantProfileViewModelMapping(TblApplicantProfile table)
    {
        return new ApplicantProfileViewModel()
        {
            ApplicantProfilesId = table.ApplicantProfilesId,
            UserId = table.UserId,
            LocationId = table.LocationId,
            FullName = table.FullName,
            Phone = table.Phone,
            Address = table.Address,
            Description = table.Description,
            Version = table.Version,
            CreatedAt = table.CreatedAt,
            UpdatedAt = table.UpdatedAt
        };
    }

    public TblUser UsersMapping(UsersViewModels users)
    {
        return new TblUser
        {
            UserId = Guid.NewGuid(),
            RoleId = users.RoleId,
            Username = users.Username,
            ProfilePhoto = users.ProfilePhoto,
            Email = users.Email,
            PasswordHash = users.PasswordHash,
            IsInformationCompleted = users.IsInformationCompleted,
            Version = users.Version,
            CreatedAt = users.CreatedAt,
            UpdatedAt = users.UpdatedAt,
            IsDelete = false
        };
    }
    public UsersViewModels UserViewModels(TblUser table)
    {
        return new UsersViewModels()
        {
            RoleId = table.RoleId,
            Username = table.Username,
            ProfilePhoto = table.ProfilePhoto,
            Email = table.Email,
            PasswordHash = table.PasswordHash,
            IsInformationCompleted = table.IsInformationCompleted,
            Version = table.Version,
            CreatedAt = table.CreatedAt,
            UpdatedAt = table.UpdatedAt,
        };
    }
}
