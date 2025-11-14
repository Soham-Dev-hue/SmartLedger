using SmartLedger.DAL.Entities;

namespace SmartLedger.DAL.Interfaces
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task<Organization?> GetByNameAsync(string name);
    }
}
