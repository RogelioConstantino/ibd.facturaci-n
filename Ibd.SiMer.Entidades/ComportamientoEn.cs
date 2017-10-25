using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Ibd.SiMer.Entidades
{
    public class ComportamientoEn
    {
        public int IdComportamiento { get; set; }
        public string Comportamiento { get; set; }
        public Boolean Activo { get; set; }
        public DataTable dt { get; set; }
    }
}
