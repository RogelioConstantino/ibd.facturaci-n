using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibd.SiMer.Entidades
{
    public class PuntoCargaEn
    {

        public int IdPuntoCarga { get; set; }

        public Int16 IdRegion { get; set; }
        public int IdGrupo { get; set; }
        public int IdCliente { get; set; }
        public string Codigo { get; set; }
        public string PuntoCarga { get; set; }        
        public string RPU { get; set; }
        public string RMU { get; set; }
        public string NoCuenta { get; set; }

        public double PorteoMaximo { get; set; }

        public double DemandaContratada { get; set; }
        public double DescEnergiaBase { get; set; }
        public double DescEnergiaIntermedia { get; set; }
        public double DescEnergiaPunta { get; set; }
        public double DescDemanda { get; set; }

        public Int16 IdEstatus { get; set; }
        
        public Int16 IdTarifa { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }


        


    }

    public class PuntoCargaFac : PuntoCargaEn
    {
        public string  Region { get; set; }
        public string Grupo { get; set; }
        public string Cliente { get; set; }
        public string Tarifa { get; set; }
        public string Ruta { get; set; }
        public string RutaPDF { get; set; }

    }
}