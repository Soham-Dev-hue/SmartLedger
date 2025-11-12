using SmartLedger.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLedger.DAL.Interfaces
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<IEnumerable<Invoice>> GetInvoicesByOrgAsync(Guid orgId);
        Task<IEnumerable<Invoice>> GetInvoicesByVendorAsync(Guid vendorId);
        Task<IEnumerable<Invoice>> GetPendingInvoicesAsync(Guid orgId);
    }
}
