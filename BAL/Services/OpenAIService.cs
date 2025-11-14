using Azure.AI.OpenAI;
using OpenAI.Chat;
using SmartLedger.BAL.Interfaces;
using System.ClientModel;

namespace SmartLedger.BAL.Services;

public class OpenAIService : IOpenAIService
{
    private readonly ChatClient _chatClient;
    private readonly string _model;

    public OpenAIService(IConfiguration configuration)
    {
        var apiKey = configuration["OpenAI:ApiKey"]
            ?? throw new InvalidOperationException("OpenAI API Key is not configured");
        _model = configuration["OpenAI:Model"] ?? "gpt-4o-mini";

        var client = new AzureOpenAIClient(new Uri("https://api.openai.com/v1"), new ApiKeyCredential(apiKey));
        _chatClient = client.GetChatClient(_model);
    }

    public async Task<string> GetChatCompletionAsync(string prompt, string? systemMessage = null)
    {
        var messages = new List<ChatMessage>();

        if (!string.IsNullOrEmpty(systemMessage))
        {
            messages.Add(ChatMessage.CreateSystemMessage(systemMessage));
        }

        messages.Add(ChatMessage.CreateUserMessage(prompt));

        var completion = await _chatClient.CompleteChatAsync(messages);
        return completion.Value.Content[0].Text;
    }

    public async Task<string> AnalyzeInvoiceDataAsync(string invoiceData)
    {
        var systemMessage = "You are a financial analyst assistant specialized in invoice analysis. Provide insights on payment patterns, risks, and recommendations.";
        var prompt = $"Analyze the following invoice data and provide insights:\n\n{invoiceData}";

        return await GetChatCompletionAsync(prompt, systemMessage);
    }

    public async Task<string> GenerateCashflowPredictionAsync(string financialData)
    {
        var systemMessage = "You are a financial forecasting expert. Analyze financial data and provide cashflow predictions.";
        var prompt = $"Based on the following financial data, provide a cashflow prediction and analysis:\n\n{financialData}";

        return await GetChatCompletionAsync(prompt, systemMessage);
    }
}
