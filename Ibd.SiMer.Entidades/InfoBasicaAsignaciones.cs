using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibd.SiMer.Entidades
{
    public  class InfoBasicaAsignaciones
    {
        public long IdInfoBasica { get; set; }
        public int? IdPuntoCarga { get; set; }
        public string RPUPuntoCarga { get; set; }
        public int? NumAsignacion { get; set; }
        public int? OrdenAsignacion { get; set; }
        public double DemandaLimite{ get; set; }
    }
}