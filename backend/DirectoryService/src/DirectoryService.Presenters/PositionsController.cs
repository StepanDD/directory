using DirectoryService.Contracts.PositionsDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Presenters
{
    [Route("[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePositionDto request)
        {
            return Ok(new { Message = "Position created successfully" });
        }
        [HttpGet("{PositionId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid PositionId)
        {
            return NotFound(new { Message = $"Position with id {PositionId} not found" });
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetPositionsDto request)
        {
            return Ok(new List<object>());
        }
        [HttpPut("{PositionId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid PositionId, [FromBody] UpdatePositionDto request)
        {
            return Ok(new { Message = $"Position with id {PositionId} updated successfully" });
        }
        [HttpDelete("{PositionId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid PositionId)
        {
            return Ok(new { Message = $"Position with id {PositionId} deleted successfully" });
        }
    }
}
