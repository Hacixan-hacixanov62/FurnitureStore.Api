using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO.Admin.LoginHistory
{
    public class LoginHistoryCreateDto
    {
        public string UserId { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime LoginTime { get; set; } = DateTime.UtcNow;
    }
}
