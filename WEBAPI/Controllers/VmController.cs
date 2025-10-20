using Application.DTOs;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VmController : ControllerBase
    {
        private readonly VirtualMachineProvisionService _provisionService;

        public VmController(VirtualMachineProvisionService provisionService)
        {
            _provisionService = provisionService;
        }
        [HttpPost("provision")]
        public async Task<IActionResult> ProvisionVm([FromBody] VmProvisionRequest request)
        {
            if (!Enum.IsDefined(typeof(CloudProvider), request.Provider))
                return BadRequest("Proveedor inválido.");

            if (string.IsNullOrWhiteSpace(request.Region))
                return BadRequest("Debe especificar la región.");

            if (string.IsNullOrWhiteSpace(request.Flavor))
                return BadRequest("Debe especificar el flavor.");

            try
            {
                
                var dto = new VmRequestDto
                {
                    Provider = request.Provider,
                    Region = request.Region,
                    Type = request.Type,
                    Flavor = request.Flavor,
                    FirewallRules = request.FirewallRules,
                    PublicIP = request.PublicIp,
                    Iops = request.Iops
                };

                // 🔹 Llamar al servicio de aplicación
                var vmResponse = await _provisionService.ProvisionVmAsync(dto);

                // 🔹 Respuesta HTTP 200 OK
                return Ok(new
                {
                    Message = "Máquina virtual aprovisionada exitosamente",
                    Provider = vmResponse.Provider,
                    Region = vmResponse.Region,
                    Flavor = vmResponse.Flavor,
                    Vcpus = vmResponse.Vcpus,
                    MemoryGB = vmResponse.MemoryGB
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al aprovisionar la VM: {ex.Message}");
            }
        }

    }
}
