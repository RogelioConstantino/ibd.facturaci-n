using System.Collections.Generic;
using Ibd.Framework;
using Ibd.Framework.AccesoDatos;
using Ibd.Framework.AccesoDatos.ConexionesConfig;

namespace Ibd.SiMer.Negocio.Managers
{
    public class ConexionMng
    {
        public Dictionary<string, IBaseDatos> Conexiones { get; set; }

        public ConexionMng()
        {
            //Al iniciar se crean los objetos para acelerar el acceso a los mismos
            Conexiones = new Dictionary<string, IBaseDatos>();
            ObtenerConexiones();
        }

        /// <summary>
        /// Inicializa un Log con base a los datos declarados en el Web.Config o App.Config.
        /// </summary>
        private void ObtenerConexiones()
        {
            var oAppBds = Config.GetConfigSection<ConexionConfigurationSection>("ConexionesSection");

            for (var i = 0; i <= oAppBds.Elementos.Count - 1; i++)
            {
                var bd = new BDFactory().ObtenerBd(oAppBds.Elementos[i].Nombre);
                Conexiones.Add(oAppBds.Elementos[i].Nombre, bd);
            }
        }

        public IBaseDatos Default()
        {
            return Conexiones["Default"];
        }
    }
}
