using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Builders;
using Domain.Entities;
using Domain.Enums;

namespace Application.Builders
{
    public class GcpVmBuilder : IVirtualMachineBuilder
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
            var (flavor, vcpu, mem) = GcpFlavors(type, size);
            _vm = new VirtualMachine(CloudProvider.GCP, flavor, vcpu, mem);
        }

        public void ConfigureNetwork(string region, string[]? firewallRules, bool? publicIp)
        {
            _network = new Network(CloudProvider.GCP, region);
            _network.SetOptional(firewallRules, publicIp);
            _vm?.AttachNetwork(_network);
        }

        public void ConfigureStorage(string region, int? iops)
        {
            _storage = new Storage(CloudProvider.GCP, region);
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

        private static (string flavor, int vcpu, int mem) GcpFlavors(VmType type, string size)
        {
            return type switch
            {
                VmType.Standard => size switch
                {
                    "Small" => ("e2-standard-2", 2, 8),
                    "Medium" => ("e2-standard-4", 4, 16),
                    "Large" => ("e2-standard-8", 8, 32),
                    _ => ("e2-standard-2", 2, 8)
                },
                VmType.MemoryOptimized => size switch
                {
                    "Small" => ("n2-highmem-2", 2, 16),
                    "Medium" => ("n2-highmem-4", 4, 32),
                    "Large" => ("n2-highmem-8", 8, 64),
                    _ => ("n2-highmem-2", 2, 16)
                },
                VmType.ComputeOptimized => size switch
                {
                    "Small" => ("n2-highcpu-2", 2, 2),
                    "Medium" => ("n2-highcpu-4", 4, 4),
                    "Large" => ("n2-highcpu-8", 8, 8),
                    _ => ("n2-highcpu-2", 2, 2)
                },
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
