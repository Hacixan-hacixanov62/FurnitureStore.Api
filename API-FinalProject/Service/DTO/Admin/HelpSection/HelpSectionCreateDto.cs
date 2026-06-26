using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO.Admin.HelpSection
{
    public class HelpSectionCreateDto
    {
        public string PhoneNumber { get; set; }
        public string CustomerServiceHours { get; set; }
        public bool IsWeekendClosed { get; set; }
    }
}
