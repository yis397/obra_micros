using Conceptos.Cors.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Reflection.Metadata;

namespace Conceptos.Cors.Servicios

    {
        public interface IMongoRepository<TDocument> where TDocument : IDocument
        {
            Task<IEnumerable<TDocument>> GetAll();
            Task AddItem(TDocument document);

        }

        public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
        {
            private readonly IMongoCollection<TDocument> _mongoCollection;
            public MongoRepository(IOptions<MongoSetting> options)
            {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");

            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";
            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var mongoDatabase = mongoClient.GetDatabase(mongoUrl.DatabaseName);

            //var connectionString = "mongodb://localhost:18001/materiales";
            //var mongoUrl = MongoUrl.Create(connectionString);
            //var mongoClient = new MongoClient(mongoUrl);
            //var mongoDatabase = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _mongoCollection = mongoDatabase.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }
        private protected string GetCollectionName(Type documentType)
            {
                return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()).CollectionName;
            }
            public async Task<IEnumerable<TDocument>> GetAll()
            {
                return await _mongoCollection.Find(p => true).ToListAsync();
            }
            public async Task AddItem(TDocument document)
            {
             await _mongoCollection.InsertOneAsync(document);
      
            }
          
        }
    }
