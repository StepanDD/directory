using DirectoryService.Contracts.DepartmentsDtos;
using DirectoryService.Core.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Presenters
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentsService;

        public DepartmentsController(IDepartmentsService departmentsService)
        {
            _departmentsService = departmentsService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto request)
        {
            var departmentId = await _departmentsService.Create(request, CancellationToken.None);
            return Ok(new { Message = "Department created successfully", DepartmentId = departmentId });
        }
        [HttpPatch("{DepartmentId:guid}")]
        public async Task<IActionResult> Patch([FromRoute] Guid departmentId, [FromBody] UpdateDepartmentDto request)
        {
            var updatedDepartmentId = await _departmentsService.Update(departmentId, request, CancellationToken.None);
            return Ok(new { Message = $"Department with id {updatedDepartmentId} patched successfully" });
        }
        [HttpPost("{DepartmentId:guid}/locations/{LocationId:guid}")]
        public async Task<IActionResult> BindLocation(
            [FromRoute] Guid departmentId,
            [FromRoute] Guid locationId,
            [FromBody] bool isPrimary = false)
        {
            await _departmentsService.BindLocation(departmentId, locationId, isPrimary, CancellationToken.None);
            return Ok();
        }
        [HttpGet("{DepartmentId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid departmentId)
        {
            return NotFound(new { Message = $"Department with id {departmentId} not found" });
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDepartmentsDto request)
        {
            return Ok(new List<object>());
        }
        [HttpPut("{DepartmentId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid departmentId, [FromBody] UpdateDepartmentDto request)
        {
            return Ok(new { Message = $"Department with id {departmentId} updated successfully" });
        }
        [HttpDelete("{DepartmentId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid departmentId)
        {
            return Ok(new { Message = $"Department with id {departmentId} deleted successfully" });
        }
    }
}
