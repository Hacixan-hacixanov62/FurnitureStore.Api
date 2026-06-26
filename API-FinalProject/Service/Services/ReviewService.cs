
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interface;
using Service.DTO.UI.Review;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository repository,
                             UserManager<AppUser> userManager,
                             IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            var reviews = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(int productId)
        {
            var reviews = await _repository.GetByProductIdAsync(productId);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto> GetReviewByIdAsync(int id)
        {
            var review = await _repository.GetByIdAsync(id);
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<bool> CreateReviewAsync(string userEmail, ReviewCreateDto dto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return false;

            var review = _mapper.Map<Review>(dto);
            review.AppUserId = user.Id; 

            await _repository.CreateAsync(review);
            return true;
        }
        public async Task<bool> EditReviewAsync(int reviewId, ReviewEditDto dto)
        {
            var review = await _repository.GetByIdAsync(reviewId);
            if (review == null) return false;

            if (review.AppUserId != dto.AppUserId) return false;

            var oldComment = review.Comment;
            _mapper.Map(dto, review);
            var newComment = review.Comment;

            Console.WriteLine($"Old: {oldComment}, New: {newComment}");

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

        public async Task<IEnumerable<ReviewDto>> GetAllByProductIdAsync(int productId)
        {
            var reviews = await _repository.GetAllByProductIdAsync(productId);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }
    }
}
