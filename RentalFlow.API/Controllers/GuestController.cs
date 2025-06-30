using Microsoft.AspNetCore.Mvc;
using RentalFlow.API.Application.DTOs.GuestDTOs;
using RentalFlow.API.Application.Interfaces.IServices;


namespace RentalFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;
        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestDto>>> GetAllGuests()
        {
            var guests = await _guestService.GetAllAsync();
            return Ok(guests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestDto>> GetGuestById(long id)
        {
            var guest = await _guestService.GetByIdAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            return Ok(guest);
        }

        [HttpPost]
        public async Task<ActionResult<GuestDto>> CreateGuest([FromBody] GuestCreateDto guestCreateDto)
        {
            if (guestCreateDto == null)
            {
                return BadRequest("Guest data is null.");
            }
            var createdGuest = await _guestService.CreateAsync(guestCreateDto);
            return CreatedAtAction(nameof(GetGuestById), new { id = createdGuest.Id }, createdGuest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GuestDto>> UpdateGuest(long id, [FromBody] GuestUpdateDto guestUpdateDto)
        {
            if (guestUpdateDto == null)
            {
                return BadRequest("Guest data is null.");
            }
            var updatedGuest = await _guestService.UpdateAsync(id, guestUpdateDto);
            if (updatedGuest == null)
            {
                return NotFound();
            }
            return Ok(updatedGuest);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<long>> DeleteGuest(long id)
        {
            var deletedId = await _guestService.DeleteAsync(id);
            if (deletedId == 0)
            {
                return NotFound();
            }
            return Ok(deletedId);
        }
    }
}
