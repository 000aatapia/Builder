using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Network
    {
        public CloudProvider Provider { get; private set; }
        public string Region { get; private set; }
        public string[]? FirewallRules { get; private set; } 
        public bool? PublicIP { get; private set; }

        public Network(CloudProvider provider, string region)
        {
            if (string.IsNullOrWhiteSpace(region))
                throw new DomainValidationException("region es obligatoria para la red.");

            Provider = provider;
            Region = region;
        }

        public void SetOptional(string[]? firewallRules, bool? publicIp)
        {
            FirewallRules = firewallRules;
            PublicIP = publicIp;
        }
    }
}
