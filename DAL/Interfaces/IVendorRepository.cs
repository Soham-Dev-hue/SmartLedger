using SmartLedger.DAL.Entities;

namespace SmartLedger.DAL.Interfaces
{
    public interface IVendorRepository : IRepository<Vendor>
    {
        Task<IEnumerable<Vendor>> GetVendorsByOrgAsync(Guid orgId);
    }
}
