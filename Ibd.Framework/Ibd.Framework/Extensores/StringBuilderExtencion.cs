using System.Text;

namespace Ibd.Framework.Extensores
{
    /// <summary>
    /// Agrega funcionanildad a los StringBuilder.
    /// </summary>
    /// <history>
    ///     [Oscar López Osorio]    23/01/2013      Creación.
    /// </history>
    public static class StringBuilderExtencion
    {
        public static StringBuilder RemoveLast(this StringBuilder sb, string value)
        {
            if (sb.Length < 1) return sb;
            sb.Remove(sb.ToString().LastIndexOf(value, System.StringComparison.Ordinal), value.Length);
            return sb;
        }
    }
}
