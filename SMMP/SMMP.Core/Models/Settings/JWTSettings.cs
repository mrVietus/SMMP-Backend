using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMMP.Core.Models.Settings
{
    public class JWTSettings
    {
        public string Key { get; set; }
        public int ExpiringMinutes { get; set; }
        public string Issuer { get; set; }
    }
}
