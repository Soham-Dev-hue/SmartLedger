using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLedger.BAL.Interfaces;
using SmartLedger.BAL.Models.Organization;

namespace SmartLedger.Api.Controllers
{
    [ApiController]
    [Route("api/v1/organizations")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _orgService;

        public OrganizationController(IOrganizationService orgService)
        {
            _orgService = orgService;
        }

        [HttpPost]
        [Authorize(Roles = "admin,superadmin")]
        public async Task<IActionResult> Create([FromBody] OrganizationCreateDto dto)
        {
            var org = await _orgService.CreateOrganizationAsync(dto);
            return Ok(org);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var org = await _orgService.GetOrganizationAsync(id);
            if (org == null) return NotFound();

            return Ok(org);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _orgService.GetAllOrganizationsAsync();
            return Ok(list);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(Guid id, OrganizationCreateDto dto)
        {
            var updated = await _orgService.UpdateOrganizationAsync(id, dto);
            if (!updated) return NotFound();

            return Ok(new { message = "Organization updated successfully!" });
        }
    }
}
