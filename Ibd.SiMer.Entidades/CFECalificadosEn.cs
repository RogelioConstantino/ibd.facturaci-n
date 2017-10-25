using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibd.SiMer.Entidades
{
    public class CFECalificadosEn
    {
        public long IdArchivo { get; set; }
        public int? dia { get; set; }
        public int? hora { get; set; }
        public double TC { get; set; }
        public double SML { get; set; }
        public double PrecioGas { get; set; }
        public double CTUNG { get; set; }
        public double Combustible { get; set; }
        public double CVOM { get; set; }
        public double Transmision { get; set; }
        public double CENACE { get; set; }
        public double PrecioEnergia { get; set; }
        public double PML_JOV_230 { get; set; }
        public double TBFin { get; set; }
        public double CFECalificados { get; set; }
        public double PrecioCFECalificados { get; set; }
        public int Activo { get; set; }

        public string usuario { get; set; }

    }
}
