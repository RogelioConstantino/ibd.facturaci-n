using MongoDB.Driver;

namespace Ibd.Framework.AccesoDatos.Mongo
{
    public class MongoConnectionHandler<T> where T : IMongoEntity
    {
        public MongoCollection<T> MongoCollection { get; private set; }

        public MongoConnectionHandler(string collectionName)
        {
            // Se obtiene la cadena de conexion del Web.config
            var connectionString = Config.ObtenerValuePorkeyEnSeccion<string>("MongoConnection", "ConnectionString");
            var client = new MongoClient(connectionString);
            var mongoServer = client.GetServer();
            var database = mongoServer.GetDatabase(Config.ObtenerValuePorkeyEnSeccion<string>("MongoConnection", "DataBase"));
            MongoCollection = database.GetCollection<T>(collectionName);
        }

        public MongoConnectionHandler(string collectionName,string connectionString)
        {
            // Se toma la base de datos de la cadena de conexion
            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            var client = new MongoClient(connectionString);
            var mongoServer = client.GetServer();
            var database = mongoServer.GetDatabase(databaseName);
            MongoCollection = database.GetCollection<T>(collectionName);
        }
    }
}