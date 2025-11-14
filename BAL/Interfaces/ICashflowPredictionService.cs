using SmartLedger.BAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLedger.BAL.Interfaces
{
    public interface ICashflowService
    {
        Task<IEnumerable<CashflowPredictionDto>> GetPredictionsAsync(Guid orgId);
        Task<CashflowPredictionDto?> GetPredictionForMonthAsync(Guid orgId, DateOnly month);
        Task<CashflowPredictionDto> GeneratePredictionAsync(Guid orgId);
    }
}
