using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace online_job_finder.Domain.Services.UsersServices;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _configuration;

    public UserRepository(AppDbContext db, IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
    }

    #region Authentication & Authorization

    public async Task<UsersViewModels?> RegisterAsync(UsersViewModels request)
    {
        if (await _db.TblUsers.AnyAsync(u =>
            u.Username == request.Username || u.Email == request.Email)) //Not sure && u.IsDelete == false 
        {
            return null;
        }

        var roleCheck = await _db.TblRoles
            .AsNoTracking()
            .FirstOrDefaultAsync(r =>
            r.RoleId == request.RoleId && r.IsDelete == false);

        if (roleCheck is null)
        {
            return null;
        }

        var Hashedpassword = new PasswordHasher<UsersViewModels>()
        .HashPassword(request, request.PasswordHash);

        var user = UsersMapping(request);
        user.PasswordHash = Hashedpassword;
        user.Version += 1;

        _db.TblUsers.Add(user);
        await _db.SaveChangesAsync();

        request = UsersViewModelsMapping(user);

        return request;
    }

    public async Task<TokenResponse?> LoginAsync(ViewModels.LoginRequest request)
    {
        var user = await _db.TblUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user is null)
        {
            return null;
        }

        if (new PasswordHasher<TblUser>().VerifyHashedPassword
            (user, user.PasswordHash, request.Password)
            == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return await CreateTokenResponse(user);
    }

    public async Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var user = await ValidateRefreshTokeAsync
            (request.UserId, request.RefreshToken);
        if (user is null)
            return null;

        return await CreateTokenResponse(user);
    }


    private async Task<TokenResponse> CreateTokenResponse(TblUser user)
    {
        return new TokenResponse
        {
            AccessToken = CreateToken(user!),
            RefreshToken = await GenerateAndSaveRefreshToken(user!)
        };
    }

    private async Task<TblUser?> ValidateRefreshTokeAsync(Guid userId, string refreshToken)
    {
        var user = await _db.TblUsers.FindAsync(userId);
        if (user is null || user.RefreshToken != refreshToken
            || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }

    private string GenerateRefreshToken()
    {
        var ramdomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(ramdomNumber);
        return Convert.ToBase64String(ramdomNumber);
    }

    private async Task<string> GenerateAndSaveRefreshToken(TblUser user)
    {
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        _db.Entry(user).State = EntityState.Modified; // should not save entire user
        await _db.SaveChangesAsync();
        return refreshToken;
    }

    private string CreateToken(TblUser user)
    {
        var roles = _db.TblRoles
            .AsNoTracking()
            .FirstOrDefault(r => r.RoleId == user.RoleId)!;

        if (roles is null)
        {
            return null!;
        }

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Role, roles.RoleName),
        new Claim(ClaimTypes.Name, user.Username)
    };

        var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));
        var creds = new SigningCredentials
            (key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
            audience: _configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }


    #endregion

    #region CRUD
    public List<UsersViewModels> GetUsers()
    {
        var model = _db.TblUsers
            .AsNoTracking()
            .Where(x => x.IsDelete == false)
            .OrderBy(x => x.Version)
            .ToList();

        var viewModels = model.Select(UsersViewModelsMapping).ToList();

        return viewModels;
    }

    public UsersViewModels? GetUser(string id)
    {
        var model = _db.TblUsers
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId.ToString() == id
            && x.IsDelete == false);

        if (model is null) { return null; }

        var viewModel = UsersViewModelsMapping(model);

        return viewModel;
    }

    public UsersViewModels? UpdateUser(string id, UsersViewModels models)
    {
        var item = _db.TblUsers
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId.ToString() == id
            && x.IsDelete == false);
        if (item is null) { return null; }

        if (!string.IsNullOrEmpty(models.RoleId.ToString()))
        {
            item.RoleId = models.RoleId;
        }

        if (!string.IsNullOrEmpty(models.Username))
        {
            item.Username = models.Username;
        }

        if (!string.IsNullOrEmpty(models.ProfilePhoto))
        {
            item.ProfilePhoto = models.ProfilePhoto;
        }

        if (!string.IsNullOrEmpty(models.Email))
        {
            item.Email = models.Email;
        }

        if (!string.IsNullOrEmpty(models.PasswordHash))
        {
            item.PasswordHash = models.PasswordHash;
        }

        if (!string.IsNullOrEmpty(models.IsInformationCompleted.ToString()))
        {
            item.IsInformationCompleted = models.IsInformationCompleted;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        _db.SaveChanges();

        models = UsersViewModelsMapping(item);

        return models;
    }

    public UsersViewModels? PatchUser(string id, UsersViewModels models)
    {
        var item = _db.TblUsers
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId.ToString() == id
            && x.IsDelete == false);
        if (item is null) { return null; }

        if (!string.IsNullOrEmpty(models.RoleId.ToString()))
        {
            item.RoleId = models.RoleId;
        }

        if (!string.IsNullOrEmpty(models.Username))
        {
            item.Username = models.Username;
        }

        if (!string.IsNullOrEmpty(models.ProfilePhoto))
        {
            item.ProfilePhoto = models.ProfilePhoto;
        }

        if (!string.IsNullOrEmpty(models.Email))
        {
            item.Email = models.Email;
        }

        if (!string.IsNullOrEmpty(models.PasswordHash))
        {
            item.PasswordHash = models.PasswordHash;
        }

        if (!string.IsNullOrEmpty(models.IsInformationCompleted.ToString()))

            item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        _db.SaveChanges();

        models = UsersViewModelsMapping(item);

        return models;
    }

    public bool? DeleteUser(string id)
    {
        var item = _db.TblUsers
            .AsNoTracking()
            .FirstOrDefault(x => x.UserId.ToString() == id
            && x.IsDelete == false);
        if (item is null)
        {
            return null;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;
        item.IsDelete = true;

        _db.Entry(item).State = EntityState.Modified;
        var result = _db.SaveChanges();

        return result > 0;
    }

    #endregion

    //Can use for every roles
    private static TblUser UsersMapping(UsersViewModels viewModels)
    {
        return new TblUser
        {
            UserId = Guid.NewGuid(),
            RoleId = viewModels.RoleId,
            Username = viewModels.Username,
            ProfilePhoto = viewModels.ProfilePhoto,
            Email = viewModels.Email,
            PasswordHash = viewModels.PasswordHash,
            IsInformationCompleted = viewModels.IsInformationCompleted,
            Version = viewModels.Version,
            CreatedAt = viewModels.CreatedAt,
            UpdatedAt = viewModels.UpdatedAt,
            IsDelete = false
        };

    }

    private static UsersViewModels UsersViewModelsMapping(TblUser table)
    {
        return new UsersViewModels
        {
            //UserId = Guid.NewGuid(),
            RoleId = table.RoleId,
            Username = table.Username,
            ProfilePhoto = table.ProfilePhoto,
            Email = table.Email,
            PasswordHash = table.PasswordHash,
            IsInformationCompleted = table.IsInformationCompleted,
            Version = table.Version,
            CreatedAt = table.CreatedAt,
            UpdatedAt = table.UpdatedAt,
            //IsDelete = false
        };
    }

}
