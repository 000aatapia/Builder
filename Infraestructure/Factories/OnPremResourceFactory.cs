using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infraestructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Factories
{
    public class OnPremResourceFactory : ICloudResourceFactory
    {
        public CloudProvider Provider => CloudProvider.OnPrem;

        public Network CreateNetwork(string region, string[]? firewallRules = null, bool? publicIp = null)
        {
            LoggingService.LogInfo($"OnPrem: creando VLAN en datacenter {region}");
            var network = new Network(Provider, region);
            network.SetOptional(firewallRules, publicIp);
            return network;
        }

        public Storage CreateStorage(string region, int? iops = null)
        {
            LoggingService.LogInfo($"OnPrem: creando  volume en {region} (IOPS: {iops ?? 200})");
            var storage = new Storage(Provider, region);
            storage.SetOptional(iops);
            return storage;
        }

        public async Task<string> ProvisionVmAsync(VirtualMachine vm)
        {
            LoggingService.LogInfo($"OnPrem: desplegando VM {vm.FlavorName} ({vm.Vcpus} vCPU, {vm.MemoryGB} GiB RAM)");
            await Task.Delay(300);
            return $"onprem-vm-{Guid.NewGuid()}";
        }
    }
}
