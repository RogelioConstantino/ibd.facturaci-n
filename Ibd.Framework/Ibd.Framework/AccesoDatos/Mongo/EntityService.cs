using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Ibd.Framework.AccesoDatos.Mongo
{
    public abstract class EntityService<T> : IEntityService<T> where T : IMongoEntity
    {
        protected readonly MongoConnectionHandler<T> MongoConnectionHandler;

        protected EntityService(string collectionName)
        {
            MongoConnectionHandler = new MongoConnectionHandler<T>(collectionName);
        }

        protected EntityService(string collectionName, string connectionString)
        {
            MongoConnectionHandler = new MongoConnectionHandler<T>(collectionName, connectionString);
        }


        public virtual void Insertar(T entity)
        {
            //// Save the entity with safe mode (WriteConcern.Acknowledged)
            var result = MongoConnectionHandler.MongoCollection.Save(
                entity,
                new MongoInsertOptions
                    {
                        WriteConcern = WriteConcern.Acknowledged
                    });

            if (!result.Ok)
            {
                //// Something went wrong
            }
        }

        public virtual void Eliminar(string id)
        {
            var result = MongoConnectionHandler.MongoCollection.Remove(
                Query<T>.EQ(e => e.Id,
                            new ObjectId(id)),
                RemoveFlags.None,
                WriteConcern.Acknowledged);

            if (!result.Ok)
            {
                //// Something went wrong
            }
        }

        public virtual T GetById(string id)
        {
            var entityQuery = Query<T>.EQ(e => e.Id, new ObjectId(id));
            return MongoConnectionHandler.MongoCollection.FindOne(entityQuery);
        }

        public abstract void Actualizar(T entity);
    }
}