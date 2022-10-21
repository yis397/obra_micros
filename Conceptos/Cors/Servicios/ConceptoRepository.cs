using Conceptos.Cors.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Conceptos.Cors.Servicios
{
    public class ConceptoRepository
    {
        private readonly IMongoCollection<Material> _mongo;
        public ConceptoRepository(
    IOptions<MongoSetting> mongoSetting)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";

            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(
                mongoUrl);

            var mongoDatabase = mongoClient.GetDatabase(
               mongoUrl.DatabaseName);

            _mongo = mongoDatabase.GetCollection<Material>("material");
        }
        public async Task addMaterial(Material material)
        {
            await _mongo.InsertOneAsync(material);
        }
    }
}
