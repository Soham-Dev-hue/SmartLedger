using Microsoft.EntityFrameworkCore;
using SmartLedger.DAL.Context;
using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Interfaces;

namespace SmartLedger.DAL
{
    public class CashflowPredictionRepository : Repository<CashflowPrediction>, ICashflowPredictionRepository
    {
        private readonly SmartLedgerDbContext _context;

        public CashflowPredictionRepository(SmartLedgerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CashflowPrediction>> GetByOrgAsync(Guid orgId)
        {
            return await _context.CashflowPredictions
                .Where(x => x.OrgId == orgId)
                .OrderByDescending(x => x.Month)
                .ToListAsync();
        }

        public async Task<CashflowPrediction?> GetForMonthAsync(Guid orgId, DateOnly month)
        {
            return await _context.CashflowPredictions
                .FirstOrDefaultAsync(x => x.OrgId == orgId && x.Month == month);
        }
    }
}
