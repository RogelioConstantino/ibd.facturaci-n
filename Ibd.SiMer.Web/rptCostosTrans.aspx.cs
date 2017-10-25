using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ibd.SiMer.Negocio;

using System.Text;

using System.Data;
//using System.Linq;
using System.Configuration;
using System.Data.SqlClient;

using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Ibd.SiMer.Web
{
    public partial class rptCostosTrans : System.Web.UI.Page
    {
        StringBuilder strHTMLElectric = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DataTable dtG;

                AñosNe clsNe = new AñosNe();
                dtG = clsNe.obtieneAñosResumenCFECostosTrans();
                DataSet ds = new DataSet(); ds.Tables.Add(dtG.Copy());
                ddl_year.DataSource = dtG;
                ddl_year.DataTextField = "año";
                ddl_year.DataValueField = "año";
                ddl_year.DataBind();

                MesesNe clsMesesNe = new MesesNe();
                dtG = clsMesesNe.obtieneMesesResumenCFECostosTrans(int.Parse(ddl_year.SelectedItem.Value));
                ds = new DataSet();
                ds.Tables.Add(dtG.Copy());

                ddl_month.DataSource = dtG;
                ddl_month.DataTextField = "nombreMes";
                ddl_month.DataValueField = "Mes";
                ddl_month.DataBind();
                
                buscar();

            }
        }

        private void buscar()
        {
            int strAño = int.Parse(ddl_year.SelectedValue.ToString());
            int strMes = int.Parse(ddl_month.SelectedValue.ToString());            

            System.Data.DataTable dtGR = new System.Data.DataTable();
            ResumenFacNe oclsRpt = new ResumenFacNe();
            //dtGR = oclsRpt.GetResumenFac(strAño, strMes);

            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                //    Session["dtGR"] = dtGR;
                strHTMLElectric = oclsRpt.CreateTableHTML(dtGR);
                DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
            }
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            buscar();
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            export();
            
        }



        private void export()
        {

            System.Data.DataTable dtGR;
            DataSet dsGR = new DataSet();
            try
            {

                string strAño = ddl_year.Items[ddl_year.SelectedIndex].Value;
                string strMes = ddl_month.Items[ddl_month.SelectedIndex].Value;


                rptCostosTransmisionNe oclsRpt = new rptCostosTransmisionNe();

                dtGR = oclsRpt.GetGeneralReport(strAño, strMes);

                if (dtGR.Rows.Count > 0)
                {
                    //ExporttoExcel(dtGR);
                    dsGR.Tables.Add(dtGR);

                    ExporttoExcelClosedXML(dsGR, strAño, strMes);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                // Response.Redirect("resumengeneral.aspx");
            }
        }

        private void ExporttoExcelClosedXML(DataSet ds, string año, string mes)
        {
            string strNamefileTemplate = "";
            strNamefileTemplate = "C:\\Iberdrola\\prjs\\req\\costos\\Costos_de_Transmision_2017-07.xlsx";

            var workbook = new XLWorkbook(strNamefileTemplate, XLEventTracking.Disabled);
            
            var ws_CTME = workbook.Worksheet("CTME");

            System.Data.DataTable tableCTE = ds.Tables[0];                       
            
            string strRPU = string.Empty;
            string strRMU = string.Empty;
            string strCode = string.Empty;
            string strCol = string.Empty;
                                    
                        
                int rowIdex = 2;

                // datos
                int k = 0;
                int j = 0;
                for (j = 0; j < tableCTE.Rows.Count ; j++)
                {
                //planta
                ws_CTME.Cell(ColumnLetter(0) + (j + 3)).Value = tableCTE.Rows[j].ItemArray[0].ToString().ToUpper() ?? string.Empty;
                // Contrato id
                ws_CTME.Cell(ColumnLetter(1) + (j + 3)).Value = tableCTE.Rows[j].ItemArray[1].ToString().ToUpper() ?? string.Empty;
                // Punto de carga
                ws_CTME.Cell(ColumnLetter(2) + (j + 3)).Value = tableCTE.Rows[j].ItemArray[2].ToString().ToUpper() ?? string.Empty;
                // Punta
                ws_CTME.Cell(ColumnLetter(5) + (j + 3)).Value = tableCTE.Rows[j].ItemArray[5].ToString().ToUpper() ?? string.Empty;
                // Intermedia
                ws_CTME.Cell(ColumnLetter(6) + (j + 3)).Value = tableCTE.Rows[j].ItemArray[6].ToString().ToUpper() ?? string.Empty;
                // Base
                ws_CTME.Cell(ColumnLetter(7) + (j + 3)).Value = tableCTE.Rows[j].ItemArray[7].ToString().ToUpper() ?? string.Empty;
                // Demanda Máx
                ws_CTME.Cell(ColumnLetter(8) + (j + 3)).Value = tableCTE.Rows[j].ItemArray[8].ToString().ToUpper() ?? string.Empty;
                // Demanda facturable
                ws_CTME.Cell(ColumnLetter(9) + (j + 3)).Value = tableCTE.Rows[j].ItemArray[9].ToString().ToUpper() ?? string.Empty;
                // Porteo
                ws_CTME.Cell(ColumnLetter(10) + (j + 3)).Value = tableCTE.Rows[j].ItemArray[10].ToString().ToUpper() ?? string.Empty;
                

            }
                
            string strPathReports = GetPathUploadReports();
            string strNamefile = "CostosTrasmision_" + año + "_" + mes + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
            string strFullPath = Server.MapPath(strPathReports) + strNamefile;

            workbook.SaveAs(strFullPath);

            string strUrl = "Bajarresumengral.aspx?n=" + strNamefile;
            Response.Redirect(strUrl, true);
        }

        private string ColumnLetter(int intCol)
        {
            var intFirstLetter = ((intCol) / 676) + 64;
            var intSecondLetter = ((intCol % 676) / 26) + 64;
            var intThirdLetter = (intCol % 26) + 65;

            var firstLetter = (intFirstLetter > 64) ? (char)intFirstLetter : ' ';
            var secondLetter = (intSecondLetter > 64) ? (char)intSecondLetter : ' ';
            var thirdLetter = (char)intThirdLetter;

            return string.Concat(firstLetter, secondLetter, thirdLetter).Trim();
        }

        public String GetPathUploadReports()
        {
            return ConfigurationManager.AppSettings["GuardarReporteGeneral"].ToString();
        }



    }
}