using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interface;
using Service.DTO.Admin.Instagram;
using Service.DTO.Admin.SubscribeImg;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class InstagramService : IInstagramService
    {
        private readonly IInstagramRepository _instagramRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public InstagramService(IInstagramRepository instagramRepository,
                                   IFileService fileService,
                                   IMapper mapper)
        {
           _instagramRepository = instagramRepository;  
            _fileService = fileService;
            _mapper = mapper;
        }
        public async Task CreateAsync(InstagramCreateDto model)
        {
            if (model.Img == null) throw new ArgumentException("Image file is required.");
            string img = await _fileService.UploadFileAsync(model.Img, "instagram");
            var newImg = _mapper.Map<Instagram>(model);
            newImg.Img = img;
            await _instagramRepository.CreateAsync(newImg);
        }

        public async Task DeleteAsync(int id)
        {
            var existImg = await _instagramRepository.GetByIdAsync(id);
            if (existImg == null) throw new KeyNotFoundException($"Image with ID {id} not found.");

            if (!string.IsNullOrEmpty(existImg.Img))
            {
                string fileName = Path.GetFileName(existImg.Img);
                _fileService.Delete(fileName, "instagram");
            }
            await _instagramRepository.DeleteAsync(existImg);
        }

        public async Task EditAsync(InstagramEditDto model, int id)
        {
            var existImg = await _instagramRepository.GetByIdAsync(id);

            if (existImg == null) throw new KeyNotFoundException($"Image with ID {id} not found.");
            if (model.Img != null)
            {
                if (!string.IsNullOrEmpty(existImg.Img))
                {
                    string oldFileName = Path.GetFileName(existImg.Img);
                    _fileService.Delete(oldFileName, "instagram");
                }
                string newImageUrl = await _fileService.UploadFileAsync(model.Img, "instagram");
                existImg.Img = newImageUrl;
            }
            _mapper.Map(model, existImg);
            existImg.Img = existImg.Img;
            await _instagramRepository.EditAsync(existImg);
        }

        public async Task<IEnumerable<InstagramDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<InstagramDto>>(await _instagramRepository.GetAllAsync());
        }

        public async Task<InstagramDto> GetByIdAsync(int id)
        {
            var Img = await _instagramRepository.GetByIdAsync(id);
            if (Img == null) throw new KeyNotFoundException($"SubscribeImg with ID {id} not found.");
            return _mapper.Map<InstagramDto>(Img);
        }
    }
}
