using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ibd.SiMer.Negocio;
using System.Data.SqlClient;
using System.Data;

using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;
using Ibd.SiMer.Entidades;
using System.Text;

namespace Ibd.SiMer.Web
{
    public partial class Resumen : System.Web.UI.Page
    {

        StringBuilder strHTMLElectric = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {            

            if (!IsPostBack)
            {
                DataTable dtG;

                AñosNe clsNe = new AñosNe();                
                dtG = clsNe.obtieneAñosCargadosResumen();
                DataSet ds = new DataSet(); ds.Tables.Add(dtG.Copy());
                ddl_year.DataSource = dtG;
                ddl_year.DataTextField = "año";
                ddl_year.DataValueField = "año";
                ddl_year.DataBind();
                
                MesesNe clsMesesNe = new MesesNe();
                dtG = clsMesesNe.obtieneMesesCargadosResumen(int.Parse(ddl_year.SelectedItem.Value));
                ds = new DataSet();
                ds.Tables.Add(dtG.Copy());

                ddl_month.DataSource = dtG;
                ddl_month.DataTextField = "nombreMes";
                ddl_month.DataValueField = "Mes";
                ddl_month.DataBind();
                

                centralesNe cen = new centralesNe();
                DataTable dtCen;
                dtCen = cen.Consultar();
                DataSet dsCen = new DataSet();
                dsCen.Tables.Add(dtCen.Copy());

                ddl_centrales.DataSource = dsCen;
                ddl_centrales.DataTextField = "Central";
                ddl_centrales.DataValueField = "IdCentral";
                ddl_centrales.DataBind();

                buscar();

            }
        }
        
        private void buscar()
        {
            int strAño = int.Parse(ddl_year.SelectedValue.ToString());
            int strMes = int.Parse(ddl_month.SelectedValue.ToString());
            int strCentral = int.Parse(ddl_centrales.SelectedValue.ToString());

            System.Data.DataTable dtGR = new System.Data.DataTable();
            ResumenFacNe oclsRpt = new ResumenFacNe();
            dtGR = oclsRpt.GetResumenFac(strAño, strMes, strCentral);

            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                //    Session["dtGR"] = dtGR;
                strHTMLElectric = oclsRpt.CreateTableHTML(dtGR);
                DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
            }

        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            buscar();
        }

    }
}