using BTG_core.Models.Commons;
using BTG_core.Repositorie.Commons.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BTG_core.Repositorie.Commons
{
    public class GenericRepository<TDocument> : IGenericRepository<TDocument> where TDocument : class
    {
        protected readonly IMongoCollection<TDocument> _genericCollection;

        public GenericRepository(IDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _genericCollection = database.GetCollection<TDocument>(settings.CollectionName);
        }
        public async Task<List<TDocument>> AddMultipleAsync(List<TDocument> entities)
        {
            await _genericCollection.InsertManyAsync(entities);
            return entities;
        }
       
        public async Task<TDocument> AddAsync(TDocument entity)
        {
            await _genericCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(BsonDocument entity)
        {
            await _genericCollection.DeleteOneAsync(entity);
        }

        public async Task<List<TDocument>> GetAllAsync()
        {
            return await _genericCollection.Find(element => true).ToListAsync();
        }

        public async Task<TDocument> FindOneAsyncBy(BsonDocument predicate)
        {
            return await _genericCollection.Find<TDocument>(predicate).FirstOrDefaultAsync();
        }

        public async Task<ReplaceOneResult> EditAsync(BsonDocument filter, TDocument entity)
        {
            return await _genericCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteMultipleAsync(BsonDocument filter)
        {
            await _genericCollection.DeleteManyAsync(filter);
        }


    }
}
