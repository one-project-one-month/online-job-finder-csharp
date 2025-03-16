using Microsoft.AspNetCore.Http;

namespace online_job_finder.Domain.Services.UploadImage;

public interface IUploadImageRepository
{
    string UploadImage(IFormFile file);
}
