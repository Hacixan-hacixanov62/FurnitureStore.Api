using System.ComponentModel.DataAnnotations;

namespace Service.DTO.UI.Subscription
{
    public class SubscriptionCreateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
