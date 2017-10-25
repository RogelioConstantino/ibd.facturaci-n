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
    public partial class SegregacionCarga : System.Web.UI.Page
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
                dtG = clsNe.obtieneAñosCargadosSegregacion();
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
                dtG = clsMesesNe.obtieneMesesCargadosSegregacion(int.Parse(ddl_year.SelectedItem.Value));
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

            ArchivoSegregacionEn archivo = new ArchivoSegregacionEn();            
            segregacionNe segNe = new segregacionNe();
            ArchivoSegregacionNe  archivoHeader = new ArchivoSegregacionNe();

            int iAño = int.Parse(ddl_year.SelectedValue.ToString());
            int iMes = int.Parse(ddl_month.SelectedValue.ToString());
            int iCentral = int.Parse(ddl_centrales.SelectedValue.ToString());


            string filePath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(filePath);


            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);

                Int64 iKey = 0;
                int iRenglones = 1;
                bool firstRow = true;

                foreach (IXLRow row in workSheet.Rows())
                {
                    if (firstRow)
                    {
                        //foreach (IXLCell cell in row.Cells())
                        //{
                        //}
                        archivo.Archivo = FileUpload1.PostedFile.FileName;
                        archivo.IdCentral = iCentral;
                        archivo.año = iAño;
                        archivo.mes = iMes;
                        iKey = archivoHeader.insertarHeaderArchivo(archivo);
                        
                        firstRow = false;
                    }
                    else
                    {
                        iRenglones++;

                        var cell1 = row.Cell(1);
                        string value1 = cell1.GetValue<string>();

                        var cell2 = row.Cell(2);
                        string value2 = cell2.GetValue<string>();

                        var cell3 = row.Cell(3);
                        string value3 = cell3.GetValue<string>();

                        var cell4 = row.Cell(4);
                        string value4 = cell4.GetValue<string>();

                        var cell5 = row.Cell(5);
                        string value5 = cell5.GetValue<string>();

                        var cell6 = row.Cell(6);
                        string value6 = cell6.GetValue<string>();

                        var cell7 = row.Cell(7);
                        string value7 = cell7.GetValue<string>();

                        var cell8 = row.Cell(8);
                        string value8 = cell8.GetValue<string>();

                        segregacionEn en = new segregacionEn();
                        en.fecha = value1;
                        en.CAPACIDAD = double.Parse(value2);
                        en.INTERCONEXION = double.Parse(value3);
                        en.DENTRO = double.Parse(value4);
                        en.TOLERANCIA = double.Parse(value5);
                        en.FUERA = double.Parse(value6);
                        en.PRUEBAS = double.Parse(value7);
                        en.DECLARADAS = double.Parse(value8);
                        en.idArchivo = iKey;
                        Boolean bResult = segNe.InsertaRegistroSegregacion(en);

                    }

                }


                archivo.Archivo = FileUpload1.PostedFile.FileName;
                archivo.IdCentral = iCentral;
                archivo.año = iAño;
                archivo.mes = iMes;
                archivo.Mensaje = "Exitoso";
                archivo.NoRegistros = iRenglones;
                iKey = archivoHeader.actualizarHeaderArchivo(archivo);
                progressBarr.Visible = false;

                lnkExportar.Visible = true;
                LinkButton3.Visible = true;

            }
        }

        private void buscar()
        {
            int strAño = int.Parse(ddl_year.SelectedValue.ToString());
            int strMes =  int.Parse(ddl_month.SelectedValue.ToString());
            int strCentral = int.Parse(ddl_centrales.SelectedValue.ToString());

            System.Data.DataTable dtGR = new System.Data.DataTable();
            segregacionNe oclsRpt = new segregacionNe();
            dtGR = oclsRpt.GetArchivoSegregacion(strAño, strMes, strCentral);
              

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