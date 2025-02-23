using Microsoft.EntityFrameworkCore;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.Services.JobCategoryServices;
using online_job_finder.Domain.Services.LocationServices;
using online_job_finder.Domain.Services.RoleServices;
using online_job_finder.Domain.Services.SkillServices;
using online_job_finder.Domain.Services.UsersServices;

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// I add some folders in here
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<SkillRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();

builder.Services.AddScoped<LocationRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();

builder.Services.AddScoped<JobCategoryRepository>();
builder.Services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
