using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interface;
using Service.DTO.Admin.BlogPost;
using Service.DTOs.Admin.Products;
using Service.Helpers;
using Service.Services;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _postRepository;
        private readonly IBlogCategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public BlogPostService(IBlogPostRepository postRepository,
                               IBlogCategoryRepository blogCategoryRepository,
                               IWebHostEnvironment env,
                               IFileService fileService,
                               IMapper mapper)
        {
            _postRepository = postRepository;
            _categoryRepository = blogCategoryRepository;
            _env = env;
            _fileService = fileService; 
            _mapper = mapper;
        }

        public async Task CreateAsync(BlogPostCreateDto model)
        {
            var categoryExists = await _categoryRepository.GetByIdAsync(model.BlogCategoryId);
            if (categoryExists == null)
            {
                throw new KeyNotFoundException($"Category with ID {model.BlogCategoryId} not found.");
            }

            var blogPost = _mapper.Map<BlogPost>(model);
            await _postRepository.CreateAsync(blogPost);

            if (model.Images != null && model.Images.Any())
            {
                blogPost.Images = new List<BlogPostImg>();

                foreach (var file in model.Images)
                {
                    string imageUrl = await _fileService.UploadFileAsync(file, "blogposts");
                    blogPost.Images.Add(new BlogPostImg { Image = imageUrl, BlogPostId = blogPost.Id });
                }

                var firstImage = blogPost.Images.FirstOrDefault();
                if (firstImage != null)
                {
                    firstImage.IsMain = true;
                }

                await _postRepository.EditAsync(blogPost);
            }
        }

       
        public async Task EditAsync(int id, BlogPostEditDto model)
        {
            var blogPost = await _postRepository.GetByIdWithIncludesAsync(id);
            if (blogPost == null) throw new KeyNotFoundException($"Blog post with ID {id} not found.");
            var categoryExists = await _categoryRepository.GetByIdAsync(model.BlogCategoryId);
            if (categoryExists == null) throw new KeyNotFoundException($"Category with ID {model.BlogCategoryId} not found.");
            _mapper.Map(model, blogPost);
            blogPost.Images ??= new List<BlogPostImg>();

            if (model.Images != null && model.Images.Any())
            {
                foreach (var file in model.Images)
                {
                    string imageUrl = await _fileService.UploadFileAsync(file, "blogposts");
                    blogPost.Images.Add(new BlogPostImg { Image = imageUrl, BlogPostId = blogPost.Id, IsMain = false });
                }
            }
            foreach (var img in blogPost.Images)
            {
                img.IsMain = img.Id == model.MainImageId;
            }
            await _postRepository.EditAsync(blogPost);
        }


        public async Task DeleteAsync(int id)
        {
            var blogPost = await _postRepository.GetByIdWithIncludesAsync(id);
            if (blogPost == null) throw new KeyNotFoundException($"Blog post with ID {id} not found.");

            await _postRepository.DeleteAsync(blogPost);
        }

        public async Task<IEnumerable<BlogPostDto>> GetAllAsync()
        {
            var blogPosts = await _postRepository.ApplyIncludes().ToListAsync();
            return _mapper.Map<IEnumerable<BlogPostDto>>(blogPosts);
        }

        public async Task<BlogPostDto> GetByIdAsync(int id)
        {
            var blogPost = await _postRepository.ApplyIncludes().FirstOrDefaultAsync(bp => bp.Id == id); 
            if (blogPost == null) throw new KeyNotFoundException($"Blog post with ID {id} not found.");
            return _mapper.Map<BlogPostDto>(blogPost);
        }

       
        public async Task<bool> DeleteImageAsync(int blogPostId, int blogPostImageId)
        {
            var blogPost = await _postRepository.GetByIdWithIncludesAsync(blogPostId);
            if (blogPost == null) return false;

            var image = blogPost.Images.FirstOrDefault(i => i.Id == blogPostImageId);
            if (image == null) return false;

            string imageName = Path.GetFileName(image.Image);
            string filePath = Path.Combine(_env.WebRootPath, "Uploads", "blogposts", imageName);
            if (File.Exists(filePath))
            {
               _fileService.Delete(imageName, "blogposts");
            }
            blogPost.Images.Remove(image);
            if (image.IsMain && blogPost.Images.Any())
            {
                blogPost.Images.First().IsMain = true;
            }
            await _postRepository.EditAsync(blogPost);
            return true;
        }

        public async Task<PaginationResponse<BlogPostDto>> GetPaginateAsync(int page, int take)
        {
            var products = _postRepository.GetAllWithExpression(null);
            int totalItemCount = products.Count();
            var paginated = products
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();
            var mappedDatas = _mapper.Map<IEnumerable<BlogPostDto>>(paginated);
            int totalPage = (int)Math.Ceiling((decimal)totalItemCount / take);
            return new PaginationResponse<BlogPostDto>(mappedDatas, totalPage, page, totalItemCount);
        }

        public async Task<IEnumerable<BlogPostDto>> SearchByCategoryAndName(string categoryOrProductName)
        {
            if (string.IsNullOrWhiteSpace(categoryOrProductName))
            {
                var allPosts = await  _postRepository.GetAllWithIncludeAsync();
                return _mapper.Map<IEnumerable<BlogPostDto>>(allPosts);
            }
            var keyword = categoryOrProductName.ToLower().Trim();

            var products = _postRepository.GetAllWithExpression(x =>
                x.Title.ToLower().Contains(keyword) ||
                x.BlogCategory.Name.ToLower().Contains(keyword));

            return _mapper.Map<IEnumerable<BlogPostDto>>(products);
        }


        public async Task<IEnumerable<BlogPostDto>> FilterAsync(string categoryName)
        {
            return _mapper.Map<IEnumerable<BlogPostDto>>(await _postRepository.FilterAsync(categoryName));

        }

        public async Task<BlogPostDto> GetPreviousAsync(int currentId)
        {
            var previous = await _postRepository.GetPreviousAsync(currentId);
            return _mapper.Map<BlogPostDto>(previous);
        }

        public async Task<BlogPostDto> GetNextAsync(int currentId)
        {
            var next = await _postRepository.GetNextAsync(currentId);
            return _mapper.Map<BlogPostDto>(next);
        }
    }
}

