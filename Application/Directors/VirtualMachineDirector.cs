using Application.Builders;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Directors
{
    public class VirtualMachineDirector
    {
        private readonly IVirtualMachineBuilder _builder;

        public VirtualMachineDirector(IVirtualMachineBuilder builder)
        {
            _builder = builder;
        }

        public void Construct(VmRequestDto request)
        {
            _builder.Reset();
            _builder.SetBaseConfiguration(request.Type, request.Size);
            _builder.ConfigureNetwork(request.Region, request.FirewallRules, request.PublicIP);
            _builder.ConfigureStorage(request.Region, request.Iops);
            _builder.SetOptionalSettings(request.DiskOptimization, request.MemoryOptimization, request.KeyPairName);
        }
    }
}
