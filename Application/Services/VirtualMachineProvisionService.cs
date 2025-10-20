using Application.Builders;
using Application.Directors;
using Application.DTOs;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{
    public class VirtualMachineProvisionService
    {
        private readonly Dictionary<CloudProvider, ICloudResourceFactory> _factories;

        public VirtualMachineProvisionService(IEnumerable<ICloudResourceFactory> factories)
        {
            _factories = factories.ToDictionary(f => f.Provider, f => f);
        }

        public async Task<VmResponseDto> ProvisionVmAsync(VmRequestDto request)
        {
            IVirtualMachineBuilder builder = request.Provider switch
            {
                CloudProvider.AWS => new AwsVmBuilder(),
                CloudProvider.Azure => new AzureVmBuilder(),
                CloudProvider.GCP => new GcpVmBuilder(),
                CloudProvider.OnPrem => new OnPremVmBuilder(),
                _ => throw new ArgumentException("Proveedor invalido")
            };

            var director = new VirtualMachineDirector(builder);
            director.Construct(request);

            var vm = builder.GetResult();

            // Obtener la fábrica correspondiente y "provisionar"
            var factory = _factories[request.Provider];
            var vmId = await factory.ProvisionVmAsync(vm);

            return new VmResponseDto
            {
                Provider = request.Provider.ToString(),
                Flavor = vm.FlavorName,
                Vcpus = vm.Vcpus,
                MemoryGB = vm.MemoryGB,
                Region = vm.Network!.Region,
                Success = true,
                Message = $"VM aprovisionado correctamente ID {vmId}"
            };
        }
    }
}
