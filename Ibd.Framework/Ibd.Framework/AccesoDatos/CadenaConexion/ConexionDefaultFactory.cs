namespace Ibd.Framework.AccesoDatos.CadenaConexion
{
    public class ConexionDefaultFactory:IConexionFactory
    {
        public IConexion GetConexionObject()
        {
            return new ConexionDefault();
        }
    }
}
