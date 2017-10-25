using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Ibd.Framework.AccesoDatos
{
    public abstract class Mongeable
    {
        public int Id;
    }

    public class BdMongo
    {
        private static string database_name = "IndraFrameWork";
        static MongoServer server;
        
        public static MongoCollection getCollection<T>(string collection_name)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            server = client.GetServer();
            var database = server.GetDatabase(database_name);

            var collectionSettings = new MongoCollectionSettings
                {
                    ReadPreference = ReadPreference.Nearest
                };
            var collection = database.GetCollection<T>(collection_name, collectionSettings);

            return collection;
        }

        public static Mongeable Get<T>(string collection_name, int id) where T : Mongeable
        {
            return getCollection<T>(collection_name).FindOneByIdAs<T>(id);
        }

        public static List<T> GetBy<T>(string collection_name, string field, string value) where T : Mongeable
        {
            return getCollection<T>(collection_name).FindAs<T>(new QueryDocument(field, value)).ToList<T>();
        }

        public static void Save<T>(string collection_name, T document) where T : Mongeable
        {
            getCollection<T>(collection_name).Save(document);
        }

        public static void Set<T>(string collection_name, int id, string field, string value) where T : Mongeable
        {
            getCollection<T>(collection_name).Update(Query<T>.EQ(a => a.Id, id), new UpdateDocument(field, value));
        }

        public static void Delete<T>(string collection_name, int id) where T : Mongeable
        {
            getCollection<T>(collection_name).Remove(Query<T>.EQ(a => a.Id, id));
        }
    }
}