using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Builders
{
    public class OnPremiseVmBuilder : IVirtualMachineBuilder
    {
        private readonly VirtualMachine _vm = new();

        public void SetProvider()
        {
            _vm.Provider = "OnPremise";
        }

        public void SetComputeResources(string vmType)
        {
            switch (vmType)
            {
                case "Standard":
                    _vm.Vcpus = 2;
                    _vm.MemoryGB = 4;
                    break;
                case "MemoryOptimized":
                    _vm.Vcpus = 2;
                    _vm.MemoryGB = 16;
                    _vm.MemoryOptimization = true;
                    break;
                case "ComputeOptimized":
                    _vm.Vcpus = 2;
                    _vm.MemoryGB = 2;
                    _vm.DiskOptimization = true;
                    break;
            }
        }

        public void ConfigureNetwork()
        {
            _vm.Network = new Network
            {
                Region = "onprem-region",
                FirewallRules = new List<string> { "SSH" },
                PublicIP = false
            };
        }

        public void ConfigureStorage()
        {
            _vm.Storage = new Storage
            {
                Region = "onprem-region",
                Iops = 1500
            };
        }

        public VirtualMachine Build() => _vm;
    }
}
