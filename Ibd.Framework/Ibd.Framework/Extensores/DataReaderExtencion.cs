using System;
using System.Data;

namespace Ibd.Framework.Extensores
{
    public static class DataReaderExtencion
    {
        public static T ToAppFormat<T>(this IDataReader reader, string column)
        {
            var tipo = typeof(T);
            var u = Nullable.GetUnderlyingType(tipo);


            var value = reader[column];
            return (T) ((DBNull.Value.Equals(value))
                            ? null
                            : ((u != null) ? (T)Convert.ChangeType(value, u):Convert.ChangeType(value, typeof (T))));

        }
    }
}