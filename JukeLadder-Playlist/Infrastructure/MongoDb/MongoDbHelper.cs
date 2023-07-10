using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.MongoDb;

public class MongoDbHelper<T> : IMongoDbHelper<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public MongoDbHelper(MongoDbSettings settings)
    {
        var db = new MongoClient(settings.ConnectionString)
            .GetDatabase(settings.DatabaseName);
        if (typeof(T) == typeof(Track))
            _collection = db.GetCollection<T>("Playlist");
        else if (typeof(T) == typeof(PlaylistState))
            _collection = db.GetCollection<T>("PlaylistState");
        else
            throw new NotImplementedException($"MongoDBHelper not implemented for type {typeof(T)}");
    }
    
    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _collection.Find(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _collection.Find(predicate).ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Expression<Func<T, bool>> predicate, T entity, CancellationToken cancellationToken)
    {
        await _collection.ReplaceOneAsync(predicate, entity, cancellationToken: cancellationToken);
    }

    public async Task InsertAsync(T entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        await _collection.DeleteOneAsync(predicate, cancellationToken);
    }
}