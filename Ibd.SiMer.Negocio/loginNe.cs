using System;
using System.Collections.Generic;
using Ibd.SiMer.Entidades;
using Ibd.SiMer.Datos;
using Ibd.SiMer.Negocio.Managers;
using Ibd.Framework;
using System.Data;
using System.Text;

namespace Ibd.SiMer.Negocio
{
    public class Login : usuariosEn
    {


        public DataTable GetUser( )
        {
            var oDa = new seguridadDa(Singleton<ConexionMng>.Single.Default());

            oDa.UserName = this.UserName;
            oDa.Password = this.Password;

            var Dt = oDa.GetUser();
            return Dt;
        }

    }
}
