using System.Linq.Expressions;

namespace collegeAppBackend.Data.Repository
{
    // Application Level Repository Interface
    public interface ICollegeRepository<T>
    {
        // Retrieves all records of type T asynchronously
        Task<List<T>> GetAllAsync();

        // Retrieves a single record of type T based on a filter expression asynchronously
        // Optionally uses no tracking for the query
        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);

        // Creates a new record of type T asynchronously
        Task<T> CreateAsync(T dbRecord);

        // Updates an existing record of type T asynchronously
        Task<T> UpdateAsync(T dbRecord);

        // Deletes an existing record of type T asynchronously
        Task<bool> DeleteAsync(T dbRecord);
    }
}
