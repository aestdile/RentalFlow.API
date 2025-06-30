using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalFlow.API.Application.DTOs.HomeDTOs;
using RentalFlow.API.Application.Interfaces.IServices;
using RentalFlow.API.Domain.Entities;


namespace RentalFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomeDto>>> GetAll()
        {
            var values = await _homeService.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HomeDto>> GetById(long id)
        {
            var value = await _homeService.GetByIdAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<ActionResult<HomeDto>> Post([FromBody] HomeCreateDto homeCreateDto)
        {
            if (homeCreateDto == null)
            {
                return BadRequest("Home data is null.");
            }
            var createdHome = await _homeService.CreateAsync(homeCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdHome.Id }, createdHome);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HomeDto>> Put(long id, [FromBody] HomeUpdateDto homeUpdateDto)
        {
            if (homeUpdateDto == null)
            {
                return BadRequest("Home data is null.");
            }
            var updatedHome = await _homeService.UpdateAsync(id, homeUpdateDto);
            if (updatedHome == null)
            {
                return NotFound();
            }
            return Ok(updatedHome);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<long>> Delete(long id)
        {
            var deletedHome = await _homeService.DeleteAsync(id);
            if (deletedHome == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
