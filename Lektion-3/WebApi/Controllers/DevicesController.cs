using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }


        [HttpPost]
        public async Task<IActionResult> Create(DeviceRequest device)
        {
            return new OkObjectResult(await _deviceService.CreateDeviceAsync(device.DeviceId));
        }
    }
}
