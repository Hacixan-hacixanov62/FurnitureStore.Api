using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Service.DTO.Admin.AskUsFrom;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class AskUsFromService : IAskUsFromService
    {
        private readonly IAskUsFromRepository _askUsFromRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AskUsFromService(IAskUsFromRepository askUsFromRepository,
                                IAccountService accountService,
                                IMapper mapper)
        {
            _askUsFromRepository = askUsFromRepository;
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task CreateAsync(AskUsFromCreateDto model)
        {
            var existingUser = await _accountService.GetUserByEmailAsync(model.Email);
            if (existingUser == null) throw new Exception("First be register.");
            var subscription = _mapper.Map<AskUsFrom>(model);
            subscription.CreatedDate = DateTime.UtcNow;
            await _askUsFromRepository.CreateAsync(subscription);
        }

        public async Task DeleteAsync(int id)
        {
           var askUs = await _askUsFromRepository.GetByIdAsync(id);
           if (askUs == null) throw new KeyNotFoundException($"AskUsFrom with ID {id} not found.");
           await _askUsFromRepository.DeleteAsync(askUs); 

        }

        public async Task<IEnumerable<AskUsFromDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<AskUsFromDto>>(await _askUsFromRepository.GetAllAsync());
        }

        public async Task<AskUsFromDto> GetByIdAsync(int id)
        {
            var message = await _askUsFromRepository.GetByIdAsync(id);
            if (message == null) throw new KeyNotFoundException($"Message with ID {id} not found.");

            return _mapper.Map<AskUsFromDto>(message);
        }

        public async Task ApproveMessageAsync(int id)
        {
            var askUs = await _askUsFromRepository.GetByIdAsync(id);
            if (askUs == null) throw new KeyNotFoundException($"AskUsFrom with ID {id} not found.");

            askUs.IsApproved = true; 
            await _askUsFromRepository.EditAsync(askUs);
        }

        public async Task<IEnumerable<AskUsFromDto>> GetApprovedMessagesAsync()
        {
            var messages = await _askUsFromRepository.GetAllAsync();
            var approvedMessages = messages.Where(m => m.IsApproved);

            return _mapper.Map<IEnumerable<AskUsFromDto>>(approvedMessages);
        }
    }
}
