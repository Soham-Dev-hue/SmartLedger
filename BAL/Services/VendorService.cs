using SmartLedger.BAL.Interfaces;
using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Interfaces;
using SmartLedger.DAL.Models;

namespace SmartLedger.BAL.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _vendorRepo;

        public VendorService(IVendorRepository vendorRepo)
        {
            _vendorRepo = vendorRepo;
        }

        public async Task<IEnumerable<VendorResponse>> GetVendorsAsync(Guid orgId)
        {
            var vendors = await _vendorRepo.GetVendorsByOrgAsync(orgId);

            return vendors.Select(v => new VendorResponse
            {
                Id = v.Id,
                Name = v.Name,
                Email = v.Email,
                Phone = v.Phone,
                Category = v.Category,
                CreatedAt = v.CreatedAt
            });
        }

        public async Task<VendorResponse?> GetVendorByIdAsync(Guid id)
        {
            var v = await _vendorRepo.GetByIdAsync(id);
            if (v == null) return null;

            return new VendorResponse
            {
                Id = v.Id,
                Name = v.Name,
                Email = v.Email,
                Phone = v.Phone,
                Category = v.Category,
                CreatedAt = v.CreatedAt
            };
        }

        public async Task<VendorResponse> CreateVendorAsync(Guid orgId, VendorRequest request)
        {
            var vendor = new Vendor
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Category = request.Category,
                CreatedAt = DateTime.UtcNow
            };

            await _vendorRepo.AddAsync(vendor);
            await _vendorRepo.SaveAsync();

            return new VendorResponse
            {
                Id = vendor.Id,
                Name = vendor.Name,
                Email = vendor.Email,
                Phone = vendor.Phone,
                Category = vendor.Category,
                CreatedAt = vendor.CreatedAt
            };
        }

        public async Task<bool> UpdateVendorAsync(Guid id, VendorRequest request)
        {
            var vendor = await _vendorRepo.GetByIdAsync(id);
            if (vendor == null) return false;

            vendor.Name = request.Name;
            vendor.Email = request.Email;
            vendor.Phone = request.Phone;
            vendor.Category = request.Category;

            _vendorRepo.Update(vendor);
            await _vendorRepo.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteVendorAsync(Guid id)
        {
            var vendor = await _vendorRepo.GetByIdAsync(id);
            if (vendor == null) return false;

            _vendorRepo.Remove(vendor);
            await _vendorRepo.SaveAsync();
            return true;
        }
    }
}
