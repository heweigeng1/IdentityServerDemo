using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Id4Server.Models
{
    public class LoginInput
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool EnableLocalLogin { get; set; }
    }
}
