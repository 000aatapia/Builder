using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VirtualMachine
    {
        public CloudProvider Provider { get; private set; }         
        public string FlavorName { get; private set; }              
        public int Vcpus { get; private set; }                      
        public int MemoryGB { get; private set; }                  
        public bool? MemoryOptimization { get; private set; }      
        public bool? DiskOptimization { get; private set; }         
        public string? KeyPairName { get; private set; }            

        public Network? Network { get; private set; }
        public Storage? Storage { get; private set; }

        public VirtualMachine(CloudProvider provider, string flavorName, int vcpus, int memoryGB)
        {
            if (string.IsNullOrWhiteSpace(flavorName))
                throw new DomainValidationException("FlavorName es obligatorio.");
            if (vcpus <= 0)
                throw new DomainValidationException("vCPUs debe se mayor a 0.");
            if (memoryGB <= 0)
                throw new DomainValidationException("MemoryGB debe se mayor a 0.");

            Provider = provider;
            FlavorName = flavorName;
            Vcpus = vcpus;
            MemoryGB = memoryGB;
        }

        public void SetOptionalSettings(bool? memoryOpt, bool? diskOpt, string? keyPair)
        {
            MemoryOptimization = memoryOpt;
            DiskOptimization = diskOpt;
            KeyPairName = keyPair;
        }

        public void AttachNetwork(Network network)
        {
            if (network == null)
                throw new DomainValidationException("la red no puede ser nula.");
            if (network.Provider != Provider)
                throw new DomainValidationException("El proveedor de la red deber ser el proveedor de la VM.");

            Network = network;
        }

        public void AttachStorage(Storage storage)
        {
            if (storage == null)
                throw new DomainValidationException("el almacenamiento no puede ser nula");
            if (storage.Provider != Provider)
                throw new DomainValidationException("El proveedor storage deber ser el proveedor de la VM.");

            Storage = storage;
        }

        public void ValidateConsistency()
        {
            if (Network == null)
                throw new DomainValidationException("La red debe estar conectada..");
            if (Storage == null)
                throw new DomainValidationException("El almacenamiento debe estar asignado..");
            if (Network.Region != Storage.Region)
                throw new DomainValidationException("La red y el almacenamiento deben compartir la misma región.");
        }
    }
}
