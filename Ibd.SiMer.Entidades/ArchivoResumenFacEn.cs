using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibd.SiMer.Entidades
{
    public class ArchivoResumenFacEn
    {
        public long IdArchivo { get; set; }
        public string Archivo { get; set; }
        public int? IdCentral { get; set; }
        public int año { get; set; }
        public int mes { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int Activo { get; set; }
        public int? NoRegistros { get; set; }
        public string Mensaje { get; set; }
    }
}
