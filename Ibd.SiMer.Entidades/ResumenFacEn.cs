using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibd.SiMer.Entidades
{
    public class ResumenFacEn
    {
        public long IdArchivo { get; set; }        
        public int IdTipoEncabezadoResumen { get; set; }
        public int IdTipoRenglonResumen { get; set; }
        public int? IdCentral { get; set; }
        public int IdPuntoCarga { get; set; }
              
        public double KWH_Base { get; set; }
        public double KWH_Intermedia { get; set; }
        public double KWH_Punta { get; set; }
        public double KWH_SemiPunta { get; set; }
        public double KWH_TOTALES { get; set; }
        public double KW_Base { get; set; }
        public double KW_Intermedia { get; set; }
        public double KW_Punta { get; set; }
        public double KW_SemiPunta { get; set; }
        public double KVARH { get; set; }
        public double FP { get; set; }
        public double hrs_Base { get; set; }
        public double hrs_Intermedia { get; set; }
        public double hrs_Punta { get; set; }
        public double hrs_SemiPunta { get; set; }
        public double FactorCarga_Base { get; set; }
        public double FactorCarga_Intermedia { get; set; }
        public double FactorCarga_Punta { get; set; }
        public double FactorCarga_SemiPunta { get; set; }

    }
    
}
