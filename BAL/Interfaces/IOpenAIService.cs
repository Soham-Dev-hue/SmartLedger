namespace SmartLedger.BAL.Interfaces;

public interface IOpenAIService
{
    Task<string> GetChatCompletionAsync(string prompt, string? systemMessage = null);
    Task<string> AnalyzeInvoiceDataAsync(string invoiceData);
    Task<string> GenerateCashflowPredictionAsync(string financialData);
}
