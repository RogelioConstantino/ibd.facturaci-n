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
    public static class IntExtencion
    {
        public static object ToDBNull(this int? dInt)
        {
            if (dInt == null)
            {
                return DBNull.Value;
            }
            return dInt;
        }

        public static object ToChr(this int charCode)
        {
            if (charCode > 255)
                throw new ArgumentOutOfRangeException("charCode", charCode, "charCode de estar entre 0 y 255.");
            return Encoding.Default.GetString(new[] { (byte)charCode });
        }
    }
}
