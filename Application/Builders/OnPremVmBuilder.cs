using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Builders
{
    public class OnPremVmBuilder : IVirtualMachineBuilder
    {
        private VirtualMachine? _vm;
        private Network? _network;
        private Storage? _storage;

        public void Reset()
        {
            _vm = null;
            _network = null;
            _storage = null;
        }

        public void SetBaseConfiguration(VmType type, string size)
        {
            var (flavor, vcpu, mem) = OnPremFlavors(type, size);
            _vm = new VirtualMachine(CloudProvider.OnPrem, flavor, vcpu, mem);
        }

        public void ConfigureNetwork(string region, string[]? firewallRules, bool? publicIp)
        {
            _network = new Network(CloudProvider.OnPrem, region);
            _network.SetOptional(firewallRules, publicIp);
            _vm?.AttachNetwork(_network);
        }

        public void ConfigureStorage(string region, int? iops)
        {
            _storage = new Storage(CloudProvider.OnPrem, region);
            _storage.SetOptional(iops);
            _vm?.AttachStorage(_storage);
        }

        public void SetOptionalSettings(bool? diskOpt, bool? memOpt, string? keyPair)
        {
            _vm?.SetOptionalSettings(memOpt, diskOpt, keyPair);
        }

        public VirtualMachine GetResult()
        {
            _vm?.ValidateConsistency();
            return _vm!;
        }

        private static (string flavor, int vcpu, int mem) OnPremFlavors(VmType type, string size)
        {
            return type switch
            {
                VmType.Standard => size switch
                {
                    "Small" => ("onprem-std1", 2, 4),
                    "Medium" => ("onprem-std2", 4, 8),
                    "Large" => ("onprem-std3", 8, 16),
                    _ => ("onprem-std1", 2, 4)
                },
                VmType.MemoryOptimized => size switch
                {
                    "Small" => ("onprem-mem1", 2, 16),
                    "Medium" => ("onprem-mem2", 4, 32),
                    "Large" => ("onprem-mem3", 8, 64),
                    _ => ("onprem-mem1", 2, 16)
                },
                VmType.ComputeOptimized => size switch
                {
                    "Small" => ("onprem-cpu1", 2, 2),
                    "Medium" => ("onprem-cpu2", 4, 4),
                    "Large" => ("onprem-cpu3", 8, 8),
                    _ => ("onprem-cpu1", 2, 2)
                },
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
