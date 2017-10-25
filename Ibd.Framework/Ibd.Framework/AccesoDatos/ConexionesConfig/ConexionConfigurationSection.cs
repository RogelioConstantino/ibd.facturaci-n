using System.Configuration;

namespace Ibd.Framework.AccesoDatos.ConexionesConfig
{
    public class ConexionConfigurationSection : ConfigurationSection
    {
        /// <history>
        ///    [Oscar López Osorio] 11/09/2012 Creación.
        /// </history>
        [ConfigurationProperty("Conexiones")]
        public ConexionCollection Elementos
        {
            get { return (ConexionCollection)base["Conexiones"]; }
        }
    }
}
