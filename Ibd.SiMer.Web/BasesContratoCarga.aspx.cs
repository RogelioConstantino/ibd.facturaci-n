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
    public partial class BasesContratoCarga : System.Web.UI.Page
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

                ddl_puntoCarga.DataSource = dsCen;
                ddl_puntoCarga.DataTextField = "Central";
                ddl_puntoCarga.DataValueField = "IdCentral";
                ddl_puntoCarga.DataBind();
                                
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
            progressBarr.Visible = true;

            ArchivoBasesContratoEN archivo = new ArchivoBasesContratoEN();
            BasesContratoEN reg = new BasesContratoEN();

            BasesContratoNe archivoHeader = new BasesContratoNe();
            //ResumenFacNe archivoHeader = new ResumenFacNe();

            //int iAño = int.Parse(ddl_year.SelectedValue.ToString());
            //int iMes = int.Parse(ddl_month.SelectedValue.ToString());
            //int iCentral = int.Parse(ddl_puntoCarga.SelectedValue.ToString());

            string filePath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(filePath);

            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);

                Int64 iKey = 0;
                int iRenglones = 2;

                for (iRenglones = 2; iRenglones < workSheet.RowCount(); iRenglones++)
                {
                    if (iRenglones == 2)
                    {
                        archivo.Archivo = FileUpload1.PostedFile.FileName;                                                
                        iKey = archivoHeader.InsertaArchivoBasesContrato(archivo);
                    }
                    else 
                    {
                        if (workSheet.Cell(iRenglones, 1).GetValue<string>() =="") break;

                        reg.IdArchivo = iKey;                        
                        //reg.IdPuntoCarga = workSheet.Cell(iRenglones, 3).GetValue<int>();
                        reg.RPU= workSheet.Cell(iRenglones, 1).GetValue<string>();
                        reg.RMU = workSheet.Cell(iRenglones, 2).GetValue<string>();
                        reg.Codigo = workSheet.Cell(iRenglones, 3).GetValue<string>();

                        reg.FechaInicio = workSheet.Cell(iRenglones, 4).GetValue<string>();
                        reg.Duracion = workSheet.Cell(iRenglones, 5).GetValue<int>();

                        reg.DescuentoEnergia_B = workSheet.Cell(iRenglones, 6).GetValue<double>();
                        reg.DescuentoEnergia_I = workSheet.Cell(iRenglones, 7).GetValue<double>();
                        reg.DescuentoEnergia_P = workSheet.Cell(iRenglones, 8).GetValue<double>();
                        reg.DescuentoEnergia_SP = workSheet.Cell(iRenglones, 9).GetValue<double>();
                        reg.DescuentoDemanda = workSheet.Cell(iRenglones, 10).GetValue<double>();
                        reg.CFPm = workSheet.Cell(iRenglones, 11).GetValue<double>();
                        reg.CVPm = workSheet.Cell(iRenglones, 12).GetValue<double>();
                                                                        
                        Boolean bResult = archivoHeader.InsertaRegistroBasesContrato(reg);                        
                    }
                }

                archivo.IdArchivo = iKey;
                archivo.Archivo = FileUpload1.PostedFile.FileName;                
                archivo.Mensaje = "Exitoso";
                archivo.NoRegistros = iRenglones;
                iKey = archivoHeader.actualizaArchivoBasesContrato(archivo);
                progressBarr.Visible = false;

                lnkExportar.Visible = true;
                LinkButton3.Visible = true;

                buscar();
            }
        }

        private void buscar()
        {
            int strAño = int.Parse(ddl_year.SelectedValue.ToString());
            int strMes = int.Parse(ddl_month.SelectedValue.ToString());
            int strCentral = int.Parse(ddl_puntoCarga.SelectedValue.ToString());

            System.Data.DataTable dtGR = new System.Data.DataTable();
            BasesContratoNe oclsRpt = new BasesContratoNe();
            dtGR = oclsRpt.GetArchivoBasesContrato(strAño, strMes, strCentral);


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