using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interface;
using Service.DTO.Admin.SubscribeImg;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class SubscribeImgService : ISubscribeImgService
    {
        private readonly ISubscribeImgRepository _subscribeImgRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public SubscribeImgService(ISubscribeImgRepository subscribeImgRepository,
                                   IFileService fileService,
                                   IMapper mapper)
        {
            _subscribeImgRepository = subscribeImgRepository;
            _fileService = fileService;
            _mapper = mapper;
        }
        public async Task CreateAsync(SubscribeImgCreateDto model)
        {
            if (model.Img == null) throw new ArgumentException("Image file is required.");
            string img = await _fileService.UploadFileAsync(model.Img, "subscribeImgs");
            var newSubscribeImg = _mapper.Map<SubscribeImg>(model);
            newSubscribeImg.Img = img;
            await _subscribeImgRepository.CreateAsync(newSubscribeImg);
        }

        public async Task DeleteAsync(int id)
        {
            var existSubscribeImg = await _subscribeImgRepository.GetByIdAsync(id);
            if (existSubscribeImg == null) throw new KeyNotFoundException($"SubscribeImg with ID {id} not found.");

            if (!string.IsNullOrEmpty(existSubscribeImg.Img))
            {
                string fileName = Path.GetFileName(existSubscribeImg.Img);
                _fileService.Delete(fileName, "subscribeImgs");
            }
            await _subscribeImgRepository.DeleteAsync(existSubscribeImg);
        }

        public async Task EditAsync(SubscribeImgEditDto model, int id)
        {
            var existSubscribeImg = await _subscribeImgRepository.GetByIdAsync(id);

            if (existSubscribeImg == null) throw new KeyNotFoundException($"SubscribeImg with ID {id} not found.");
            if (model.Img != null)
            {

                if (!string.IsNullOrEmpty(existSubscribeImg.Img))
                {
                    string oldFileName = Path.GetFileName(existSubscribeImg.Img);
                    _fileService.Delete(oldFileName, "subscribeImgs");
                }
                string newImageUrl = await _fileService.UploadFileAsync(model.Img, "subscribeImgs");
                existSubscribeImg.Img = newImageUrl;
            }
            _mapper.Map(model, existSubscribeImg);
            existSubscribeImg.Img = existSubscribeImg.Img;
            await _subscribeImgRepository.EditAsync(existSubscribeImg);
        }

        public async Task<IEnumerable<SubscribeImgDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<SubscribeImgDto>>(await _subscribeImgRepository.GetAllAsync());
        }

        public async Task<SubscribeImgDto> GetByIdAsync(int id)
        {
            var subscribeImg = await _subscribeImgRepository.GetByIdAsync(id);
            if (subscribeImg == null) throw new KeyNotFoundException($"SubscribeImg with ID {id} not found.");
            return _mapper.Map<SubscribeImgDto>(subscribeImg);
        }
    }
}
