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
    public class GcpResourceFactory : ICloudResourceFactory
    {
        public CloudProvider Provider => CloudProvider.GCP;

        public Network CreateNetwork(string region, string[]? firewallRules = null, bool? publicIp = null)
        {
            LoggingService.LogInfo($"GCP: creando VPC red en la region {region}");
            var network = new Network(Provider, region);
            network.SetOptional(firewallRules, publicIp);
            return network;
        }

        public Storage CreateStorage(string region, int? iops = null)
        {
            LoggingService.LogInfo($"GCP: creando almacenamiento en la region {region} (IOPS: {iops ?? 300})");
            var storage = new Storage(Provider, region);
            storage.SetOptional(iops);
            return storage;
        }

        public async Task<string> ProvisionVmAsync(VirtualMachine vm)
        {
            LoggingService.LogInfo($"GCP: cargando instancia {vm.FlavorName} ({vm.Vcpus} vCPU, {vm.MemoryGB} GiB RAM)");
            await Task.Delay(300);
            return $"gcp-vm-{Guid.NewGuid()}";
        }
    }
}
