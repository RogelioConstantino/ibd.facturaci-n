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

namespace Ibd.SiMer.Web.Facturacion.CfeCalificados
{
    public partial class CFECalificadosCarga : System.Web.UI.Page
    {
        StringBuilder strHTMLElectric = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkExportar.Visible = true;
            LinkButton3.Visible = true;
            progressBarr.Visible = true;

            if (!IsPostBack)
            {

                AñosNe clsNe = new AñosNe();
                DataTable dtG;
                dtG = clsNe.obtieneAñosCargadosResumen();
                DataSet ds = new DataSet(); ds.Tables.Add(dtG.Copy());
                ddl_year.DataSource = dtG;
                ddl_year.DataTextField = "año";
                ddl_year.DataValueField = "año";
                ddl_year.DataBind();

                ddl_year2.DataSource = dtG;
                ddl_year2.DataTextField = "año";
                ddl_year2.DataValueField = "año";
                ddl_year2.DataBind();

                MesesNe clsMesesNe = new MesesNe();
                dtG = clsMesesNe.obtieneMesesCargadosResumen(int.Parse(ddl_year.SelectedItem.Value));
                ds = new DataSet();
                ds.Tables.Add(dtG.Copy());

                ddl_month.DataSource = dtG;
                ddl_month.DataTextField = "nombreMes";
                ddl_month.DataValueField = "Mes";
                ddl_month.DataBind();

                ddl_month2.DataSource = dtG;
                ddl_month2.DataTextField = "nombreMes";
                ddl_month2.DataValueField = "Mes";
                ddl_month2.DataBind();

                //buscar();
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
            try { 
                    progressBarr.Visible = true;

                    ArchivoCFECalificadosEn archivo = new ArchivoCFECalificadosEn();
                    CFECalificadosEn reg = new CFECalificadosEn();

                    ArchivoCFECalificadosNe archivoHeader = new ArchivoCFECalificadosNe();
                    CFECalificadosNe archivoRegistro = new CFECalificadosNe();

                    int iAño = int.Parse(ddl_year2.SelectedValue.ToString());
                    int iMes = int.Parse(ddl_month2.SelectedValue.ToString());
            
                    int dias =  System.DateTime.DaysInMonth(iAño, iMes); 
                    int dia = 1;

                    string filePath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(filePath);

                    using (XLWorkbook workBook = new XLWorkbook(filePath))
                    {
                        IXLWorksheet workSheet = workBook.Worksheet("Hoja1");

                        Int64 iKey = 0;
                        int iRenglones = 2;

                        for (iRenglones = 2; iRenglones < workSheet.RowCount(); iRenglones++)
                        {
                            if (iRenglones == 2)
                            {
                                archivo.Archivo = FileUpload1.PostedFile.FileName;                        
                                archivo.año = iAño;
                                archivo.mes = iMes;
                                iKey = archivoHeader.InsertaHeader(archivo);
                            }
                            else 
                            {

                                if (workSheet.Cell(iRenglones, 3).GetValue<string>() == "" || workSheet.Cell(iRenglones, 3).GetValue<string>() == "0")
                                {
                                    break;
                                }

                                reg.IdArchivo = iKey;

                                reg.dia = dia; //workSheet.Cell(iRenglones, 3).GetValue<int>();
                                reg.hora = workSheet.Cell(iRenglones, 2).GetValue<int>();

                                reg.TC = workSheet.Cell(iRenglones, 3).GetValue<double>();
                                reg.SML = workSheet.Cell(iRenglones, 4).GetValue<double>();
                                reg.PrecioGas = workSheet.Cell(iRenglones, 5).GetValue<double>();
                                reg.CTUNG = workSheet.Cell(iRenglones, 6).GetValue<double>();
                                reg.Combustible = workSheet.Cell(iRenglones, 7).GetValue<double>();
                                reg.CVOM = workSheet.Cell(iRenglones, 8).GetValue<double>();
                                reg.Transmision = workSheet.Cell(iRenglones, 9).GetValue<double>();
                                reg.CENACE = workSheet.Cell(iRenglones, 10).GetValue<double>();
                                reg.PrecioEnergia = workSheet.Cell(iRenglones, 11).GetValue<double>();
                                reg.PML_JOV_230 = workSheet.Cell(iRenglones, 12).GetValue<double>();
                                reg.TBFin = workSheet.Cell(iRenglones, 14).GetValue<double>();
                                reg.CFECalificados = workSheet.Cell(iRenglones, 15).GetValue<double>();
                                reg.PrecioCFECalificados = workSheet.Cell(iRenglones, 16).GetValue<double>();
                                reg.usuario = "";
                                reg.Activo = 1;                                                

                                //var FP = workSheet.Cell(iRenglones, 13).GetValue<string>();
                                //reg.FP = double.Parse("0" + FP);
                                                
                                Boolean bResult = archivoRegistro.InsertaRegistro(reg);

                                if (reg.hora == 24)
                                {
                                    dia++;
                                }
                            }
                            if (dia > dias)  
                            {
                                break;
                            }

                        }

                        archivo.IdArchivo = iKey;
                        archivo.Archivo = FileUpload1.PostedFile.FileName;                
                        archivo.año = iAño;
                        archivo.mes = iMes;
                        archivo.Mensaje = "Exitoso!";
                        archivo.NoRegistros = iRenglones;
                        iKey = archivoHeader.actualizaHeader(archivo);
                
                        progressBarr.Visible = false;

                        lnkExportar.Visible = true;
                        LinkButton3.Visible = true;

                        buscar();
                    }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
        
        private void buscar()
        {
            int strAño = int.Parse(ddl_year.SelectedValue.ToString());
            int strMes = int.Parse(ddl_month.SelectedValue.ToString());            

            System.Data.DataTable dtGR = new System.Data.DataTable();
            ArchivoCFECalificadosNe oclsRpt = new ArchivoCFECalificadosNe();
            dtGR = oclsRpt.GetArchivo(strAño, strMes);


            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                //    Session["dtGR"] = dtGR;
                strHTMLElectric = oclsRpt.CreateTableHTML(dtGR);
                DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
            }

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            buscar();
        }

    }
}