using MongoDB.Bson;

namespace Ibd.Framework.AccesoDatos.Mongo
{
    // Esta interfaz hace que todas la que implementen esta interface tengan el campo ID
    // Asi se podra trabajar con una clase gererica
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}