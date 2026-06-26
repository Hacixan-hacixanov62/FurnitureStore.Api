using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.UI.Subscription;
using Service.Services;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Client
{
    public class SubscriptionController : BaseController
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe([FromBody] SubscriptionCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _subscriptionService.SubscribeAsync(model);
            return Ok("Subscribed successfully!");
        }
    }
}
