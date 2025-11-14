using SmartLedger.BAL.Interfaces;
using SmartLedger.BAL.Models.Organization;
using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Interfaces;

namespace SmartLedger.BAL.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _orgRepo;

        public OrganizationService(IOrganizationRepository orgRepo)
        {
            _orgRepo = orgRepo;
        }

        public async Task<OrganizationDto> CreateOrganizationAsync(OrganizationCreateDto dto)
        {
            var org = new Organization
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Industry = dto.Industry,
                GstNumber = dto.GstNumber,
                Country = dto.Country,
                Plan = dto.Plan ?? "Free",
                CreatedAt = DateTime.UtcNow
            };

            await _orgRepo.AddAsync(org);
            await _orgRepo.SaveAsync();

            return new OrganizationDto
            {
                Id = org.Id,
                Name = org.Name,
                Industry = org.Industry,
                GstNumber = org.GstNumber,
                Country = org.Country,
                Plan = org.Plan,
                CreatedAt = org.CreatedAt
            };
        }

        public async Task<OrganizationDto?> GetOrganizationAsync(Guid id)
        {
            var org = await _orgRepo.GetByIdAsync(id);

            if (org == null) return null;

            return new OrganizationDto
            {
                Id = org.Id,
                Name = org.Name,
                Industry = org.Industry,
                GstNumber = org.GstNumber,
                Country = org.Country,
                Plan = org.Plan,
                CreatedAt = org.CreatedAt
            };
        }

        public async Task<IEnumerable<OrganizationDto>> GetAllOrganizationsAsync()
        {
            var list = await _orgRepo.GetAllAsync();

            return list.Select(org => new OrganizationDto
            {
                Id = org.Id,
                Name = org.Name,
                Industry = org.Industry,
                GstNumber = org.GstNumber,
                Country = org.Country,
                Plan = org.Plan,
                CreatedAt = org.CreatedAt
            });
        }

        public async Task<bool> UpdateOrganizationAsync(Guid id, OrganizationCreateDto dto)
        {
            var org = await _orgRepo.GetByIdAsync(id);
            if (org == null) return false;

            org.Name = dto.Name;
            org.Industry = dto.Industry;
            org.GstNumber = dto.GstNumber;
            org.Country = dto.Country;
            org.Plan = dto.Plan;

            _orgRepo.Update(org);
            await _orgRepo.SaveAsync();

            return true;
        }
    }
}
