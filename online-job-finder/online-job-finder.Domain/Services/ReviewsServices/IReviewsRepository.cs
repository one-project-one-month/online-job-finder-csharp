namespace online_job_finder.Domain.Services.ReviewsServices;

public interface IReviewsRepository
{
    Task<ReviewViewModels> CreateReview(ReviewViewModels model, string userid);
    Task<bool?> DeleteReview(string id, string userid);
    Task<ReviewViewModels?> GetReview(string id, string userid);
    Task<List<ReviewViewModels>> GetReviews(string userid);
    Task<ReviewViewModels?> PatchReview(string id, ReviewViewModels models, string userid);
    Task<ReviewViewModels?> UpdateReview(string id, ReviewViewModels models, string userid);
}