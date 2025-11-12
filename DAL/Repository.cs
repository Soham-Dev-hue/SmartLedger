using Microsoft.EntityFrameworkCore;
using SmartLedger.DAL.Context;
using SmartLedger.DAL.Interfaces;
using SmartLedger.DAL;
using System.Linq.Expressions;


namespace SmartLedger.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly SmartLedgerDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(SmartLedgerDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id) =>
            await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public void Update(T entity) =>
            _dbSet.Update(entity);

        public void Remove(T entity) =>
            _dbSet.Remove(entity);

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}

