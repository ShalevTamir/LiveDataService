using LiveDataService.LiveParameters.Models.Dtos;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace LiveDataService.Mongo.Models
{
    public class Frame
    {
        public ObjectId _id { get; set; }

        [BsonElement("timestamp")]
        public long TimeStamp { get; set; }

        [BsonElement("parameters")]
        public List<TelemetryParameterDto> Parameters;


    }
}
