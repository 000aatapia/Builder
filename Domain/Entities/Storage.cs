using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Storage
    {
        public string Region { get; set; } = string.Empty;
        public int? Iops { get; set; }
    }
}
