using System;
using Domain.Common;

namespace Domain.Entities
{
    public class HelpSection : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string CustomerServiceHours { get; set; }
        public bool IsWeekendClosed { get; set; } = true;
    }
}
