using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibd.SiMer.Entidades
{
    public class InfoBasicaPuntosCargaEn
    {
        public long IdInfoBasica { get; set; }
        public int? IdPuntoCarga { get; set; }
        public string RPUPuntoCarga { get; set; }

        public double DemandaMaxima { get; set; }
        public double CapacidadPorteo { get; set; }
        public double DemandaContratadaServNormal { get; set; }        
    }
}
