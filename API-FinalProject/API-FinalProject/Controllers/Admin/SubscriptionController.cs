using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.UI.Subscription;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class SubscriptionController : BaseController
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
            return Ok(subscriptions);
        }

        [HttpDelete]
        public async Task<IActionResult> Unsubscribe([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))  return BadRequest("Email is required.");
            await _subscriptionService.UnsubscribeAsync(email);
            return Ok($"Unsubscribed user with email: {email}");
        }
    }
}
