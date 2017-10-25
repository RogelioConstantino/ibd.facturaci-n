namespace Ibd.Framework.AccesoDatos.CadenaConexion
{
    public class ConexionDefault:IConexion
    {
        public string Proveedor()
        {
           return Config.ObtenerValuePorkey<string>("PROVEEDOR_ADONET");
        }

        public string CadenaConexion()
        {
            return Config.ObtenerValuePorkey<string>("CADENA_CONEXION");
        }
    }
}