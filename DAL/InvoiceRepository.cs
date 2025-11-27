using Microsoft.EntityFrameworkCore;
using SmartLedger.DAL.Context;
using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLedger.DAL
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
          private readonly SmartLedgerDbContext _context;

        public InvoiceRepository(SmartLedgerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByOrgAsync(Guid orgId)
        {
            return await _context.Set<Invoice>()
                .Where(i => i.OrgId == orgId)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByVendorAsync(Guid vendorId)
        {
            return await _context.Set<Invoice>()
                .Where(i => i.VendorId == vendorId)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetPendingInvoicesAsync(Guid orgId)
        {
            return await _context.Set<Invoice>()
                .Where(i => i.OrgId == orgId && i.Status == "pending")
                .ToListAsync();
        }
    }
}
