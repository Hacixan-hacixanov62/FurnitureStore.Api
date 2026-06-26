using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public List<Order> Orders { get; set; }
        public ICollection<BlogReview> BlogReviews { get; set; }
        public bool IsBlocked { get; set; } = false;
    }
}
