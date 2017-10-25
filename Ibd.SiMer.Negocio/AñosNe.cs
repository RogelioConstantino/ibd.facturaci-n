using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ibd.SiMer.Datos;
using Ibd.SiMer.Negocio.Managers;
using Ibd.Framework;
using System.Data;
using System.Text;

namespace Ibd.SiMer.Negocio
{
    public class AñosNe
    {
        public DataTable obtieneAñosCargados( )
        {
            var oDa = new AñosDa();
            var Dt = oDa.obtieneAñosCargados();
            return Dt;
        }
        public DataTable obtieneAñosCargadosSegregacion()
        {
            var oDa = new AñosDa();
            var Dt = oDa.obtieneAñosCargadosSegregacion();
            return Dt;
        }

        public DataTable obtieneAñosCargadosResumen()
        {
            var oDa = new AñosDa();
            var Dt = oDa.obtieneAñosCargadosResumen();
            return Dt;
        }
        public DataTable obtieneAñosResumenCFECostosTrans()
        {
            var oDa = new AñosDa();
            var Dt = oDa.obtieneAñosResumenCFECostosTrans();
            return Dt;
        }

        public DataTable obtieneAñosResumenCFECalificados()
        {
            var oDa = new AñosDa();
            var Dt = oDa.obtieneAñosResumenCFECalificados();
            return Dt;
        }

    }
}
