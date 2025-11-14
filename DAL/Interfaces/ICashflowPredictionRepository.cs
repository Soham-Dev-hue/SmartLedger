using SmartLedger.DAL.Entities;

namespace SmartLedger.DAL.Interfaces
{
    public interface ICashflowPredictionRepository : IRepository<CashflowPrediction>
    {
        Task<IEnumerable<CashflowPrediction>> GetByOrgAsync(Guid orgId);
        Task<CashflowPrediction?> GetForMonthAsync(Guid orgId, DateOnly month);
    }
}
