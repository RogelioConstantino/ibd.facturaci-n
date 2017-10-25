using System;

namespace Ibd.Framework.Extensores
{
    /// <summary>
    /// Agrega funcionanildad a los DateTime.
    /// </summary>
    /// <history>
    ///     [Oscar López Osorio]    23/01/2013      Creación.
    /// </history>
    public static class DateTimeExtencion
    {
        public static string ToAnsiDateTime(this DateTime dFecha)
        {
            return string.Format("{0}{1}{2} {3}:{4}:{5}", dFecha.Year.ToString("0000"), dFecha.Month.ToString("00"), dFecha.Day.ToString("00"), dFecha.Hour.ToString("00"), dFecha.Minute.ToString("00"), dFecha.Second.ToString("00"));
        }

        public static object ToDBNull(this DateTime? dFecha)
        {
            if (dFecha == null)
            {
                return DBNull.Value;
            }
            return dFecha;
        }

        public static object ToDBNull(this DateTime dFecha)
        {
            // Fecha default .Net
            var def = new DateTime(0001, 01, 01);

            if (dFecha == def)
            {
                return DBNull.Value;
            }
            return dFecha;
        }

        public static int DateToInt(this DateTime dFecha)
        {
            return Convert.ToInt32(string.Format("{0}{1}{2}", dFecha.Year.ToString("0000"), dFecha.Month.ToString("00"), dFecha.Day.ToString("00")));
        }

        public static DateTime ToMongoUtc(this DateTime dFecha)
        {
            var fecha = new DateTime(dFecha.Year, dFecha.Month, dFecha.Day, dFecha.Hour, dFecha.Minute, dFecha.Second,
                dFecha.Millisecond, DateTimeKind.Utc);
            return fecha;
        }
    }
}
