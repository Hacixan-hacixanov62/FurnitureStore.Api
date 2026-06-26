using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Settings;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;
        public SettingService(ISettingRepository settingRepository,IMapper mapper)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
            
        }

        public async Task EditAsync(int id, SettingEditDto model)
        {
            Setting setting = await _settingRepository.GetByIdAsync(id);
            if (setting == null) throw new KeyNotFoundException($"Setting with ID {id} not found.");
            _mapper.Map(model, setting);
            await _settingRepository.EditAsync(setting);
        }

        public async Task<IEnumerable<SettingDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<SettingDto>>(await _settingRepository.GetAllAsync());
        }

        public async Task<SettingDto> GetByIdAsync(int id)
        {
            var setting = await _settingRepository.GetByIdAsync(id);
            if (setting == null) throw new KeyNotFoundException($"Setting with ID {id} not found.");
            return _mapper.Map<SettingDto>(setting);
        }
    }
}
