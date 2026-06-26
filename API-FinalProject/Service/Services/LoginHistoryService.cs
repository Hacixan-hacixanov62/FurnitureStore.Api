using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interface;
using Service.DTO.Admin.LoginHistory;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class LoginHistoryService : ILoginHistoryService
    {
        private readonly ILoginHistoryRepository _repository;
        private readonly IMapper _mapper;

        public LoginHistoryService(ILoginHistoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(LoginHistoryCreateDto dto)
        {
            var entity = _mapper.Map<LoginHistory>(dto);
            await _repository.AddAsync(entity);
        }

        public async Task<List<LoginHistoryListDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<List<LoginHistoryListDto>>(list);
        }

        public async Task<List<LoginHistoryListDto>> GetByUserIdAsync(string userId)
        {
            var list = await _repository.GetByUserIdAsync(userId);
            return _mapper.Map<List<LoginHistoryListDto>>(list);
        }
    }
}
