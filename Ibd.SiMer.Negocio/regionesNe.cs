﻿using System;
using System.Collections.Generic;
using Ibd.SiMer.Entidades;
using Ibd.SiMer.Datos;
using Ibd.SiMer.Negocio.Managers;
using Ibd.Framework;
using System.Data;
using System.Text;

namespace Ibd.SiMer.Negocio
{
    public class regionesNe
    {
        public DataTable Consultar()
        {
            var oDa = new regionesDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.Consultar();
            return Dt;
        }

    }
}
