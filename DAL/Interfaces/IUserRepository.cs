using SmartLedger.DAL.Entities;

namespace SmartLedger.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsersByOrgAsync(Guid orgId);
    }
}
