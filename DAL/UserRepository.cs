using SmartLedger.DAL.Context;
using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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
    }
}
