using Application.DTOs.Destination;
using Application.Interface.Service;
using AutoMapper;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationController : ControllerBase
    {
        private readonly IDestinationService _destinationService;
        private readonly IMapper _mapper;

        public DestinationController(IDestinationService destinationService, IMapper mapper)
        {
            _destinationService = destinationService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllDestinations(
            [FromQuery] int? pageIndex = null,
            [FromQuery] int? pageSize = null)
        {
            var destinations = await _destinationService.GetAllAsync(pageIndex, pageSize);
            return Ok(destinations);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDestinationById(Guid id)
        {
            var destination = await _destinationService.GetByIdAsync(id);
            if (destination == null)
            {
                return NotFound();
            }
            return Ok(destination);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateDestination([FromBody] DestinationDTO dto)
        {
            var destination = _mapper.Map<Destination>(dto);
            await _destinationService.AddAsync(destination);
            return CreatedAtAction(nameof(CreateDestination), new { id = destination.Id }, destination);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDestination(Guid id, [FromBody] DestinationDTO dto)
        {
            var destination = _mapper.Map<Destination>(dto);
            destination.Id = id;

            await _destinationService.UpdateAsync(destination);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDestination(Guid id)
        {
            await _destinationService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchDestinationsByAddress([FromQuery] string address)
        {
            var destinations = await _destinationService.SearchByAddress(address);
            return Ok(destinations);
        }

    }
}
