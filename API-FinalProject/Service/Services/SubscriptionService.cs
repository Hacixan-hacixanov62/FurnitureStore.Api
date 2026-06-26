using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interface;
using Service.DTO.UI.Subscription;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IAccountService accountService,
                                    IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task SubscribeAsync(SubscriptionCreateDto model)
        {
            var existingUser = await _accountService.GetUserByEmailAsync(model.Email);
            if (existingUser == null) throw new Exception("First be register.");

            var existingSubscription = await _subscriptionRepository.GetByEmailAsync(model.Email);
            if (existingSubscription != null)
                throw new Exception("Subscribe with this mail already exists!");

            var subscription = _mapper.Map<Subscription>(model);
            subscription.CreatedDate = DateTime.UtcNow;

            await _subscriptionRepository.CreateAsync(subscription);
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
        {
            var subscriptions = await _subscriptionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        }

        public async Task UnsubscribeAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.");

            var subscription = await _subscriptionRepository.GetByEmailAsync(email);
            if (subscription == null)
                throw new Exception("No subscription found with this email.");

            await _subscriptionRepository.DeleteAsync(subscription);
        }

    }
}
