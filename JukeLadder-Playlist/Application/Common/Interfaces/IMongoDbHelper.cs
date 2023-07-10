using System.Linq.Expressions;

namespace Application.Common.Interfaces;

public interface IMongoDbHelper<T>
{
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task UpdateAsync(Expression<Func<T, bool>> predicate, T entity, CancellationToken cancellationToken);
    Task InsertAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
}
