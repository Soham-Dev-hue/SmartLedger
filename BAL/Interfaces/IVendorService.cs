using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Models;

namespace SmartLedger.BAL.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<VendorResponse>> GetVendorsAsync(Guid orgId);
        Task<VendorResponse?> GetVendorByIdAsync(Guid id);
        Task<VendorResponse> CreateVendorAsync(Guid orgId, VendorRequest request);
        Task<bool> UpdateVendorAsync(Guid id, VendorRequest request);
        Task<bool> DeleteVendorAsync(Guid id);
    }
}
