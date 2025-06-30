using Microsoft.AspNetCore.Mvc;
using RentalFlow.API.Application.DTOs.HomeRequestDTOs;
using RentalFlow.API.Application.Interfaces.IServices;


namespace RentalFlow.API.Controllers
{
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
        public async Task<ActionResult<HomeRequestDto>> GetHomeRequestById(long id, [FromServices] IHomeRequestService homeRequestService)
        {
            var homeRequest = await homeRequestService.GetByIdAsync(id);
            if (homeRequest == null)
            {
                return NotFound();
            }
            return Ok(homeRequest);
        }

        [HttpPost]
        public async Task<ActionResult<HomeRequestDto>> CreateHomeRequest([FromBody] HomeRequestCreateDto homeRequestCreateDto, [FromServices] IHomeRequestService homeRequestService)
        {
            if (homeRequestCreateDto == null)
            {
                return BadRequest("Home request data is null.");
            }
            var createdHomeRequest = await homeRequestService.CreateAsync(homeRequestCreateDto);
            return CreatedAtAction(nameof(GetHomeRequestById), new { id = createdHomeRequest.Id }, createdHomeRequest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HomeRequestDto>> UpdateHomeRequest(long id, [FromBody] HomeRequestUpdateDto homeRequestUpdateDto, [FromServices] IHomeRequestService homeRequestService)
        {
            if (homeRequestUpdateDto == null)
            {
                return BadRequest("Home request data is null.");
            }
            var updatedHomeRequest = await homeRequestService.UpdateAsync(id, homeRequestUpdateDto);
            if (updatedHomeRequest == null)
            {
                return NotFound();
            }
            return Ok(updatedHomeRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomeRequest(long id, [FromServices] IHomeRequestService homeRequestService)
        {
            var result = await homeRequestService.DeleteAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
