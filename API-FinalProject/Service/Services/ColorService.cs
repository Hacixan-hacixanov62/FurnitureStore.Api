using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Service.DTO.Admin.Color;
using Service.DTO.Admin.Tag;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;
        public ColorService(IColorRepository colorRepository,
                            IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(ColorCreateDto model)
        {
            var color = await _colorRepository.GetAllWithExpressionAsync(x => x.Name.ToLower() == model.Name.ToLower());
            if (color.ToList().Count > 0) throw new ArgumentException("This color has already exist");
            await _colorRepository.CreateAsync(_mapper.Map<Color>(model));  
        }

        public async Task DeleteAsync(int id)
        {
            var color = await _colorRepository.GetByIdAsync(id);
            if (color == null) throw new KeyNotFoundException($"Color with ID {id} not found.");
            await _colorRepository.DeleteAsync(color);
        }

        public async Task EditAsync(ColorEditDto model, int id)
        {
            var color = await _colorRepository.GetByIdAsync(id);
            if (color == null) throw new KeyNotFoundException($"Color with ID {id} not found.");

            if (!string.Equals(color.Name.Trim(), model.Name.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                var sameNameCategory = await _colorRepository
                    .GetAllWithExpressionAsync(x => x.Name.Trim().ToLower() == model.Name.Trim().ToLower() && x.Id != id);

                if (sameNameCategory.Any())
                    throw new ArgumentException("This color name already exists.");
            }

            _mapper.Map(model, color);
            await _colorRepository.EditAsync(color);                   
        }

        public async Task<IEnumerable<ColorDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ColorDto>>(await _colorRepository.GetAllAsync());
        }

        public async Task<ColorDto> GetByIdAsync(int id)
        {
            var color = await _colorRepository.GetByIdAsync(id);
            if (color == null) throw new KeyNotFoundException($"Color with ID {id} not found.");
            return _mapper.Map<ColorDto>(color);
        }
    }
}
