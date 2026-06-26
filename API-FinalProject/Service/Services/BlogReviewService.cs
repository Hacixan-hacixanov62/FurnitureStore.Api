using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interface;
using Service.DTO.UI.BlogReview;
using Service.DTO.UI.Review;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class BlogReviewService : IBlogReviewService
    {
        private readonly IBlogReviewRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public BlogReviewService(IBlogReviewRepository repository,
                             UserManager<AppUser> userManager,
                             IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogReviewDto>> GetAllReviewsAsync()
        {
            var reviews = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BlogReviewDto>>(reviews);
        }

        public async Task<IEnumerable<BlogReviewDto>> GetReviewsByPostIdAsync(int postId)
        {
            var reviews = await _repository.GetByPostIdAsync(postId);
            return _mapper.Map<IEnumerable<BlogReviewDto>>(reviews);
        }

        public async Task<BlogReviewDto> GetReviewByIdAsync(int id)
        {
            var review = await _repository.GetByIdAsync(id);
            return _mapper.Map<BlogReviewDto>(review);
        }

        public async Task<bool> CreateReviewAsync(string userEmail, BlogReviewCreateDto dto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return false;

            var review = _mapper.Map<BlogReview>(dto);
            review.AppUserId = user.Id;

            await _repository.CreateAsync(review);
            return true;
        }
        public async Task<bool> EditReviewAsync(int reviewId, BlogReviewEditDto dto)
        {
            var review = await _repository.GetByIdAsync(reviewId);
            if (review == null) return false;

            if (review.AppUserId != dto.AppUserId) return false;

            var oldComment = review.Comment;
            _mapper.Map(dto, review);
            var newComment = review.Comment;

            await _repository.EditAsync(review);
            return true;
        }
        public async Task<bool> DeleteReviewAsync(string userEmail, int reviewId)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null) return false;

            var review = await _repository.GetByIdAsync(reviewId);
            if (review == null) return false;

            if (review.AppUserId != user.Id) return false;

            await _repository.DeleteAsync(review);
            return true;
        }




        public async Task DeleteAsync(int reviewId)
        {
            var review = await _repository.GetByIdAsync(reviewId);
            if (review == null) throw new KeyNotFoundException("Not found");
            await _repository.DeleteAsync(review);
        }

        public async Task<IEnumerable<BlogReviewDto>> GetAllByPostIdAsync(int postId)
        {
            var reviews = await _repository.GetAllByPostAsync(postId);
            return _mapper.Map<IEnumerable<BlogReviewDto>>(reviews);
        }
    }
}
