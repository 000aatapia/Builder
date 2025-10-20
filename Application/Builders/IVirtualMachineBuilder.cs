using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Builders
{
    public interface IVirtualMachineBuilder
    {
        void Reset();
        void SetBaseConfiguration(VmType type, string size);
        void ConfigureNetwork(string region, string[]? firewallRules, bool? publicIp);
        void ConfigureStorage(string region, int? iops);
        void SetOptionalSettings(bool? diskOpt, bool? memOpt, string? keyPair);
        VirtualMachine GetResult();
    }
}
