using Microsoft.AspNetCore.Mvc;
using RentalFlow.API.Application.DTOs.HostDTOs;
using RentalFlow.API.Application.Interfaces.IServices;


namespace RentalFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostController : ControllerBase
    {
        private readonly IHostService _hostService;
        public HostController(IHostService hostService)
        {
            _hostService = hostService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HostDto>>> GetAllHosts()
        {
            var hosts = await _hostService.GetAllAsync();
            return Ok(hosts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HostDto>> GetHostById(long id)
        {
            var host = await _hostService.GetByIdAsync(id);
            if (host == null)
            {
                return NotFound();
            }
            return Ok(host);
        }

        [HttpPost]
        public async Task<ActionResult<HostDto>> CreateHost([FromBody] HostCreateDto hostCreateDto)
        {
            if (hostCreateDto == null)
            {
                return BadRequest("Host data is null.");
            }
            var createdHost = await _hostService.CreateAsync(hostCreateDto);
            return CreatedAtAction(nameof(GetHostById), new { id = createdHost.Id }, createdHost);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HostDto>> UpdateHost(long id, [FromBody] HostUpdateDto hostUpdateDto)
        {
            if (hostUpdateDto == null)
            {
                return BadRequest("Host data is null.");
            }
            var updatedHost = await _hostService.UpdateAsync(id, hostUpdateDto);
            if (updatedHost == null)
            {
                return NotFound();
            }
            return Ok(updatedHost);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHost(long id)
        {
            var deleted = await _hostService.DeleteAsync(id);
            if (deleted == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
