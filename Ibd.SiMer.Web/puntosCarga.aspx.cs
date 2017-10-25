using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ibd.SiMer.Negocio;
using System.Text;

using System.Web.Script.Services;
using System.Web.Services;

using Ibd.SiMer.Entidades;



namespace Ibd.SiMer.Web
{
    public partial class puntosCarga : System.Web.UI.Page
    {
        StringBuilder strHTML = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

            System.Data.DataTable dtGR = new System.Data.DataTable();

            puntosCargaNe ocls = new puntosCargaNe();

            dtGR = ocls.Consultar();

            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                Session["dtGR"] = dtGR;
                strHTML = ocls.CreateTableHTML(dtGR);
                DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<PuntoCargaFac> getById(string Id)
        {
            var lista = new List<PuntoCargaFac>();
            var pcDa = new PuntoCargaNe();
            try
            {
                lista = pcDa.ConsultarById(Id);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lista;
        }
    }
}