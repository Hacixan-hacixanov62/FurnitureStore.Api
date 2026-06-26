using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Service.DTO.Admin.AboutBannerImg;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class AboutBannerImgService : IAboutBannerImgService
    {
        private readonly IAboutBannerImgRepository _aboutBannerImgRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public AboutBannerImgService(IAboutBannerImgRepository aboutBannerImgRepository,
                                   IFileService fileService,
                                   IMapper mapper)
        {
            _aboutBannerImgRepository = aboutBannerImgRepository;
            _fileService = fileService;
            _mapper = mapper;
        }
        public async Task CreateAsync(AboutBannerImgCreateDto model)
        {
            if (model.Img == null) throw new ArgumentException("Image file is required.");
            string img = await _fileService.UploadFileAsync(model.Img, "aboutbannerimgs");
            var newImg = _mapper.Map<AboutBannerImg>(model);
            newImg.Img = img;
            await _aboutBannerImgRepository.CreateAsync(newImg);
        }

        public async Task DeleteAsync(int id)
        {
            var existImg = await _aboutBannerImgRepository.GetByIdAsync(id);
            if (existImg == null) throw new KeyNotFoundException($"Image with ID {id} not found.");

            if (!string.IsNullOrEmpty(existImg.Img))
            {
                string fileName = Path.GetFileName(existImg.Img);
                _fileService.Delete(fileName, "aboutbannerimgs");
            }
            await _aboutBannerImgRepository.DeleteAsync(existImg);
        }

        public async Task EditAsync(AboutBannerImgEditDto model, int id)
        {
            var existImg = await _aboutBannerImgRepository.GetByIdAsync(id);

            if (existImg == null) throw new KeyNotFoundException($"Image with ID {id} not found.");
            if (model.Img != null)
            {

                if (!string.IsNullOrEmpty(existImg.Img))
                {
                    string oldFileName = Path.GetFileName(existImg.Img);
                    _fileService.Delete(oldFileName, "aboutbannerimgs");
                }
                string newImageUrl = await _fileService.UploadFileAsync(model.Img, "aboutbannerimgs");
                existImg.Img = newImageUrl;
            }
            _mapper.Map(model, existImg);
            existImg.Img = existImg.Img;
            await _aboutBannerImgRepository.EditAsync(existImg);
        }

        public async Task<IEnumerable<AboutBannerImgDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<AboutBannerImgDto>>(await _aboutBannerImgRepository.GetAllAsync());
        }

        public async Task<AboutBannerImgDto> GetByIdAsync(int id)
        {
            var Img = await _aboutBannerImgRepository.GetByIdAsync(id);
            if (Img == null) throw new KeyNotFoundException($"SubscribeImg with ID {id} not found.");
            return _mapper.Map<AboutBannerImgDto>(Img);
        }
    }
}
