using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLedger.BAL.Interfaces;
using SmartLedger.DAL.Entities;

namespace SmartLedger.Controllers
{
    [ApiController]
    [Route("api/v1/invoices")]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("{orgId}")]
        public async Task<IActionResult> GetOrgInvoices(Guid orgId)
        {
            var invoices = await _invoiceService.GetOrgInvoicesAsync(orgId);
            return Ok(invoices);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetInvoice(Guid id)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody] Invoice invoice)
        {
            var result = await _invoiceService.AddInvoiceAsync(invoice);
            return CreatedAtAction(nameof(GetInvoice), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(Guid id, [FromBody] Invoice invoice)
        {
            invoice.Id = id;
            var result = await _invoiceService.UpdateInvoiceAsync(invoice);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            await _invoiceService.DeleteInvoiceAsync(id);
            return NoContent();
        }
    }
}
