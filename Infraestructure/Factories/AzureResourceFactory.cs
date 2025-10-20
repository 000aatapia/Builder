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
    public class AzureResourceFactory : ICloudResourceFactory
    {
        public CloudProvider Provider => CloudProvider.Azure;

        public Network CreateNetwork(string region, string[]? firewallRules = null, bool? publicIp = null)
        {
            LoggingService.LogInfo($"Azure:  creando red en la region {region}");
            var network = new Network(Provider, region);
            network.SetOptional(firewallRules, publicIp);
            return network;
        }

        public Storage CreateStorage(string region, int? iops = null)
        {
            LoggingService.LogInfo($"Azure: creando  almacenamiento en la region {region} with {iops ?? 500} IOPS");
            var storage = new Storage(Provider, region);
            storage.SetOptional(iops);
            return storage;
        }

        public async Task<string> ProvisionVmAsync(VirtualMachine vm)
        {
            LoggingService.LogInfo($"Azure: Deploying VM {vm.FlavorName} ({vm.Vcpus} vCPU, {vm.MemoryGB} GiB RAM)");
            await Task.Delay(300);
            return $"azure-vm-{Guid.NewGuid()}";
        }
    }
}
