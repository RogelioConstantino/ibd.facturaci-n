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
    public partial class InfoBasica : System.Web.UI.Page
    {
        StringBuilder strHTMLElectric = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkExportar.Visible = true;
            LinkButton3.Visible = true;
            progressBarr.Visible = true;

            if (!IsPostBack)
            {                
                centralesNe cen = new centralesNe();
                DataTable dtCen;
                dtCen = cen.Consultar();
                DataSet dsCen = new DataSet();
                dsCen.Tables.Add(dtCen.Copy());

                ddl_centrales.DataSource = dsCen;
                ddl_centrales.DataTextField = "Central";
                ddl_centrales.DataValueField = "IdCentral";
                ddl_centrales.DataBind();

                ddl_centrales2.DataSource = dsCen;
                ddl_centrales2.DataTextField = "Central";
                ddl_centrales2.DataValueField = "IdCentral";
                ddl_centrales2.DataBind();

                buscar();
            }
        }

        public void OnConfirm(object sender, EventArgs e)
        {
            string saA = "";
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //Your logic for OK button
                // save();
                saA = "Yes";

                ImportExcel();
            }
            else
            {
                //Your logic for cancel button
                saA = "No";
            }
        }

        protected void ImportExcel()
        {
            progressBarr.Visible = true;

            InfoBasicaEn infoBas = new InfoBasicaEn();
            Int64 iKey = 0;

            InfoBasicaPuntosCargaEn InfoBasicaPC = new InfoBasicaPuntosCargaEn();
            InfoBasicaAsignaciones InfoBasicaAsig = new InfoBasicaAsignaciones();

            ArchivoSegregacionEn archivo = new ArchivoSegregacionEn();
            segregacionNe segNe = new segregacionNe();
            ArchivoSegregacionNe archivoHeader = new ArchivoSegregacionNe();
                        
            int iCentral = int.Parse(ddl_centrales.SelectedValue.ToString());
            
            string filePath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(filePath);
            
            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                
                infoBas.IdCentral = int.Parse(ddl_centrales2.SelectedValue);
                infoBas.IdConvenio = 1;

                infoBas.Archivo = FileUpload1.PostedFile.FileName;

                infoBas.FechaAplicacionInicial = DateTime.Parse(  (workSheet.Cell(5, 8).Value.ToString()));
                infoBas.IdTipoCambio = 1;

                infoBas.PotenciaFuenteEnergia = double.Parse(  workSheet.Cell( 6, 3).Value.ToString());
                infoBas.DemandaPotenciaLocal = double.Parse(workSheet.Cell(7, 3).Value.ToString());
                infoBas.CapacidadPorteoTotal = double.Parse(workSheet.Cell(8, 3).Value.ToString());

                infoBas.CapacidadRespaldoFalla = double.Parse(workSheet.Cell(6, 8).Value.ToString());
                infoBas.BandaCompensasión = double.Parse(workSheet.Cell(7, 8).Value.ToString());
                //infoBas.CapacidadRespaldoFalla = double.Parse(workSheet.Cell(8, 8).Value.ToString());
                
                //iKey = infoBas.insertarInfoBas(infoBas);
                
                int iRenglones = 13;

                for (iRenglones = 13; iRenglones < 17; iRenglones++)
                {
                    InfoBasicaPC.IdInfoBasica= iKey;
                    InfoBasicaPC.RPUPuntoCarga = workSheet.Cell(iRenglones, 3).GetValue<string>();
                    InfoBasicaPC.DemandaMaxima = workSheet.Cell(iRenglones, 4).GetValue<double>();
                    InfoBasicaPC.CapacidadPorteo = workSheet.Cell(iRenglones, 5).GetValue<double>();
                    InfoBasicaPC.DemandaContratadaServNormal = workSheet.Cell(iRenglones, 6).GetValue<double>();

                    //Boolean bResult = InfoBasicaPCNeg.InsertaRegistroSegregacion(InfoBasicaPC);

                    InfoBasicaAsig.IdInfoBasica = iKey;
                    InfoBasicaAsig.RPUPuntoCarga = workSheet.Cell(iRenglones, 3).GetValue<string>();

                    InfoBasicaAsig.NumAsignacion = 1;
                    InfoBasicaAsig.OrdenAsignacion = workSheet.Cell(iRenglones, 8).GetValue<int>();
                    InfoBasicaAsig.DemandaLimite = workSheet.Cell(iRenglones, 7).GetValue<double>();

                    InfoBasicaAsig.NumAsignacion = 2;
                    InfoBasicaAsig.OrdenAsignacion = workSheet.Cell(iRenglones, 10).GetValue<int>();
                    InfoBasicaAsig.DemandaLimite = workSheet.Cell(iRenglones, 9).GetValue<double>();

                    //Boolean bResult = InfoBasicaAsigNe.InsertaRegistroSegregacion(InfoBasicaAsig);
                }

                archivo.Archivo = FileUpload1.PostedFile.FileName;
                archivo.IdCentral = iCentral;
                
                archivo.Mensaje = "Exitoso";
                archivo.NoRegistros = iRenglones;
                //iKey = archivoHeader.actualizarHeaderArchivo(archivo);
                
                progressBarr.Visible = false;

                lnkExportar.Visible = true;
                LinkButton3.Visible = true;
            }
        }

        private void buscar()
        {            
            int strCentral = int.Parse(ddl_centrales.SelectedValue.ToString());

            System.Data.DataTable dtGR = new System.Data.DataTable();
            segregacionNe oclsRpt = new segregacionNe();
            //dtGR = oclsRpt.GetArchivoSegregacion(strAño, strMes, strCentral);
            
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