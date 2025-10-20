using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VmRequestDto
    {
        public CloudProvider Provider { get; set; }
        public VmType Type { get; set; }
        public string Region { get; set; } = string.Empty;
        public bool? DiskOptimization { get; set; }
        public bool? MemoryOptimization { get; set; }
        public string? KeyPairName { get; set; }
        public string[]? FirewallRules { get; set; }
        public bool? PublicIP { get; set; }
        public int? Iops { get; set; }
        public string Size { get; set; } = "Medium"; // Small, Medium, Large
    }
}
