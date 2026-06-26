using Domain.Common;

namespace Domain.Entities
{
    public class AskUsFrom : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool IsApproved { get; set; }
    }
}
