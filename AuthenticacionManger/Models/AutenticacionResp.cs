using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticacionManger.Models
{
    public class AutenticacionResp
    {
        public string UserName { get; set; }
        public string JwToken { get; set; }
        public int ExpiresIn { get; set; }

    }
}
