using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VirtualMachine
    {
        public string Provider { get; set; } = string.Empty;
        public int Vcpus { get; set; }
        public int MemoryGB { get; set; }
        public bool MemoryOptimization { get; set; }
        public bool DiskOptimization { get; set; }
        public string? KeyPairName { get; set; }

        public Network Network { get; set; } = new();
        public Storage Storage { get; set; } = new();

        public override string ToString()
        {
            return $"{Provider} VM - {Vcpus} vCPU / {MemoryGB} GB RAM";
        }
    }
}
