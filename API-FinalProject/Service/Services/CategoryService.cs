using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Repository.Repositories.Interfaces;
using Service.DTO.Admin.Category;
using Service.DTOs.Admin.Products;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository,
                             IFileService fileService,
                             IMapper mapper)

        {
            _categoryRepository = categoryRepository;    
            _fileService = fileService;
            _mapper = mapper;
        }
        public async Task CreateAsync(CategoryCreateDto model)
        {
            var categoryExists = await _categoryRepository.GetAllWithExpressionAsync(x => x.Name.Trim().ToLower() == model.Name.Trim().ToLower());
            if (categoryExists.ToList().Count > 0) throw new ArgumentException("This category has already exist");
            string imageUrl = await _fileService.UploadFileAsync(model.Image, "categories");
            Category category = _mapper.Map<Category>(model);
            category.Image = imageUrl;
            await _categoryRepository.CreateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException($"Category with ID {id} not found.");

            if (!string.IsNullOrEmpty(category.Image))
            {
                string fileName = Path.GetFileName(category.Image);
                _fileService.Delete(fileName, "categories");
            }
            await _categoryRepository.DeleteAsync(category);
        }

        //public async Task EditAsync(CategoryEditDto model, int id)
        //{
        //  var category = await _categoryRepository.GetAllWithExpressionAsync(x => x.Name.ToLower() == model.Name.ToLower());
        //  if (category.ToList().Count > 0) throw new ArgumentException("This category has already exist");

        //    Category existCategory = await _categoryRepository.GetByIdAsync(id);
        //    if (existCategory == null) throw new KeyNotFoundException($"Category with ID {id} not found.");

        //    if (model.Image != null)
        //    {
        //        if (!string.IsNullOrEmpty(existCategory.Image))
        //        {
        //            string oldFileName = Path.GetFileName(existCategory.Image);
        //            _fileService.Delete(oldFileName, "categories");
        //        }
        //        string newImage = await _fileService.UploadFileAsync(model.Image, "categories");
        //        existCategory.Image = newImage;
        //    }
        //    _mapper.Map(model, existCategory);
        //    await _categoryRepository.EditAsync(existCategory);
        //}


        public async Task EditAsync(CategoryEditDto model, int id)
        {
            Category existCategory = await _categoryRepository.GetByIdAsync(id);
            if (existCategory == null)
                throw new KeyNotFoundException($"Category with ID {id} not found.");

            if (!string.Equals(existCategory.Name.Trim(), model.Name.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                var sameNameCategory = await _categoryRepository
                    .GetAllWithExpressionAsync(x => x.Name.Trim().ToLower() == model.Name.Trim().ToLower() && x.Id != id);

                if (sameNameCategory.Any())
                    throw new ArgumentException("This category name already exists.");
            }

            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(existCategory.Image))
                {
                    string oldFileName = Path.GetFileName(existCategory.Image);
                    _fileService.Delete(oldFileName, "categories");
                }

                string newImage = await _fileService.UploadFileAsync(model.Image, "categories");
                existCategory.Image = newImage;
            }

            _mapper.Map(model, existCategory);
            await _categoryRepository.EditAsync(existCategory);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(await _categoryRepository.GetAllAsync());
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException($"Category with ID {id} not found.");
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<PaginationResponse<CategoryDto>> GetPaginateAsync(int page, int take)
        {
            var products = _categoryRepository.GetAllWithExpression(null);
            int totalItemCount = products.Count();

            var paginated = products
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();

            var mappedDatas = _mapper.Map<IEnumerable<CategoryDto>>(paginated);

            int totalPage = (int)Math.Ceiling((decimal)totalItemCount / take);

            return new PaginationResponse<CategoryDto>(mappedDatas, totalPage, page, totalItemCount);
        }

        public async Task<Dictionary<string, int>> GetCategoryProductCountsAsync()
        {
            return await _categoryRepository.GetCategoryProductCountsAsync();
        }
    }
}
