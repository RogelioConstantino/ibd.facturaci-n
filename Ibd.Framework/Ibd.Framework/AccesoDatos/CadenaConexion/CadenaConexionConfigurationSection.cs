using System.Configuration;

namespace Ibd.Framework.AccesoDatos.CadenaConexion
{
    /* Representa la primer jerarquia */
    /// <summary>
    /// Esta contiene una colección de ítems, es por eso que se asocia a “ProveedorDatosCollection”..
    /// </summary>
    public class CadenaConexionConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("CadenasConexion")]
        public CadenaConexionCollection Elementos => ((CadenaConexionCollection)(base["CadenasConexion"]));
    }
}