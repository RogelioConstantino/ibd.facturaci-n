using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Ibd.SiMer.Entidades
{
    public class clientesEn
    {        
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public Boolean Activo { get; set; }
        public DataTable dt { get; set; }
    }
}
