
using Domain.Common;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public string AppUserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string StripeSessionId { get; set; }
        public AppUser AppUser { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}
