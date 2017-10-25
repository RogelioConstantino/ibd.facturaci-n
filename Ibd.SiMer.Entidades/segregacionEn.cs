using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibd.SiMer.Entidades
{
    public class segregacionEn
    {
        public long idArchivo { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public double CAPACIDAD { get; set; }
        public double INTERCONEXION { get; set; }
        public double DENTRO { get; set; }
        public double TOLERANCIA { get; set; }
        public double FUERA { get; set; }
        public double PRUEBAS { get; set; }
        public double DECLARADAS { get; set; }
                
    }
}
