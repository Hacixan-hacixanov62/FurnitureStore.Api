using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Account
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> Message { get; set; }     
    }
}
