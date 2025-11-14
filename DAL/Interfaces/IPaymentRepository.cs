using SmartLedger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLedger.DAL.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByInvoiceAsync(Guid invoiceId);
        Task<IEnumerable<Payment>> GetPaymentsByOrgAsync(Guid orgId);
    }
}
