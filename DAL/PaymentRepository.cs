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
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly SmartLedgerDbContext _context;

        public PaymentRepository(SmartLedgerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByInvoiceAsync(Guid invoiceId)
        {
            return await _context.Payments
                .Where(p => p.InvoiceId == invoiceId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByOrgAsync(Guid orgId)
        {
            // Join payments -> invoice -> org (invoice has OrgId)
            return await _context.Payments
                .Include(p => p.Invoice)
                .Where(p => p.Invoice != null && p.Invoice.OrgId == orgId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
