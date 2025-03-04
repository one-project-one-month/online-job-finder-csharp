using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.Services.CompanyProfileServices;
using online_job_finder.Domain.Services.JobCategoryServices;
using online_job_finder.Domain.Services.JobServices;
using online_job_finder.Domain.Services.LocationServices;
using online_job_finder.Domain.Services.RoleServices;
using online_job_finder.Domain.Services.SkillServices;
using online_job_finder.Domain.Services.UsersServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



//builder.Services.AddDbContext<AppDbContext>(options =>
//        options.UseSqlServer(builder.Configuration.GetConnectionString
//        ("MSSQLConnection")));

var databaseType = builder.Configuration["DatabaseType"] ?? "MSSQL"; // Default to MSSQL

if (databaseType == "MSSQL")
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection")));
}
else
if (databaseType == "MySQL")
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("MySQLConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySQLConnection"))));
}



// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                      "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                      "Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});


//builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

//builder.Services.AddScoped<SkillRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();

//builder.Services.AddScoped<LocationRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();

//builder.Services.AddScoped<JobCategoryRepository>();
builder.Services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();

builder.Services.AddScoped<ICompanyProfileServices, CompanyProfileServices>();
builder.Services.AddHttpContextAccessor();


// I add some folders in here
builder.Services.AddScoped<JobRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
