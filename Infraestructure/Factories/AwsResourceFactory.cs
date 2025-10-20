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
    public class AwsResourceFactory : ICloudResourceFactory
    {
        public CloudProvider Provider => CloudProvider.AWS;

        public Network CreateNetwork(string region, string[]? firewallRules = null, bool? publicIp = null)
        {
            LoggingService.LogInfo($"AWS: creando red en la region {region}");
            var network = new Network(Provider, region);
            network.SetOptional(firewallRules, publicIp);
            return network;
        }

        public Storage CreateStorage(string region, int? iops = null)
        {
            LoggingService.LogInfo($"AWS: creando almacenamiento en la region {region} with {iops ?? 1000} IOPS");
            var storage = new Storage(Provider, region);
            storage.SetOptional(iops);
            return storage;
        }

        public async Task<string> ProvisionVmAsync(VirtualMachine vm)
        {
            LoggingService.LogInfo($"AWS: Provisioning VM {vm.FlavorName} ({vm.Vcpus} vCPU, {vm.MemoryGB} GiB RAM) en la region {vm.Network?.Region}");
            await Task.Delay(300);
            return $"aws-vm-{Guid.NewGuid()}";
        }
    }
}
