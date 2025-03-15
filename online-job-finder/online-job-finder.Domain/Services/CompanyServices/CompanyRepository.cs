using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace online_job_finder.Domain.Services.CompanyServices;

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _db;

    public CompanyRepository(AppDbContext db)
    {
        _db = db;
    }

    public CompanyUsersViewModels CreateCompany(CompanyUsersViewModels reqModel)
    {
        var userItem = reqModel.User;

        var Hashedpassword = new PasswordHasher<UsersViewModels>()
        .HashPassword(userItem, userItem.PasswordHash);

        var user = UsersMapping(reqModel);
        user.PasswordHash = Hashedpassword;
        user.Version += 1;
        var model = _db.TblUsers.Add(user);
        _db.SaveChanges();

        reqModel.UserId = user.UserId;
        var company = CompanyMapping(reqModel);
        company.Version += 1;
        var result = _db.TblCompanyProfiles.Add(company);
        _db.SaveChanges();

        var companyUserItem = CompanyViewMapping(company);
        companyUserItem.User = UsersViewMapping(user);


        return companyUserItem;

    }

    public List<CompanyUsersViewModels> GetCompanies()
    {
        var companyItems = _db.TblCompanyProfiles
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x => x.IsDelete == false).ToList();

        if (companyItems == null) return null!;

        var companyResponse = new List<CompanyUsersViewModels>();

        foreach (var a in companyItems)
        {
            var companyUserItem = CompanyViewMapping(a);
            companyUserItem.User = UsersViewMapping(a.User);
            companyResponse.Add(companyUserItem);
        }

        return companyResponse;
    }

    public CompanyUsersViewModels GetCompanyById(string userId)
    {
        var companyItems = _db.TblCompanyProfiles
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefault(x => x.UserId.ToString() == userId
            && x.IsDelete == false);

        if (companyItems == null) return null!;

        var companyUserItem = CompanyViewMapping(companyItems);
        companyUserItem.User = UsersViewMapping(companyItems.User);
        return companyUserItem;
    }


    //Mappings

    private static TblUser UsersMapping(CompanyUsersViewModels reqModel)
    {
        return new TblUser
        {
            UserId = Guid.NewGuid(),
            RoleId = reqModel.User.RoleId,
            Username = reqModel.User.Username,
            ProfilePhoto = reqModel.User.ProfilePhoto,
            Email = reqModel.User.Email,
            PasswordHash = reqModel.User.PasswordHash,
            IsInformationCompleted = reqModel.User.IsInformationCompleted,
            Version = reqModel.User.Version,
            CreatedAt = reqModel.User.CreatedAt,
            UpdatedAt = reqModel.User.UpdatedAt,
            IsDelete = false
        };
    }


    private static UsersViewModels UsersViewMapping(TblUser reqModel)
    {
        return new UsersViewModels
        {
            RoleId = reqModel.RoleId,
            Username = reqModel.Username,
            ProfilePhoto = reqModel.ProfilePhoto,
            Email = reqModel.Email,
            PasswordHash = reqModel.PasswordHash,
            IsInformationCompleted = reqModel.IsInformationCompleted,
            Version = reqModel.Version,
            CreatedAt = reqModel.CreatedAt,
            UpdatedAt = reqModel.UpdatedAt
        };
    }


    private static TblCompanyProfile CompanyMapping(CompanyUsersViewModels reqModel)
    {
        return new TblCompanyProfile
        {
            CompanyProfilesId = Guid.NewGuid(),
            UserId = reqModel.UserId,
            LocationId = reqModel.LocationId,
            CompanyName = reqModel.CompanyName,
            Phone = reqModel.Phone,
            Website = reqModel.Website,
            Address = reqModel.Address,
            Description = reqModel.Description,
            Version = reqModel.Version,
            CreatedAt = reqModel.CreatedAt,
            UpdatedAt = reqModel.UpdatedAt,
            IsDelete = false
        };
    }

    private static CompanyUsersViewModels CompanyViewMapping(TblCompanyProfile reqModel )
    {
        return new CompanyUsersViewModels
        {
            CompanyProfilesId = reqModel.CompanyProfilesId,
            UserId = reqModel.UserId,
            LocationId = reqModel.LocationId,
            CompanyName = reqModel.CompanyName,
            Phone = reqModel.Phone,
            Website = reqModel.Website,
            Address = reqModel.Address,
            Description = reqModel.Description,
            Version = reqModel.Version,
            CreatedAt = reqModel.CreatedAt,
            UpdatedAt = reqModel.UpdatedAt
        };
    }
}
