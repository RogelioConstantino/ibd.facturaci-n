using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibd.SiMer.Entidades
{
    public class InfoBasicaEn
    {
        public int IdInfoBasica { get; set; }        
        public int? IdConvenio { get; set; }
        public int? IdCentral { get; set; }

        public int? IdTipoCambio { get; set; }

        public DateTime? FechaAplicacionInicial { get; set; }
        public DateTime? FechaAplicacionFinal { get; set; }

        public double PotenciaFuenteEnergia { get; set; }
        public double DemandaPotenciaLocal { get; set; }
        public double CapacidadPorteoTotal { get; set; }
        public double CapacidadRespaldoFalla { get; set; }
        public double BandaCompensasión { get; set; }

        public string Archivo { get; set; }
        public int? NoRegistros { get; set; }
        public string Mensaje { get; set; }

    }
}
