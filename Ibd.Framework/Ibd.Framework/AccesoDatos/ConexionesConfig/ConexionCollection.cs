using System.Configuration;

namespace Ibd.Framework.AccesoDatos.ConexionesConfig
{
    // Representa la segunda jerarquia 
    /// <summary>
    /// Descripción: Esta clase es el conjunto de valores cargados, desde el WebConfig.Conf 
    /// o App.Conf.
    /// </summary>
    /// <history>
    ///    [Oscar López Osorio] 19/02/2014 Creación.
    /// </history>
    [ConfigurationCollection(typeof(ConexionElement), AddItemName = "Conexion")]
    public class ConexionCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConexionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConexionElement)element).Nombre;
        }


        public ConexionElement this[int index]
        {
            get { return (ConexionElement)BaseGet(index); }
        }

        public ConexionElement this[string id]
        {
            get { return (ConexionElement)BaseGet(id); }
        }

    }
}
