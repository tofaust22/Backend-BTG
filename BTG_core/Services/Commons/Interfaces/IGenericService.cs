using BTG_core.Repositorie.Commons.Interfaces;
using MongoDB.Bson;

namespace BTG_core.Services.Commons.Interfaces
{
    public interface IGenericService<TDocument, TRepository> where TDocument : class where TRepository : IGenericRepository<TDocument>
    {
        TRepository GenericRepository { set; get; }
        Task<List<TDocument>> GetAll();
        Task<TDocument> Create(TDocument request);
        Task<TDocument> GetOneById(ObjectId id);

        Task Remove(ObjectId id);

        Task<TDocument> Update(ObjectId id, TDocument request);


    }
}
