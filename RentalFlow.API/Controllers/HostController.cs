using Microsoft.AspNetCore.Mvc;
using RentalFlow.API.Application.DTOs.HostDTOs;
using RentalFlow.API.Application.Interfaces.IServices;


namespace RentalFlow.API.Controllers;

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
        try
        {
            var host = await _hostService.GetByIdAsync(id);
            return Ok(host);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
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

        try
        {
            var updatedHost = await _hostService.UpdateAsync(id, hostUpdateDto);
            return Ok(updatedHost);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHost(long id)
    {
        try
        {
            var deleted = await _hostService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
