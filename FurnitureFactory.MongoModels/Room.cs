﻿namespace FurnitureFactory.MongoModels
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Room : IMongoModel
    {
        public Room(string name)
        {
            this.Id = ObjectId.GenerateNewId().ToString();
            this.Name = name;
        }

        [BsonElement("_id")]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
