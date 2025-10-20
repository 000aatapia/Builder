using Domain.Builders;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Director
{
    public class VmDirector
    {
        public VirtualMachine Construct(IVirtualMachineBuilder builder, string vmType)
        {
            builder.SetProvider();
            builder.SetComputeResources(vmType);
            builder.ConfigureNetwork();
            builder.ConfigureStorage();
            return builder.Build();
        }
    }
}
