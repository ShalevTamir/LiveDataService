using LiveDataService.Mongo.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LiveDataService.Mongo.Services
{
    public class MongoFramesService
    {
        private readonly IMongoCollection<Frame> _framesCollection;

        public MongoFramesService(IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection("MongoDB").Get<ArchiveDatabaseSettings>();
            var mongoClient = new MongoClient(databaseSettings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            _framesCollection = mongoDatabase.GetCollection<Frame>(databaseSettings.CollectionName);
        }

        public async Task InsertAsync(Frame frame) =>
            await _framesCollection.InsertOneAsync(frame);
    }
}
