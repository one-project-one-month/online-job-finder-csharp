using System;

namespace online_job_finder.Domain.Services.CompanyProfileServices.CompanyProfileResponses;

public class CompanyReviewsViewModel
{
    public List<ReviewViewModel> Reviews { get; set; }
    public double AverageRating { get; set; }
    public int TotalReviews { get; set; }
}
