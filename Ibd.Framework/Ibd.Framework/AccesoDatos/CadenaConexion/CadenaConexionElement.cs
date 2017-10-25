using System.Configuration;

namespace Ibd.Framework.AccesoDatos.CadenaConexion
{
    /* ************************************************ ********************
     
     * ************************************************** ******************* */

    /// <summary>
    /// Descripción: Esta clase mapea los atributos de los elementos del archivo 
    /// configuración a esta clase. Representa el Elemento.
    /// </summary>
    public class CadenaConexionElement : ConfigurationElement
    {
        /// <summary>
        /// Devuelve el valor del ID.
        /// </summary>
        [ConfigurationProperty("id", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Id
        {
            get { return ((string) (base["id"])); }
            set { base["id"] = value; }
        }

        /// <summary>
        /// Devuelve el valor del DbProviderFactory.
        /// </summary>
        [ConfigurationProperty("proveedor", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Proveedor
        {
            get { return ((string) (base["proveedor"])); }
            set { base["proveedor"] = value; }
        }

        /// <summary>
        /// Nombre del servidor.
        /// </summary>
        [ConfigurationProperty("servidor", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Servidor
        {
            get { return ((string) base["servidor"]); }
            set { base["servidor"] = value; }
        }

        [ConfigurationProperty("baseDatos", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string BaseDatos
        {
            get { return ((string) base["baseDatos"]); }
            set { base["baseDatos"] = value; }
        }

        [ConfigurationProperty("autenticacion", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Autenticacion
        {
            get { return ((string) base["autenticacion"]); }
            set { base["autenticacion"] = value; }
        }

        [ConfigurationProperty("usuario", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Usuario
        {
            get { return (string) (base["usuario"]); }
            set { base["usuario"] = value; }
        }

        [ConfigurationProperty("password", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Password
        {
            get { return (string) (base["password"]); }
            set { base["password"] = value; }
        }
    }
}