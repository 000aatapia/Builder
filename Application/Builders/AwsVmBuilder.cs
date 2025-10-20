using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Builders
{
    public class AwsVmBuilder : IVirtualMachineBuilder
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
            var (flavor, vcpu, mem) = AwsFlavors(type, size);
            _vm = new VirtualMachine(CloudProvider.AWS, flavor, vcpu, mem);
        }

        public void ConfigureNetwork(string region, string[]? firewallRules, bool? publicIp)
        {
            _network = new Network(CloudProvider.AWS, region);
            _network.SetOptional(firewallRules, publicIp);
            _vm?.AttachNetwork(_network);
        }

        public void ConfigureStorage(string region, int? iops)
        {
            _storage = new Storage(CloudProvider.AWS, region);
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

        private static (string flavor, int vcpu, int mem) AwsFlavors(VmType type, string size)
        {
            return type switch
            {
                VmType.Standard => size switch
                {
                    "Small" => ("t3.medium", 2, 4),
                    "Medium" => ("m5.large", 2, 8),
                    "Large" => ("m5.xlarge", 4, 16),
                    _ => ("t3.medium", 2, 4)
                },
                VmType.MemoryOptimized => size switch
                {
                    "Small" => ("r5.large", 2, 16),
                    "Medium" => ("r5.xlarge", 4, 32),
                    "Large" => ("r5.2xlarge", 8, 64),
                    _ => ("r5.large", 2, 16)
                },
                VmType.ComputeOptimized => size switch
                {
                    "Small" => ("c5.large", 2, 4),
                    "Medium" => ("c5.xlarge", 4, 8),
                    "Large" => ("c5.2xlarge", 8, 16),
                    _ => ("c5.large", 2, 4)
                },
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
