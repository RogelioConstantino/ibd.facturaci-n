using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ibd.Framework.Extensores
{
    /// <summary>
    /// Agrega funcionanildad a las Listas Genericas.
    /// </summary>
    /// <history>
    ///     [Luis Marconi Vazquez Bravo]    09/04/2014      Creación.
    /// </history>
    public static class GenericListExtencion
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> oElementos)
        {
            var properties = typeof(T).GetProperties();
            var result = new DataTable();

            foreach (var prop in properties)
            {
                result.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in oElementos)
            {
                var row = result.NewRow();
                foreach (var prop in properties)
                {
                    var itemValue = prop.GetValue(item, new object[] { });
                    row[prop.Name] = itemValue;
                }
                result.Rows.Add(row);
            }
            return result;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> oElementos, string[] columnas)
        {
            var properties = typeof(T).GetProperties();
            var result = new DataTable();


            foreach (var prop in from columna in columnas from prop in properties where prop.Name == columna select prop)
            {
                result.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in oElementos)
            {
                var row = result.NewRow();
                foreach (var columna in columnas)
                {
                    foreach (var prop in properties)
                    {
                        if (columna != prop.Name) continue;
                        var itemValue = prop.GetValue(item, new object[] {});
                        row[prop.Name] = itemValue;
                    }
                }
                result.Rows.Add(row);
            }
            return result;
        }
    
    }
}
