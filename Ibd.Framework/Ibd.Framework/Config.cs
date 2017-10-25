using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Collections.Specialized;

namespace Ibd.Framework
{

    /// <summary>
    ///    Clase para el manejo del archivo de web.config o app.config.
    /// </summary>
    /// <history>
    ///    [Oscar López Osorio] 23/01/2014 Creación.
    /// </history>
    public static class Config
    {
        /// <summary>
        /// Obtiene el valor de un appSettings por medio del Key.
        /// </summary>
        /// <param name="sKey">
        /// El config key.
        /// </param>
        /// <returns>
        /// El valor.
        /// </returns>
        public static T ObtenerValuePorkey<T>([NotNull]string sKey)
        {
            var value = ConfigurationManager.AppSettings[sKey];
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(string.Format("No se encuentra el elemento {0} en el Web.Config.", sKey));
            }
            return value == null
                           ? default(T)
                           : (T)Convert.ChangeType(value, typeof(T));
        }

        
        public static IEnumerable<KeyValuePair<string, string>> LeerSeccion([NotNull] string sectionName)
        {
            var nvc = ConfigurationManager.GetSection(sectionName) as NameValueCollection;

            if (nvc == null)
            {
                throw new ArgumentException(string.Format("No se encuentra la sección {0} en el Web.Config.", sectionName));
            }

            var result = (from config in nvc.Cast<string>()
                          select new KeyValuePair<string, string>
                          (
                              config,
                              nvc[config]
                          ));
            return result;
        }

        /// <summary>
        /// Obtiene el valor de un key en una sección espesifica.
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="sKey">
        /// El config key.
        /// </param>
        /// <returns>
        /// El valor.
        /// </returns>
        public static T ObtenerValuePorkeyEnSeccion<T>([NotNull]string sectionName, [NotNull]string sKey)
        {
            //Se seleccionan el key de la seccion
            var value = LeerSeccion(sectionName).Select(ob => ob).FirstOrDefault(ob => ob.Key == sKey).Value;
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(string.Format("No se encuentra el elemento {0} en la sección {1} del Web.Config.", sKey, sectionName));
            }
            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Obtiene una Sección de configuración.
        /// </summary>
        /// <param name="sectionName">
        /// El nombre de la sección.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public static T GetConfigSection<T>(string sectionName) where T : class
        {
            var section = ConfigurationManager.GetSection(sectionName) as T;
            return section;
        }
    }
}
