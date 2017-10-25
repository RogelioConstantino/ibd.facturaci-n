using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ibd.Framework.AccesoDatos.Mongo
{
    //Esta clase hace que se mapee el campo Id a _id de la coleccion
    public class MongoEntity : IMongoEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}