using System;

namespace Ibd.Framework
{
    public class Globalizacion
    {
        public static string NombreMes(int nMes)
        {
            return TipoTitulo(Convert.ToString(System.Globalization.CultureInfo.GetCultureInfo("es-MX").DateTimeFormat.GetMonthName(nMes).ToUpperInvariant()));
        }

        public static string TipoTitulo(string sTexto)
        {
            var formatter = new System.Globalization.CultureInfo("es-MX", false).TextInfo;
            return formatter.ToTitleCase(sTexto.ToLower());
        }
    }
}