using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Ibd.Framework.Extensores
{
    public static class XmlExtencion
    {
        /// <summary>
        ///     Serializa un objeto a cadena xml sin namespace
        /// </summary>
        /// <typeparam name="T">El tipo de objeto que sera serializado</typeparam>
        /// <param name="objeto">El objeto a serealizar</param>
        /// <returns>El objeto serializado como cadena xml</returns>
        public static string SerializarXml<T>(this T objeto)
        {
            var xmlSerializer = new XmlSerializer(typeof (T));
            var stringWriter = new StringWriter();
            var xmlWriter = new XmlTextWriter(stringWriter) {Formatting = Formatting.Indented};
            var oNamespace = new XmlSerializerNamespaces();
            oNamespace.Add("", ""); // Sin espacios de nombres

            xmlSerializer.Serialize(xmlWriter, objeto, oNamespace);
            return stringWriter.ToString();
        }

        /// <summary>
        ///     Serializa un objeto a cadena xml sin namespace
        /// </summary>
        /// <typeparam name="T">El tipo de objeto que sera serializado</typeparam>
        /// <param name="objeto">El objeto a serealizar</param>
        /// <returns>El objeto serializado como cadena xml</returns>
        public static XmlDocument SerializarXmlDocument<T>(this T objeto)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(objeto.SerializarXml());
            return xmlDoc;
        }
    }
}