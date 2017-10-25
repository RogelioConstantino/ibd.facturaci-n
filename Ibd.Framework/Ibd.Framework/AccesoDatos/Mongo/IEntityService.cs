namespace Ibd.Framework.AccesoDatos.Mongo
{
    public interface IEntityService<T> where T : IMongoEntity
    {
        void Insertar(T entity);

        void Eliminar(string id);

        T GetById(string id);

        void Actualizar(T entity);
    }
}
