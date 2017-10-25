using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ibd.SiMer.Negocio;

using System.Text;

using System.Data; 
using System.Configuration;
using System.Data.SqlClient;

using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;

using System.Web.Script.Services;
using System.Web.Services;

using Ibd.SiMer.Entidades;


namespace Ibd.SiMer.Web
{
    public partial class facturacion : System.Web.UI.Page
    {
        string strEmail = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            hdUsuario.Value  = Convert.ToString(Session["IdUsuario"]); 

            if (!IsPostBack)
            {

                AñosNe clsNe = new AñosNe();
                DataTable dtG;
                dtG = clsNe.obtieneAñosCargados();
                DataSet ds = new DataSet(); ds.Tables.Add(dtG.Copy());
                ddl_year.DataSource = dtG;
                ddl_year.DataTextField = "año";
                ddl_year.DataValueField = "año";
                ddl_year.DataBind();


                MesesNe clsMesesNe = new MesesNe();
                dtG = clsMesesNe.obtieneMesesCargados(int.Parse(ddl_year.SelectedItem.Value));
                ds = new DataSet();
                ds.Tables.Add(dtG.Copy());

                ddl_month.DataSource = dtG;
                ddl_month.DataTextField = "nombreMes";
                ddl_month.DataValueField = "numMes";
                ddl_month.DataBind();


                //ComportamientoNe clsCompNe = new ComportamientoNe(); 
                //dtG = clsCompNe.Consultar();
                //ds = new DataSet();
                //ds.Tables.Add(dtG.Copy());

                //ddl_comportamiento.DataSource = dtG;
                //ddl_comportamiento.DataTextField = "Comportamiento";
                //ddl_comportamiento.DataValueField = "IdComportamiento";
                //ddl_comportamiento.DataBind();

                //string[] años = { "Selecione el Año", "2017" };
                //ddl_year.DataSource = años;
                //ddl_year.DataBind();

                //string[] meses = { "Selecione un mes", "Marzo", };
                //ddl_month.DataSource = meses;
                //ddl_month.DataBind();

                //puntosCargaNe clsNe = new puntosCargaNe();


                //string[] puntos = { "Selecione un punto de carga", "Tiendas Soriana S.A. de C.V. Cucapah", "Tiendas Soriana S.A. de C.V. Gato Bronco" };
                //ddl_puntos.DataSource = puntos;

                //ddl_puntos.DataSource = 

                //DataTable dtG;

                //dtG = clsNe.Consultar();

                ////dtG.Rows.Add(0, "0", "-- TODOS --");
                //DataSet ds = new DataSet(); ds.Tables.Add(dtG.Copy());
                ////ds.Tables[0].DefaultView.Sort = "Punto de Carga";
                //ddl_puntos.DataSource = ds;
                //ddl_puntos.DataTextField = "Punto de Carga";
                //ddl_puntos.DataValueField = "IdPuntoCarga";
                //ddl_puntos.DataBind();
                ////ddl_puntos.Items.Add("");
                ////ddl_puntos.SelectedValue = "";

                //ddl_puntos.DataBind();

                //buscar();

            }
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {


            System.Data.DataSet ds;
            System.Data.DataTable dtGR;
            DataSet dsGR = new DataSet();
            try
            {

                string strAño = ddl_year.Items[ddl_year.SelectedIndex].Value;
                int strMes = ddl_month.SelectedIndex + 2;
                int strPunto = 0;// int.Parse( ddl_puntos.SelectedValue.ToString());

                rptConcentradoNe oclsRpt = new rptConcentradoNe();
                ds = oclsRpt.prcFac_get5MinBitHorario(strPunto, strAño, strMes.ToString(),"","0");
                ExporttoExcelClosedXML(ds, int.Parse(strAño), strMes);

                //if (dtGR.Rows.Count > 0)
                //{
                //ExporttoExcel(dtGR);
                //dsGR.Tables.Add(dtGR);
                //}
            }
            catch (Exception ex)
            {
            }
            finally
            {
                // Response.Redirect("resumengeneral.aspx");
            }


        }
        
        private void ExporttoExcelClosedXML(DataSet ds, int strAño, int strMes)
        {
            long k = 0;
            long j = 4;

            try {

                System.Data.DataTable tableTarifa = ds.Tables[2];
                var workbook = new XLWorkbook();
                if (tableTarifa.Rows[0]["fechacorte"].ToString() != "1900-01-01"){
                    workbook = new XLWorkbook("C:\\Iberdrola\\prjs\\req\\Macro\\template_FacturaMercado_1.xlsx");
                }
                else
                {
                    workbook = new XLWorkbook("C:\\Iberdrola\\prjs\\req\\Macro\\template_FacturaMercado.xlsx");
                }

                
                var ws = workbook.Worksheet("5MinBit");

                System.Data.DataTable table = ds.Tables[0];
                //foreach (System.Data.DataTable table in ds.Tables)
                //{
                // ws.Cell(2, 2).Value = table;
                // workbook.Worksheets.Add(table);
                // datos                    
                for (j = 0; j < table.Rows.Count; j++)
                {
                    for (k = 0; k < table.Columns.Count; k++)
                    {
                        ws.Cell(ColumnLetter(k) + (j + 4)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[k].ToString().ToUpper() ?? string.Empty;
                    }
                }
                //}

                System.Data.DataTable tablePuntocarga = ds.Tables[1];
                ws.Cell("C1").Value = tablePuntocarga.Rows[0].ItemArray[0].ToString().ToUpper() ?? string.Empty;
                              

                var ws_Subtotales1 = workbook.Worksheet("Subtotales1");
                ws_Subtotales1.Cell("b1").Value = tablePuntocarga.Rows[0]["PorteoMaximo"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                ws_Subtotales1.Cell("b2").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                ws_Subtotales1.Cell("b3").Value = tableTarifa.Rows[0]["mesaño"].ToString() ?? string.Empty;
                ws_Subtotales1.Cell("b4").Value = tableTarifa.Rows[0]["diasdelmes"].ToString() ?? string.Empty;
                ws_Subtotales1.Cell("b5").Value = tableTarifa.Rows[0]["tarifa"].ToString() ?? string.Empty;               
                ws_Subtotales1.Cell("b6").Value = tableTarifa.Rows[0]["FRI"].ToString() ?? string.Empty;
                ws_Subtotales1.Cell("b7").Value = tableTarifa.Rows[0]["FRB"].ToString() ?? string.Empty;
                               

                var ws_Subtotales2 = workbook.Worksheet("Subtotales2");
                ws_Subtotales2.Cell("b1").Value = tablePuntocarga.Rows[0]["PorteoMaximo"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                ws_Subtotales2.Cell("b2").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;

                // ws_Subtotales2.Cell("b2").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                ws_Subtotales2.Cell("b3").Value = tableTarifa.Rows[0]["mesaño"].ToString() ?? string.Empty;
                ws_Subtotales2.Cell("b4").Value = tableTarifa.Rows[0]["diasdelmes"].ToString() ?? string.Empty;
                ws_Subtotales2.Cell("b5").Value = tableTarifa.Rows[0]["tarifa"].ToString() ?? string.Empty;
                ws_Subtotales2.Cell("b6").Value = tableTarifa.Rows[0]["FRI"].ToString() ?? string.Empty;
                ws_Subtotales2.Cell("b7").Value = tableTarifa.Rows[0]["FRB"].ToString() ?? string.Empty;
                ws_Subtotales2.Cell("b8").Value = tableTarifa.Rows[0]["fechacorte"].ToString() ?? string.Empty;
                
                var ws_Datos = workbook.Worksheet("DATOS");
                ws_Datos.Cell("b28").Value = tableTarifa.Rows[0]["PrecioBase"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                ws_Datos.Cell("b26").Value = tableTarifa.Rows[0]["PrecioIntermedio"].ToString() ?? string.Empty;
                //ws_Datos.Cell("b26").Value = tableTarifa.Rows[0]["PrecioSemipunta"].ToString() ?? string.Empty;
                ws_Datos.Cell("b27").Value = tableTarifa.Rows[0]["PrecioPunta"].ToString() ?? string.Empty;
                ws_Datos.Cell("b30").Value = tableTarifa.Rows[0]["DemandaFacturable"].ToString() ?? string.Empty;
                ws_Datos.Cell("b1").Value = tableTarifa.Rows[0]["Tarifa"].ToString() ?? string.Empty;
                
                
                ws_Datos.Cell("b31").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_B"].ToString() ?? string.Empty;
                ws_Datos.Cell("b32").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_I"].ToString() ?? string.Empty;
                ws_Datos.Cell("b33").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_P"].ToString() ?? string.Empty;
                ws_Datos.Cell("b34").Value = tablePuntocarga.Rows[0]["DescuentoDemanda"].ToString() ?? string.Empty;
                                

                var ws_Datos2 = workbook.Worksheet("DATOS2");
                ws_Datos2.Cell("b29").Value = tableTarifa.Rows[0]["PrecioBase"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                ws_Datos2.Cell("b28").Value = tableTarifa.Rows[0]["PrecioIntermedio"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b1").Value = tableTarifa.Rows[0]["PrecioSemipunta"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b27").Value = tableTarifa.Rows[0]["PrecioPunta"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b30").Value = tableTarifa.Rows[0]["DemandaFacturable"].ToString() ?? string.Empty;
                //ws_Datos2.Cell("b1").Value = tableTarifa.Rows[0]["Tarifa"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b1").Value = tableTarifa.Rows[0]["mesaño"].ToString() ?? string.Empty;

                ws_Datos2.Cell("b31").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_B"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b32").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_I"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b33").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_P"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b34").Value = tablePuntocarga.Rows[0]["DescuentoDemanda"].ToString() ?? string.Empty;

                System.Data.DataTable tableCliente = ds.Tables[3];
                ws_Datos.Cell("b3").Value = tableCliente.Rows[0]["cliente"].ToString() ?? string.Empty;
                ws_Datos.Cell("b4").Value = tableCliente.Rows[0]["Direccion1"].ToString() ?? string.Empty;
                ws_Datos.Cell("b5").Value = tableCliente.Rows[0]["Direccion2"].ToString() ?? string.Empty;
                ws_Datos.Cell("b6").Value = tableCliente.Rows[0]["RFC"].ToString() ?? string.Empty;

                ws_Datos2.Cell("b3").Value = tableCliente.Rows[0]["cliente"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b4").Value = tableCliente.Rows[0]["Direccion1"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b5").Value = tableCliente.Rows[0]["Direccion2"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b6").Value = tableCliente.Rows[0]["RFC"].ToString() ?? string.Empty;


                var ws_anexo = workbook.Worksheet("Anexo2");
                ws_anexo.Cell("D126").Value = tablePuntocarga.Rows[0]["RMU"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                ws_anexo.Cell("I126").Value = tablePuntocarga.Rows[0]["RPU"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;

                ws_Datos2.Cell("b9").Value = tableTarifa.Rows[0]["mesaño"].ToString() ?? string.Empty;


                string strPathReports = GetPathUploadReports();
                string strNamefile = "Anexo_Mercado_" + strAño.ToString() + strMes.ToString() + "_"+ tablePuntocarga.Rows[0]["Codigo"].ToString() +  ".xlsx";
                string strFullPath = Server.MapPath(strPathReports) + strNamefile;

                workbook.SaveAs(strFullPath);

                string strUrl = "Bajarresumengral.aspx?n=" + strNamefile;
                Response.Redirect(strUrl, true);

            }

            catch (Exception ex)
            {
            }
            
}


        public String GetPathUploadReports()
        {
            return ConfigurationManager.AppSettings["GuardarReporteGeneral"].ToString();
        }


        private string ColumnLetter(long intCol)
        {
            var intFirstLetter = ((intCol) / 676) + 64;
            var intSecondLetter = ((intCol % 676) / 26) + 64;
            var intThirdLetter = (intCol % 26) + 65;

            var firstLetter = (intFirstLetter > 64) ? (char)intFirstLetter : ' ';
            var secondLetter = (intSecondLetter > 64) ? (char)intSecondLetter : ' ';
            var thirdLetter = (char)intThirdLetter;

            return string.Concat(firstLetter, secondLetter, thirdLetter).Trim();
        }
        
        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<PuntoCargaFac> Consulta(string Anio, string Mes, string Tipo)
        {             

            var lista = new List<PuntoCargaFac>();
            var pcDa = new PuntoCargaNe();
            try
            {
                lista = pcDa.ConsultarPrevio(Anio, Mes,  Tipo);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lista;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<PuntoCargaFac> GenerarAnexos(string Ids, string Mes, string Usuario, string Tipo, string Minutales)
        {            
            var ruta = HttpContext.Current.Server.MapPath("~/");

            //var ruta = Server.MapPath("/UploadedFiles");

            var lista = new List<PuntoCargaFac>();
            var pcDa = new PuntoCargaNe();

            try
            {
                if (Tipo == "1")
                {
                    lista = pcDa.GenerarAnexos(Ids, Mes, ruta, Usuario, Minutales);
                }
                else
                {                    
                    lista = pcDa.GenerarAnexosAltamira(Ids, Mes, ruta, Usuario);
                }                
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally {
            }

            return lista;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<PuntoCargaFac> GenerarPDF(string Ids, string Mes, string Usuario, string Tipo)
        {
            var ruta = HttpContext.Current.Server.MapPath("~/");            
            var lista = new List<PuntoCargaFac>();
            var pcDa = new PuntoCargaNe();
            try
            {                
                    lista = pcDa.GenerarPDF(Ids, Mes, ruta, Usuario);                
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
            }

            return lista;
        }



    }
}