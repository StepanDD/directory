using DirectoryService.Contracts.LocationsDtos;
using DirectoryService.Core.Locations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Presenters
{
    [Route("[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsService _locationsService;

        public LocationsController(ILocationsService locationsService)
        {
            _locationsService = locationsService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLocationDto request)
        {
            var locationId = await _locationsService.Create(request, CancellationToken.None);
            return Ok(new { Message = "Location created successfully", LocationId = locationId });
        }
        [HttpGet("{LocationId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid LocationId)
        {
            return NotFound(new { Message = $"Location with id {LocationId} not found" });
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetLocationsDto request)
        {
            return Ok(new List<object>());
        }
        [HttpPut("{LocationId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid LocationId, [FromBody] UpdateLocationDto request)
        {
            return Ok(new { Message = $"Location with id {LocationId} updated successfully" });
        }
        [HttpDelete("{LocationId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid LocationId)
        {
            return Ok(new { Message = $"Location with id {LocationId} deleted successfully" });
        }
    }
}
