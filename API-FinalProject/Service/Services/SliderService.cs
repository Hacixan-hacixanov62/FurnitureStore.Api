using System;
using System.Collections.Generic;
using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Service.DTO.Admin.Category;
using Service.DTO.Admin.Sliders;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;     
        public SliderService(ISliderRepository sliderRepository,
                             IFileService fileService,
                             IMapper mapper)
                           
        {
            _sliderRepository = sliderRepository;
            _fileService = fileService;
            _mapper = mapper;           
        }
        public async Task CreateAsync(SliderCreateDto model)
        {
            string imageUrl = await _fileService.UploadFileAsync(model.Image, "sliders");
            Slider slider = _mapper.Map<Slider>(model);
            slider.Image = imageUrl; 
            await _sliderRepository.CreateAsync(slider);
        }

        public async Task DeleteAsync(int id)
        {
            Slider slider = await _sliderRepository.GetByIdAsync(id);
            if (slider == null) throw new KeyNotFoundException($"Slider with ID {id} not found.");

            if (!string.IsNullOrEmpty(slider.Image))
            {              
                string fileName = Path.GetFileName(slider.Image);
                _fileService.Delete(fileName, "sliders");
            }
            await _sliderRepository.DeleteAsync(slider);
        }

        public async Task EditAsync(SliderEditDto model, int id)
        {
            var existSlider = await _sliderRepository.GetByIdAsync(id);
            if (existSlider == null)
                throw new KeyNotFoundException($"Slider with ID {id} not found.");
            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(existSlider.Image))
                {
                    string oldFileName = Path.GetFileName(existSlider.Image);
                    _fileService.Delete(oldFileName, "sliders");
                }
                string newImage = await _fileService.UploadFileAsync(model.Image, "sliders");
                existSlider.Image = newImage;
            }
            _mapper.Map(model, existSlider); 
            await _sliderRepository.EditAsync(existSlider);
        }

        public async Task<IEnumerable<SliderDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<SliderDto>>(await _sliderRepository.GetAllAsync());
        }

        public async Task<SliderDto> GetByIdAsync(int id)
        {
            var slider = await _sliderRepository.GetByIdAsync(id);
            if (slider == null) throw new KeyNotFoundException($"Slider with ID {id} not found.");
            return _mapper.Map<SliderDto>(slider);
        }

        public async Task<PaginationResponse<SliderDto>> GetPaginateAsync(int page, int take)
        {
            var products = _sliderRepository.GetAllForPagination(null);
            int totalItemCount = products.Count();

            var paginated = products
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();

            var mappedDatas = _mapper.Map<IEnumerable<SliderDto>>(paginated);

            int totalPage = (int)Math.Ceiling((decimal)totalItemCount / take);

            return new PaginationResponse<SliderDto>(mappedDatas, totalPage, page, totalItemCount);
        }
    }
}
