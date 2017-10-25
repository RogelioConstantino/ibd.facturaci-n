using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibd.SiMer.Entidades
{
    public class BasesContratoEN
    {
        public long IdArchivo { get; set; }
        public int IdPuntoCarga { get; set; }
        public string RPU { get; set; }
        public string RMU { get; set; }
        public string Codigo { get; set; }
        
        public DateTime? FechaCreacion { get; set; }
        public int Activo { get; set; }
                
        public double DescuentoEnergia_B { get; set; }
        public double DescuentoEnergia_I { get; set; }
        public double DescuentoEnergia_P { get; set; }
        public double DescuentoEnergia_SP { get; set; }
        public double DescuentoDemanda { get; set; }
        public double CFPm { get; set; }
        public double CVPm { get; set; }
        public string FechaInicio { get; set; }
        public int Duracion { get; set; }

    }

}
