using Microsoft.EntityFrameworkCore;
using SmartLedger.DAL.Context;
using SmartLedger.DAL.Entities;
using SmartLedger.DAL.Interfaces;

namespace SmartLedger.DAL
{
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        private readonly SmartLedgerDbContext _context;

        public OrganizationRepository(SmartLedgerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Organization?> GetByNameAsync(string name)
        {
            return await _context.Organizations
                .FirstOrDefaultAsync(o => o.Name.ToLower() == name.ToLower());
        }
    }
}
