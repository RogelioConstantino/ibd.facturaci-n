using System.Configuration;

namespace Ibd.Framework.AccesoDatos.ConexionesConfig
{
    /// <summary>
    /// Descripción: Esta clase mapea los atributos de los elementos del archivo 
    /// configuración a esta clase. Representa el Elemento.
    /// </summary>
    /// <history>
    ///    [Oscar López Osorio] 11/02/2014 Creación.
    /// </history>
    public class ConexionElement : ConfigurationElement
    {
        [ConfigurationProperty("nombre", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Nombre
        {
            get { return (string)base["nombre"]; }
            set { base["nombre"] = value; }
        }

        [ConfigurationProperty("proveedor", DefaultValue = "System.Data.SqlClient", IsKey = false, IsRequired = false)]
        public string Proveedor
        {
            get { return (string)base["proveedor"]; }
            set { base["proveedor"] = value; }
        }

        [ConfigurationProperty("conexion", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Conexion
        {
            get { return (string)base["conexion"]; }
            set { base["conexion"] = value; }
        }

        [ConfigurationProperty("factory", DefaultValue = "BDFactoryProvider", IsKey = false, IsRequired = false)]
        public string Factory
        {
            get { return (string)base["factory"]; }
            set { base["factory"] = value; }
        }
    }
}
