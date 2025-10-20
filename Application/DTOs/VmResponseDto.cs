using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VmResponseDto
    {
        public string Provider { get; set; } = string.Empty;
        public string Flavor { get; set; } = string.Empty;
        public int Vcpus { get; set; }
        public int MemoryGB { get; set; }
        public string Region { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
} 