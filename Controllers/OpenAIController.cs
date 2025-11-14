using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLedger.BAL.Interfaces;

namespace SmartLedger.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OpenAIController : ControllerBase
{
    private readonly IOpenAIService _openAIService;

    public OpenAIController(IOpenAIService openAIService)
    {
        _openAIService = openAIService;
    }

    [HttpPost("chat")]
    public async Task<IActionResult> GetChatCompletion([FromBody] ChatRequest request)
    {
        try
        {
            var response = await _openAIService.GetChatCompletionAsync(
                request.Prompt,
                request.SystemMessage
            );

            return Ok(new { response });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("analyze-invoice")]
    public async Task<IActionResult> AnalyzeInvoice([FromBody] AnalyzeInvoiceRequest request)
    {
        try
        {
            var analysis = await _openAIService.AnalyzeInvoiceDataAsync(request.InvoiceData);
            return Ok(new { analysis });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("predict-cashflow")]
    public async Task<IActionResult> PredictCashflow([FromBody] CashflowRequest request)
    {
        try
        {
            var prediction = await _openAIService.GenerateCashflowPredictionAsync(request.FinancialData);
            return Ok(new { prediction });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

public record ChatRequest(string Prompt, string? SystemMessage);
public record AnalyzeInvoiceRequest(string InvoiceData);
public record CashflowRequest(string FinancialData);
