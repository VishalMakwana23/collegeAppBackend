using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace collegeAppBackend.Data.Repository
{
    // Generic repository for handling database operations
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly CollegeDBContext _context;
        private readonly DbSet<T> _dbSet;

        // Constructor to initialize the context and DbSet
        public CollegeRepository(CollegeDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Method to get all records asynchronously
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Method to get a single record based on a filter expression
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(filter);
            }
            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        // Method to create a new record asynchronously
        public async Task<T> CreateAsync(T dbRecord)
        {
            await _dbSet.AddAsync(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        // Method to update an existing record asynchronously
        public async Task<T> UpdateAsync(T dbRecord)
        {
            _dbSet.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        // Method to delete a record asynchronously
        public async Task<bool> DeleteAsync(T dbRecord)
        {
            _dbSet.Remove(dbRecord);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
 