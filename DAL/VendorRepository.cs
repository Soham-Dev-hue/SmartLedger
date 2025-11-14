using Microsoft.EntityFrameworkCore;
using SmartLedger.DAL.Context;
using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Interfaces;

namespace SmartLedger.DAL
{
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        private readonly SmartLedgerDbContext _context;

        public VendorRepository(SmartLedgerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vendor>> GetVendorsByOrgAsync(Guid orgId)
        {
            return await _context.Vendors
                .Where(v => v.OrgId == orgId)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();
        }
    }
}
