using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Builders
{
    public class GcpVmBuilder : IVirtualMachineBuilder
    {
        private readonly VirtualMachine _vm = new();

        public void SetProvider()
        {
            _vm.Provider = "GCP";
        }

        public void SetComputeResources(string vmType)
        {
            switch (vmType)
            {
                case "Standard":
                    _vm.Vcpus = 2;
                    _vm.MemoryGB = 8;
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
                Region = "us-central1",
                FirewallRules = new List<string> { "HTTP", "SSH" },
                PublicIP = true
            };
        }

        public void ConfigureStorage()
        {
            _vm.Storage = new Storage
            {
                Region = "us-central1",
                Iops = 2500
            };
        }

        public VirtualMachine Build() => _vm;
    }
}
