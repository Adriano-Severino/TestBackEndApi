using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBackEndApi.Domain;
using TestBackEndApi.Services.MongoDb;
using WorldOfImagination.Models;

namespace WorldOfImagination.Servicee.MongoDb
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Provider> _booksCollection;

        public MongoDbService(
            IOptions<DataBaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<Provider>(
                bookStoreDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<Provider>> GetAsync() =>
            await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<Provider?> GetAsync(string id) =>
            await _booksCollection.Find(x => x.ObjectId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Provider newBook) =>
            await _booksCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Provider updatedBook) =>
            await _booksCollection.ReplaceOneAsync(x => x.ObjectId == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _booksCollection.DeleteOneAsync(x => x.ObjectId == id);
    }
}
