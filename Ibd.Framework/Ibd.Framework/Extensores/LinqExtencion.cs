using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibd.Framework.Extensores
{
    /// <summary>
    /// Agrega funcionanildad a los metodos de LINQ.
    /// </summary>
    /// <history>
    ///     [Oscar López Osorio]    09/04/2013      Creación.
    /// </history>
    public static class LinqExtencion
    {
        public static void Update<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
    }
}
