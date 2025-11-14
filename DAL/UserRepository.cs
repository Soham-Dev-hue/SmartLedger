using Microsoft.EntityFrameworkCore;
using SmartLedger.DAL.Context;
using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Interfaces;

namespace SmartLedger.DAL
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly SmartLedgerDbContext _context;

        public UserRepository(SmartLedgerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsersByOrgAsync(Guid orgId)
        {
            return await _context.Users
                .Where(u => u.OrgId == orgId)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }
    }
}
