using SmartLedger.BAL.Models.Organization;

namespace SmartLedger.BAL.Interfaces
{
    public interface IOrganizationService
    {
        Task<OrganizationDto> CreateOrganizationAsync(OrganizationCreateDto dto);
        Task<OrganizationDto?> GetOrganizationAsync(Guid id);
        Task<IEnumerable<OrganizationDto>> GetAllOrganizationsAsync();
        Task<bool> UpdateOrganizationAsync(Guid id, OrganizationCreateDto dto);
    }
}
