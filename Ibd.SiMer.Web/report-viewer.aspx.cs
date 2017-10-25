using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.Reporting.WebForms;

using System.Data;

using System.IO;

using System.Configuration;

using Ibd.SiMer.Negocio;




using System.Text;
using System.Data.SqlClient;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;

namespace Ibd.SiMer.Web
{
    public partial class report_viewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                try
                {
                    DataSet dsGR = new DataSet();
                    System.Data.DataTable dtGR = new System.Data.DataTable();
                    try
                    {
                        rptAnaliticaNe oclsRpt = new rptAnaliticaNe();
                        dsGR = oclsRpt.rptFacturacionMercado();
                        //if (dtGR != null && (dtGR.Rows.Count > 0))
                        //    if (dtGR.Rows.Count > 0)
                        //    {
                                dsGR.Tables.Add(dsGR.Tables[0]);
                            //    //ExporttoExcelClosedXML(dsGR);
                            //}
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        // Response.Redirect("resumengeneral.aspx");
                    }


                    string sPath = @"C:\tfs\mercado\Ibd.SiMer.Web\Reportes\rptFactMercado.rdlc";
                    MyReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    //MyReportViewer.Reset();
                    //MyReportViewer.LocalReport.Dispose();
                    
                    MyReportViewer.LocalReport.ReportPath = sPath;
                                        
                    MyReportViewer.LocalReport.DataSources.RemoveAt(0);
                    MyReportViewer.LocalReport.DataSources.Clear();

                    ReportDataSource ds = new ReportDataSource("DSFacMerc", dsGR.Tables[0]);
                    MyReportViewer.LocalReport.DataSources.Add(ds);


                    MyReportViewer.ShowPageNavigationControls = false;
                    MyReportViewer.ShowToolBar = false;


                    //MyReportViewer.ShowParameterPrompts = false;
                    //MyReportViewer.ShowPrintButton = true;

                    //MyReportViewer.DataBind();

                    MyReportViewer.LocalReport.Refresh();
                    //MyReportViewer.Visible = true;

                    MyReportViewer.ShowExportControls = true;
                    
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    // Response.Redirect("resumengeneral.aspx");
                }

            }
            
        }
            protected void lnkExportar_Click(object sender, EventArgs e)
            {

                Warning[] warnings;
                string[] streamids;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                byte[] bytes = MyReportViewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            //  byte[] bytes = rpvPFSolicitud_PAG1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


            //string fileNew = RutaBase + @"\TMP\" + "SolicitudPF_PAG" + NumPagina + "_SOL" + idSolicitud.ToString() + ".pdf";
            string strPathReports = GetPathUploadReports();
            string strNamefile = "ReporteFacturación_Mercado_" + DateTime.Now.ToString("yyyymm") + ".xls";
            string strFullPath = Server.MapPath(strPathReports) + strNamefile;

            if (File.Exists(strNamefile)) {
                File.Delete(strNamefile);
            }

            using (FileStream fs = File.Create(strFullPath))
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
            
            //wbstrFullPath

            string strUrl = "/Bajarresumengral.aspx?n=" + strNamefile;
            Response.Redirect(strUrl, true);
            
        }


        public String GetPathUploadReports()
        {
            return ConfigurationManager.AppSettings["GuardarReporteGeneral"].ToString();
        }


    }
}