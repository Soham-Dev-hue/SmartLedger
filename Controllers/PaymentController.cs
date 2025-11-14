using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLedger.BAL.Interfaces;
using SmartLedger.DAL.Models;
using System;

namespace SmartLedger.Api.Controllers
{
    [ApiController]
    [Route("api/v1/payments")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentRequest request)
        {
            try
            {
                var res = await _paymentService.AddPaymentAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { message = knf.Message });
            }
        }

        [HttpGet("invoice/{invoiceId}")]
        public async Task<IActionResult> GetByInvoice(Guid invoiceId)
        {
            var res = await _paymentService.GetPaymentsByInvoiceAsync(invoiceId);
            return Ok(res);
        }

        [HttpGet("org/{orgId}")]
        public async Task<IActionResult> GetByOrg(Guid orgId)
        {
            var res = await _paymentService.GetPaymentsByOrgAsync(orgId);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var p = await _paymentService.GetPaymentByIdAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PaymentRequest request)
        {
            var ok = await _paymentService.UpdatePaymentAsync(id, request);
            return ok ? Ok(new { message = "Updated" }) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _paymentService.DeletePaymentAsync(id);
            return ok ? Ok(new { message = "Deleted" }) : NotFound();
        }
    }
}
