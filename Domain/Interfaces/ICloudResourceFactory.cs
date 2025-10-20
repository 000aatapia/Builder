using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICloudResourceFactory
    {
        Network CreateNetwork(string region, string[]? firewallRules = null, bool? publicIp = null);
        Storage CreateStorage(string region, int? iops = null);
        Task<string> ProvisionVmAsync(VirtualMachine vm);
        CloudProvider Provider { get; }
    }
}
