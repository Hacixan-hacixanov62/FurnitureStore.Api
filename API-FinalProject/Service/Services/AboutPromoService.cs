using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interface;
using Service.DTO.Admin.AboutPromo;
using Service.DTO.Admin.Sliders;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class AboutPromoService : IAboutPromoService
    {
        private readonly IAboutPromoRepository _aboutPromoRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public AboutPromoService(IAboutPromoRepository aboutPromoRepository,
                             IFileService fileService,
                             IMapper mapper)

        {
            _aboutPromoRepository = aboutPromoRepository;
            _fileService = fileService;
            _mapper = mapper;
        }
        public async Task CreateAsync(AboutPromoCreateDto model)
        {
            string imageUrl = await _fileService.UploadFileAsync(model.Image, "aboutpromo");
            AboutPromo aboutPromo = _mapper.Map<AboutPromo>(model);
            aboutPromo.Image = imageUrl;
            await _aboutPromoRepository.CreateAsync(aboutPromo);
        }

        public async Task DeleteAsync(int id)
        {
            AboutPromo aboutPromo = await _aboutPromoRepository.GetByIdAsync(id);
            if (aboutPromo == null) throw new KeyNotFoundException($"About Promo with ID {id} not found.");

            if (!string.IsNullOrEmpty(aboutPromo.Image))
            {
                string fileName = Path.GetFileName(aboutPromo.Image);
                _fileService.Delete(fileName, "aboutpromo");
            }
            await _aboutPromoRepository.DeleteAsync(aboutPromo);
        }

        public async Task EditAsync(AboutPromoEditDto model, int id)
        {
            var existPromo = await _aboutPromoRepository.GetByIdAsync(id);
            if (existPromo == null)
                throw new KeyNotFoundException($"About Promo with ID {id} not found.");
            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(existPromo.Image))
                {
                    string oldFileName = Path.GetFileName(existPromo.Image);
                    _fileService.Delete(oldFileName, "aboutpromo");
                }
                string newImage = await _fileService.UploadFileAsync(model.Image, "aboutpromo");
                existPromo.Image = newImage;
            }
            _mapper.Map(model, existPromo);
            await _aboutPromoRepository.EditAsync(existPromo);
        }

        public async Task<IEnumerable<AboutPromoDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<AboutPromoDto>>(await _aboutPromoRepository.GetAllAsync());
        }

        public async Task<AboutPromoDto> GetByIdAsync(int id)
        {
            var promo = await _aboutPromoRepository.GetByIdAsync(id);
            if (promo == null) throw new KeyNotFoundException($"About Promo with ID {id} not found.");
            return _mapper.Map<AboutPromoDto>(promo);
        }    
    }
}
