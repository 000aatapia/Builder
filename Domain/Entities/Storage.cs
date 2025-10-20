using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Storage
    {
        public CloudProvider Provider { get; private set; }
        public string Region { get; private set; }
        public int? Iops { get; private set; }

        public Storage(CloudProvider provider, string region)
        {
            if (string.IsNullOrWhiteSpace(region))
                throw new DomainValidationException("La region es obligatoria.");

            Provider = provider;
            Region = region;
        }

        public void SetOptional(int? iops)
        {
            Iops = iops;
        }
    }
}
