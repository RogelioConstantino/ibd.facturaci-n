using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibd.SiMer.Entidades
{
    public class CincoMinutalEn
    {
        public int IdArchivoCincominutales { get; set; }
        public string rmu { get; set; }
        public string fecha { get; set; }
        public string ahora { get; set; }
        public double kvarh { get; set; }
        public double kwhe { get; set; }
        public double kwhr { get; set; }
        public string tipo { get; set; }        
    }

}
