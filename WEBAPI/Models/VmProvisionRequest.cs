using Domain.Enums;

namespace WEBAPI.Models
{
    public class VmProvisionRequest
    {
        public CloudProvider Provider { get; set; }
        public string Region { get; set; } = string.Empty;
        public VmType Type { get; set; }
        public string Flavor { get; set; } = string.Empty;

        // Parámetros opcionales
        public string[]? FirewallRules { get; set; }
        public bool? PublicIp { get; set; }
        public int? Iops { get; set; }
    }
}
