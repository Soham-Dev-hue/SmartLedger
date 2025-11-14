using SmartLedger.BAL.Interfaces;
using SmartLedger.BAL.Models.User;
using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Interfaces;

namespace SmartLedger.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<UserDto>> GetUsersByOrgAsync(Guid orgId)
        {
            var users = await _userRepo.GetUsersByOrgAsync(orgId);

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role,
                OrgId = u.OrgId,
                CreatedAt = u.CreatedAt
            });
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                OrgId = user.OrgId,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<bool> UpdateUserRoleAsync(Guid id, string newRole)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            user.Role = newRole;
            _userRepo.Update(user);
            await _userRepo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            _userRepo.Remove(user);
            await _userRepo.SaveAsync();
            return true;
        }
    }
}
