using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLedger.BAL.Interfaces;

namespace SmartLedger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CashflowPredictionController : ControllerBase
    {
        private readonly ICashflowService _cashflowService;

        public CashflowPredictionController(ICashflowService cashflowService)
        {
            _cashflowService = cashflowService;
        }

        [HttpGet("{orgId}")]
        public async Task<IActionResult> GetPredictions(Guid orgId)
        {
            var result = await _cashflowService.GetPredictionsAsync(orgId);
            return Ok(result);
        }

        [HttpGet("{orgId}/{year}/{month}")]
        public async Task<IActionResult> GetPredictionForMonth(Guid orgId, int year, int month)
        {
            var date = new DateOnly(year, month, 1);
            var result = await _cashflowService.GetPredictionForMonthAsync(orgId, date);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("generate/{orgId}")]
        public async Task<IActionResult> GeneratePrediction(Guid orgId)
        {
            var result = await _cashflowService.GeneratePredictionAsync(orgId);
            return Ok(result);
        }
    }
}
