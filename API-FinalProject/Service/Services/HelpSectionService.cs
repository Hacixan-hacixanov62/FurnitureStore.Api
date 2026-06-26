using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Service.DTO.Admin.HelpSection;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class HelpSectionService : IHelpSectionService
    {
        private readonly IMapper _mapper;
        private readonly IHelpSectionRepository _helpSectionRepository;

        public HelpSectionService(IMapper mapper, IHelpSectionRepository helpSectionRepository)
        {
            _mapper = mapper;
           _helpSectionRepository = helpSectionRepository;
        }

        public async Task CreateAsync(HelpSectionCreateDto model)
        {
            var entity = _mapper.Map<HelpSection>(model);          
            await _helpSectionRepository.CreateAsync(entity);
        }

        public async Task EditAsync(HelpSectionEditDto model, int id)
        {
            var entity = await _helpSectionRepository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("HelpSection not found");
            _mapper.Map(model, entity);
            await _helpSectionRepository.EditAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            HelpSection help = await _helpSectionRepository.GetByIdAsync(id);
            if (help == null) throw new KeyNotFoundException($"HelpSection with ID {id} not found.");
            await _helpSectionRepository.DeleteAsync(help);
        }

        public async Task<IEnumerable<HelpSectionDto>> GetAllAsync()
        {      
            var dtos = _mapper.Map<IEnumerable<HelpSectionDto>>( await _helpSectionRepository.GetAllAsync());
            return dtos;
        }

        public async Task<HelpSectionDto> GetByIdAsync(int id)
        {
            var entity = await _helpSectionRepository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("HelpSection not found");
            return _mapper.Map<HelpSectionDto>(entity);
        }
    }
}
