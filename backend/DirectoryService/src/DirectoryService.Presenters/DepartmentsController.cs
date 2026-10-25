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
        [HttpGet("{DepartmentId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid DepartmentId)
        {
            return NotFound(new { Message = $"Department with id {DepartmentId} not found" });
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDepartmentsDto request)
        {
            return Ok(new List<object>());
        }
        [HttpPut("{DepartmentId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid DepartmentId, [FromBody] UpdateDepartmentDto request)
        {
            return Ok(new { Message = $"Department with id {DepartmentId} updated successfully" });
        }
        [HttpDelete("{DepartmentId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid DepartmentId)
        {
            return Ok(new { Message = $"Department with id {DepartmentId} deleted successfully" });
        }
    }
}
