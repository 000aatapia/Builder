using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Network
    {
        public string Region { get; set; } = string.Empty;
        public List<string>? FirewallRules { get; set; }
        public bool PublicIP { get; set; }
    }
}
