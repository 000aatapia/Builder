using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Builders
{
    public class AzureVmBuilder : IVirtualMachineBuilder
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
            var (flavor, vcpu, mem) = AzureFlavors(type, size);
            _vm = new VirtualMachine(CloudProvider.Azure, flavor, vcpu, mem);
        }

        public void ConfigureNetwork(string region, string[]? firewallRules, bool? publicIp)
        {
            _network = new Network(CloudProvider.Azure, region);
            _network.SetOptional(firewallRules, publicIp);
            _vm?.AttachNetwork(_network);
        }

        public void ConfigureStorage(string region, int? iops)
        {
            _storage = new Storage(CloudProvider.Azure, region);
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

        private static (string flavor, int vcpu, int mem) AzureFlavors(VmType type, string size)
        {
            return type switch
            {
                VmType.Standard => size switch
                {
                    "Small" => ("D2s_v3", 2, 8),
                    "Medium" => ("D4s_v3", 4, 16),
                    "Large" => ("D8s_v3", 8, 32),
                    _ => ("D2s_v3", 2, 8)
                },
                VmType.MemoryOptimized => size switch
                {
                    "Small" => ("E2s_v3", 2, 16),
                    "Medium" => ("E4s_v3", 4, 32),
                    "Large" => ("E8s_v3", 8, 64),
                    _ => ("E2s_v3", 2, 16)
                },
                VmType.ComputeOptimized => size switch
                {
                    "Small" => ("F2s_v2", 2, 4),
                    "Medium" => ("F4s_v2", 4, 8),
                    "Large" => ("F8s_v2", 8, 16),
                    _ => ("F2s_v2", 2, 4)
                },
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
