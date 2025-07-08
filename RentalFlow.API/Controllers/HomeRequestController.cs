using Microsoft.AspNetCore.Mvc;
using RentalFlow.API.Application.DTOs.HomeRequestDTOs;
using RentalFlow.API.Application.Interfaces.IServices;


namespace RentalFlow.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeRequestController : ControllerBase
{
    private readonly IHomeRequestService _homeRequestService;
    public HomeRequestController(IHomeRequestService homeRequestService)
    {
        _homeRequestService = homeRequestService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HomeRequestDto>>> GetAllHomeRequests([FromServices] IHomeRequestService homeRequestService)
    {
        var homeRequests = await homeRequestService.GetAllAsync();
        return Ok(homeRequests);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HomeRequestDto>> GetHomeRequestById(long id)
    {
        try
        {
            var homeRequest = await _homeRequestService.GetByIdAsync(id);
            return Ok(homeRequest);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }


    [HttpPost]
    public async Task<ActionResult<HomeRequestDto>> CreateHomeRequest([FromBody] HomeRequestCreateDto homeRequestCreateDto)
    {
        if (homeRequestCreateDto == null)
        {
            return BadRequest("Home request data is null.");
        }

        var createdHomeRequest = await _homeRequestService.CreateAsync(homeRequestCreateDto);
        return CreatedAtAction(nameof(GetHomeRequestById), new { id = createdHomeRequest.Id }, createdHomeRequest);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<HomeRequestDto>> UpdateHomeRequest(long id, [FromBody] HomeRequestUpdateDto homeRequestUpdateDto)
    {
        if (homeRequestUpdateDto == null)
            return BadRequest("Home request data is null.");

        try
        {
            var updatedHomeRequest = await _homeRequestService.UpdateAsync(id, homeRequestUpdateDto);
            return Ok(updatedHomeRequest);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHomeRequest(long id)
    {
        try
        {
            var result = await _homeRequestService.DeleteAsync(id);
            if (result == 0)
                return NotFound();

            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

}