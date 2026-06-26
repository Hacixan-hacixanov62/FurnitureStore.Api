using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTO.UI.BlogReview;
using Service.DTO.UI.Review;

namespace Service.Services.Interfaces
{
    public interface IBlogReviewService
    {
        Task<IEnumerable<BlogReviewDto>> GetAllReviewsAsync();
        Task<IEnumerable<BlogReviewDto>> GetAllByPostIdAsync(int postId);
        Task<IEnumerable<BlogReviewDto>> GetReviewsByPostIdAsync(int postId);
        Task DeleteAsync(int reviewId);


        Task<BlogReviewDto> GetReviewByIdAsync(int id);
        Task<bool> CreateReviewAsync(string userEmail, BlogReviewCreateDto dto);
        Task<bool> EditReviewAsync(int id, BlogReviewEditDto dto);
        Task<bool> DeleteReviewAsync(string userEmail, int reviewId);
    }
}
