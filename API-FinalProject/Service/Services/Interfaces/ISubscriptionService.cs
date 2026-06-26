using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Service.DTO.UI.Subscription;

namespace Service.Services.Interfaces
{
    public interface ISubscriptionService
    {
        Task SubscribeAsync(SubscriptionCreateDto model);
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync();
        Task UnsubscribeAsync(string email);
    }
}
