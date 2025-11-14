using SmartLedger.BAL.Models.User;

namespace SmartLedger.BAL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersByOrgAsync(Guid orgId);
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<bool> UpdateUserRoleAsync(Guid id, string newRole);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
