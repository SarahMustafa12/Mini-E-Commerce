using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce.Data_Access;
using Mini_E_Commerce.Repository.IRepository;
using System.Linq.Expressions;
using System.Transactions;

namespace Mini_E_Commerce.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        ApplicationDbContext _context;
        private readonly DbSet<T> _db;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _db.AddAsync(entity);
            await CommitAsync();
            return entity;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true)
        {
            IQueryable<T> query = _db;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();


        }
        public async Task<T> UpdateAsync(T entity)
        {
            _db.Update(entity);
            await CommitAsync();
            return entity;
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>>? filter = null,Expression<Func<T, object>>[]? includes = null,bool tracked = true)
        {
            IQueryable<T> query = _db;

    if (filter != null)
        query = query.Where(filter);

    if (includes != null)
    {
        foreach (var include in includes)
            query = query.Include(include);
    }

    if (!tracked)
        query = query.AsNoTracking();

    return await query.FirstOrDefaultAsync();
        }



    }
}
