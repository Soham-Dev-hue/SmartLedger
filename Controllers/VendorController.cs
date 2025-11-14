using Microsoft.AspNetCore.Mvc;
using SmartLedger.BAL.Interfaces;
using SmartLedger.DAL.Models;

namespace SmartLedger.Controllers
{
    [ApiController]
    [Route("api/vendors")]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet("{orgId}")]
        public async Task<IActionResult> GetVendors(Guid orgId)
        {
            var result = await _vendorService.GetVendorsAsync(orgId);
            return Ok(result);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetVendor(Guid id)
        {
            var vendor = await _vendorService.GetVendorByIdAsync(id);
            if (vendor == null)
                return NotFound();

            return Ok(vendor);
        }

        [HttpPost("{orgId}")]
        public async Task<IActionResult> CreateVendor(Guid orgId, VendorRequest request)
        {
            var vendor = await _vendorService.CreateVendorAsync(orgId, request);
            return Ok(vendor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(Guid id, VendorRequest request)
        {
            var success = await _vendorService.UpdateVendorAsync(id, request);
            if (!success)
                return NotFound();

            return Ok("Vendor updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(Guid id)
        {
            var success = await _vendorService.DeleteVendorAsync(id);
            if (!success)
                return NotFound();

            return Ok("Vendor deleted.");
        }
    }
}
