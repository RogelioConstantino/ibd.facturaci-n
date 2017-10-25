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
    public class MesesNe
    {
        public DataTable obtieneMesesCargados(int iAño)
        {
            var oDa = new MesesDa();
            var Dt = oDa.obtieneMesesCargados( iAño);
            return Dt;
        }
        public DataTable obtieneMesesCargadosSegregacion(int iAño)
        {
            var oDa = new MesesDa();
            var Dt = oDa.obtieneMesesCargadosSegregacion(iAño);
            return Dt;
        }
        public DataTable obtieneMesesCargadosResumen(int iAño)
        {
            var oDa = new MesesDa();
            var Dt = oDa.obtieneMesesCargadosResumen(iAño);
            return Dt;
        }

        
        public DataTable obtieneMesesResumenCFECostosTrans(int iAño)
        {
            var oDa = new MesesDa();
            var Dt = oDa.obtieneMesesResumenCFECostosTrans(iAño);
            return Dt;
        }

        public DataTable obtieneMesesCFECalificados(int iAño)
        {
            var oDa = new MesesDa();
            var Dt = oDa.obtieneMesesCFECalificados(iAño);
            return Dt;
        }

    }

}
