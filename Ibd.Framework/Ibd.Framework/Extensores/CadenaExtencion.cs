using System;
using System.Text;

namespace Ibd.Framework.Extensores
{
    /// <summary>
    /// Agrega funcionanildad a los String.
    /// </summary>
    /// <history>
    ///     [Oscar López Osorio]    23/01/2013      Creación.
    /// </history>
    public static class CadenaExtencion
    {
        /// <summary> Se aplica un Trim y regresa si la cadena es <see langword="null"/> o vacia.</summary>
        /// <param name="str">String a evaluar</param>
        /// <returns> Si la cadena es <see langword="null"/> o vacia.</returns>
        public static bool NoEstablecido(this string str)
        {
            return str == null || String.IsNullOrEmpty(str.Trim());
        }

        /// <summary> Se aplica un Trim y regresa si la cadena es <see langword="null"/> o vacia.</summary>
        /// <param name="str">String a evaluar</param>
        /// <returns> Si la cadena es <see langword="null"/> o vacia.</returns>
        public static bool Establecido(this string str)
        {
            return !str.NoEstablecido();
        }

        public static object ToDBNull(this string str)
        {
            if (str == null)
            {
                return DBNull.Value;
            }
            return str;
        }

        public static short ToAsc(this string str)
        {
            return Encoding.Default.GetBytes(str)[0];
        }

        public static DateTime ToDateTime(this string dFecha, string formato)
        {
            var iniAnio = formato.IndexOf('y');
            var finAnio = formato.LastIndexOf('y');
            var iniMes = formato.IndexOf('M');
            var finMes = formato.LastIndexOf('M');
            var iniDia = formato.IndexOf('d');
            var finDia = formato.LastIndexOf('d');

            // En caso que se cuente con hora, minuto o segundo se agrega a la fecha
            var anio = Convert.ToInt32(dFecha.Substring(iniAnio, (finAnio - iniAnio) + 1));
            var mes = Convert.ToInt32(dFecha.Substring(iniMes, (finMes - iniMes) + 1));
            var dia = Convert.ToInt32(dFecha.Substring(iniDia, (finDia - iniDia) + 1));

            if (false)
            {
                return new DateTime(anio, mes, dia);
            }
            else
            {
                return new DateTime(anio, mes, dia);
            }

            /*var hora = Convert.ToInt32(dFecha.Substring(9, 2));
            var minuto = Convert.ToInt32(dFecha.Substring(12, 2));
            var segundo = Convert.ToInt32(dFecha.Substring(15, 2));*/


        }
    }
}
