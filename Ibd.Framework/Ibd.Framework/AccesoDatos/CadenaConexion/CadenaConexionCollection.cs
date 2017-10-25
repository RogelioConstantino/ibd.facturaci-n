using System.Configuration;

namespace Ibd.Framework.AccesoDatos.CadenaConexion
{
    /* Representa la segunda jerarquia */

    /// <summary>
    /// Descripción: Esta clase es el conjunto de valores cargados, desde el WebConfig.Conf 
    /// o App.Conf.
    /// </summary>
    [ConfigurationCollection(typeof(CadenaConexionElement), AddItemName = "CadenaConexion")]
    public class CadenaConexionCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new CadenaConexionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CadenaConexionElement)(element)).Id;
        }


        public CadenaConexionElement this[int index]
        {
            get { return (CadenaConexionElement)BaseGet(index); }
        }

        public CadenaConexionElement this[string id]
        {
            get { return (CadenaConexionElement)BaseGet(id); }
        }

    }
}
