using BTG_core.Models;
using BTG_core.Repositorie.Commons.Interfaces;
using BTG_core.Services.Commons.Interfaces;
using MongoDB.Bson;

namespace BTG_core.Services.Commons
{
    public abstract class GenericService<TDocument, TRepository> : IGenericService<TDocument, TRepository>
        where TDocument : class
        where TRepository : IGenericRepository<TDocument>
    {
        public TRepository GenericRepository { get; set; }
        public async Task<List<TDocument>> GetAll()
        {
            return await GenericRepository.GetAllAsync();
        }
        public async Task<TDocument> Create(TDocument document)
        {
            await GenericRepository.AddAsync(document);
            return document;
        }

        public async Task<TDocument> GetOneById(ObjectId id)
        {
            var response = await GenericRepository.FindOneAsyncBy(new BsonDocument { { "_id", id } });

            if (response == null)
                throw new NotFoundException("No se encontró ningun registro");

            return response;

        }

        public async Task Remove(ObjectId id) =>
             await GenericRepository.DeleteAsync(new BsonDocument("_id", id));

        public abstract Task<TDocument> Update(ObjectId id, TDocument request);


    }
}
