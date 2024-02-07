using LiveDataService.Mongo.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LiveDataService.Mongo.Services
{
    public class MongoFramesService
    {
        private readonly IMongoCollection<Frame> _framesCollection;
        private const string MONGO_DB_KEY = "MongoDB";
        private const string TIMESTAMP_MONGO_KEY = "timestamp";
        public MongoFramesService(IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(MONGO_DB_KEY).Get<ArchiveDatabaseSettings>();
            var mongoClient = new MongoClient(databaseSettings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            _framesCollection = mongoDatabase.GetCollection<Frame>(databaseSettings.CollectionName);
        }

        public async Task InsertAsync(Frame frame) =>
            await _framesCollection.InsertOneAsync(frame);

        public async Task<long> CountFrames(long minTimeSpan, long maxTimeSpan)
        {
            FilterDefinition<Frame> filter = Builders<Frame>.Filter.And(
                Builders<Frame>.Filter.Gt(TIMESTAMP_MONGO_KEY, minTimeSpan),
                Builders<Frame>.Filter.Lt(TIMESTAMP_MONGO_KEY, maxTimeSpan)
                );
            var count =  await _framesCollection.CountDocumentsAsync(filter);
            return count;
        }

        public async Task<List<Frame>> GetFrames(long minTimeSpan, long maxTimeSpan, int maxSamplesInPage, int pageNumber)
        {
            var findOptions = new FindOptions<Frame> { 
                Limit = maxSamplesInPage,
                Skip = (pageNumber) * maxSamplesInPage,
                //Sort = Builders<Frame>.Sort.Ascending(TIMESTAMP_MONGO_KEY)
            };
            FilterDefinition<Frame> filter = Builders<Frame>.Filter.And(
                Builders<Frame>.Filter.Gt(TIMESTAMP_MONGO_KEY, minTimeSpan),
                Builders<Frame>.Filter.Lt(TIMESTAMP_MONGO_KEY, maxTimeSpan)
                );
            
            using (IAsyncCursor<Frame> cursor = await _framesCollection.FindAsync(filter, findOptions))
            {
                return cursor.ToList();
            }
        }
    }
}
