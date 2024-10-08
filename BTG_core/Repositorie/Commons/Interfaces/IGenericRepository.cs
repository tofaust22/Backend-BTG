using MongoDB.Bson;
using MongoDB.Driver;

namespace BTG_core.Repositorie.Commons.Interfaces
{
    public interface IGenericRepository<TDocument> where TDocument : class
    {
        Task<List<TDocument>> GetAllAsync();
        Task<TDocument> AddAsync(TDocument document);
        Task<List<TDocument>> AddMultipleAsync(List<TDocument> document);
        Task<ReplaceOneResult> EditAsync(BsonDocument filter, TDocument entity);
        Task DeleteAsync(BsonDocument entity);
        Task<TDocument> FindOneAsyncBy(BsonDocument predicate);
        Task DeleteMultipleAsync(BsonDocument filter);
        

    }
}
