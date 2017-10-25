using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Ibd.SiMer.Entidades
{
   public class puntosCargaEn
    {
        public int IdPuntoCarga { get; set; }
        public int IdGrupo { get; set; }
        public int IdCliente { get; set; }
        public string RMU { get; set; }
        public string RPU { get; set; }
        public string NombrePuntoCarga { get; set; }
        public Boolean Activo { get; set; }

        public DataTable dt { get; set; }

    }
}
