using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Resources
{
    public class RefreshTokenResource
    {
        public string token { get; set; }
        public string userId { get; set; }
    }
}
