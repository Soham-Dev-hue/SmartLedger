using SmartLedger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SmartLedger.BAL.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetOrgInvoicesAsync(Guid orgId);
        Task<Invoice?> GetInvoiceByIdAsync(Guid id);
        Task<Invoice> AddInvoiceAsync(Invoice invoice);
        Task<Invoice?> UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(Guid id);
    }
}
