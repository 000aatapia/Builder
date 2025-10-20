using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Builders
{
    public interface IVirtualMachineBuilder
    {
        void SetProvider();
        void SetComputeResources(string vmType);
        void ConfigureNetwork();
        void ConfigureStorage();
        VirtualMachine Build();
    }
}
