
using System.Collections.Generic;
using Ibd.SiMer.Entidades;
using Ibd.SiMer.Datos;
using Ibd.SiMer.Negocio.Managers;
using Ibd.Framework;

namespace Ibd.SiMer.Negocio
{
    public  class logCargaMinutalNe
    {
        public List<logCargaMinutalEn> Consultar()
        {
            var oDa = new logCargaMinutalDa(Singleton<ConexionMng>.Single.Default());
            var list = oDa.Consultar();
            return list;
        }

        public EntidadFiltrable<logCargaMinutalEn> Consultar(EntidadFiltrable<logCargaMinutalEn> filtro)
        {
            var oDa = new logCargaMinutalDa(Singleton<ConexionMng>.Single.Default());
            var list = oDa.Consultar(filtro);
            return list;
        }
    }
}
