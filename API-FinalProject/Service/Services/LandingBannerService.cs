using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Service.DTO.Admin.Category;
using Service.DTO.Admin.LandingBanner;
using Service.DTO.Admin.Sliders;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class LandingBannerService : ILandingBannerService
    {
        private readonly ILandingBannerRepository _landingBannerRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public LandingBannerService(ILandingBannerRepository landingBannerRepository,
                             IFileService fileService,
                             IMapper mapper)

        {
            _landingBannerRepository = landingBannerRepository;
            _fileService = fileService;
            _mapper = mapper;
        }
        public async Task CreateAsync(LandingBannerCreateDto model)
        {
            string imageUrl = await _fileService.UploadFileAsync(model.Image, "landingbanners");
            LandingBanner landingBanners = _mapper.Map<LandingBanner>(model);
            landingBanners.Image = imageUrl;
            await _landingBannerRepository.CreateAsync(landingBanners);
        }

        public async Task DeleteAsync(int id)
        {
            LandingBanner landingBanners = await _landingBannerRepository.GetByIdAsync(id);
            if (landingBanners == null) throw new KeyNotFoundException($"Banner with ID {id} not found.");

            if (!string.IsNullOrEmpty(landingBanners.Image))
            {
                string fileName = Path.GetFileName(landingBanners.Image);
                _fileService.Delete(fileName, "landingbanners");
            }
            await _landingBannerRepository.DeleteAsync(landingBanners);
        }

        public async Task EditAsync(LandingBannerEditDto model, int id)
        {
            LandingBanner existBanner = await _landingBannerRepository.GetByIdAsync(id);
            if (existBanner == null) throw new KeyNotFoundException($"Banner with ID {id} not found.");

            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(existBanner.Image))
                {
                    string oldFileName = Path.GetFileName(existBanner.Image);
                    _fileService.Delete(oldFileName, "landingbanners");
                }                           
                string newImage = await _fileService.UploadFileAsync(model.Image, "landingbanners");
                existBanner.Image = newImage;
            }
            _mapper.Map(model, existBanner);
            await _landingBannerRepository.EditAsync(existBanner);
        }

        public async Task<IEnumerable<LandingBannerDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<LandingBannerDto>>(await _landingBannerRepository.GetAllAsync());
        }

        public async Task<LandingBannerDto> GetByIdAsync(int id)
        {
            var slider = await _landingBannerRepository.GetByIdAsync(id);
            if (slider == null) throw new KeyNotFoundException($"Banner with ID {id} not found.");
            return _mapper.Map<LandingBannerDto>(slider);
        }

        public async Task<PaginationResponse<LandingBannerDto>> GetPaginateAsync(int page, int take)
        {
            var products = _landingBannerRepository.GetAllForPagination(null);
            int totalItemCount = products.Count();

            var paginated = products
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();

            var mappedDatas = _mapper.Map<IEnumerable<LandingBannerDto>>(paginated);

            int totalPage = (int)Math.Ceiling((decimal)totalItemCount / take);

            return new PaginationResponse<LandingBannerDto>(mappedDatas, totalPage, page, totalItemCount);
        }
    }
}
