using Ibd.SiMer.Entidades;
using Ibd.SiMer.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ibd.SiMer.Web
{
    public partial class Facturar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<PuntoCargaFac> Consulta()
        {
            var lista = new List<PuntoCargaFac>();
            var pcDa = new PuntoCargaNe();
            try
            {
                lista = pcDa.Consultar();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lista;
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<PuntoCargaFac> GenerarAnexos(string Ids, string Mes, string Usuario)
        {
            var ruta = HttpContext.Current.Server.MapPath("~/");

            //var ruta = Server.MapPath("/UploadedFiles");

            var lista = new List<PuntoCargaFac>();
            var pcDa = new PuntoCargaNe();

            try
            {
                lista = pcDa.GenerarAnexos(Ids,Mes,ruta, Usuario);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lista;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<PuntoCargaFac> PuntosCarga()
        {
            var filtro = new EntidadFiltrable<PuntoCargaFac>();
            var centrales = new PuntoCargaNe();
            var result = new Ibd.SiMer.Web.DataTables<PuntoCargaFac>();

            try
            {
                filtro.Filtro = HttpContext.Current.Request.Params["search[value]"];
                filtro.RegistrosPorPagina = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);
                filtro.Pagina = filtro.Datos.Count;// (Convert.ToInt32(HttpContext.Current.Request.Params["start"]) + filtro.RegistrosPorPagina) / filtro.RegistrosPorPagina;
                filtro.OrdenarPor = Convert.ToInt32(HttpContext.Current.Request.Params["order[0][column]"]);
                filtro.Direccion = (HttpContext.Current.Request.Params["order[0][dir]"] == "asc") ? 0 : 1;

                //filtro = centrales.Consultar(filtro);
                //result.draw = Convert.ToInt32(HttpContext.Current.Request.Params["draw"]);
                //result.recordsTotal = filtro.TotalRegistros;
                //result.recordsFiltered = filtro.RegistrosFiltrados;
                //result.data = filtro.Datos;
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            //return result;

            return filtro.Datos;
        }
    }
}