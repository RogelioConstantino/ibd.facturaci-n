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
    public partial class ResumenCFECostosTransCarga : System.Web.UI.Page
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
                dtG = clsNe.obtieneAñosResumenCFECostosTrans();
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
                dtG = clsMesesNe.obtieneMesesResumenCFECostosTrans(int.Parse(ddl_year.SelectedItem.Value));
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


                empresasNe emp = new empresasNe();
                DataTable dtCen;
                dtCen = emp.Consultar();
                DataSet dsCen = new DataSet();
                dsCen.Tables.Add(dtCen.Copy());

                ddl_centrales.DataSource = dsCen;
                ddl_centrales.DataTextField = "DesEmpresa";
                ddl_centrales.DataValueField = "IdEmpresa";
                ddl_centrales.DataBind();

                ddl_centrales2.DataSource = dsCen;
                ddl_centrales2.DataTextField = "DesEmpresa";
                ddl_centrales2.DataValueField = "IdEmpresa";
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

                int iCentral = int.Parse(ddl_centrales2.SelectedValue.ToString());
                if (iCentral == 1)
                    ImportExcelCDUII();
                else
                    ImportExcelCDUII();
            }
            else
            {
                //Your logic for cancel button
                saA = "No";
            }
        }

        protected void ImportExcel()
        {
            int PuntosCarga = 4;
            int PuntoCarga = 0;
            int TipoRenglonResumen = 1;

            progressBarr.Visible = true;

            ArchivoResumenFacEn archivo = new ArchivoResumenFacEn();
            ResumenFacEn reg = new ResumenFacEn();

            ArchivoResumenFacNe archivoHeader = new ArchivoResumenFacNe();
            //ResumenFacNe archivoHeader = new ResumenFacNe();

            int iAño = int.Parse(ddl_year2.SelectedValue.ToString());
            int iMes = int.Parse(ddl_month2.SelectedValue.ToString());
            int iCentral = int.Parse(ddl_centrales2.SelectedValue.ToString());

            string filePath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(filePath);

            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);

                Int64 iKey = 0;
                int iRenglones = 4;

                for (iRenglones = 4; iRenglones < workSheet.RowCount(); iRenglones++)
                {
                    if (iRenglones < 4)
                    {
                        var cell1 = workSheet.Cell(iRenglones, 1).GetValue<double>();
                        var cell2 = workSheet.Cell(iRenglones, 2).GetValue<double>();
                        var cell3 = workSheet.Cell(iRenglones, 3).GetValue<double>();
                    }
                    else if (iRenglones == 4)
                    {
                        archivo.Archivo = FileUpload1.PostedFile.FileName;
                        archivo.IdCentral = iCentral;
                        archivo.año = iAño;
                        archivo.mes = iMes;
                        iKey = archivoHeader.InsertaRegistroResumenHeader(archivo);
                    }
                    else if ((iRenglones > 6) && (iRenglones < 17))
                    {
                        reg.IdArchivo = iKey;
                        reg.IdCentral = archivo.IdCentral;

                        reg.IdTipoRenglonResumen = 1;
                        reg.IdTipoEncabezadoResumen = (iRenglones - 6);
                        reg.IdPuntoCarga = 0;

                        reg.KWH_Base = workSheet.Cell(iRenglones, 3).GetValue<double>();
                        reg.KWH_Intermedia = workSheet.Cell(iRenglones, 4).GetValue<double>();
                        reg.KWH_Punta = workSheet.Cell(iRenglones, 5).GetValue<double>();
                        reg.KWH_SemiPunta = workSheet.Cell(iRenglones, 6).GetValue<double>();
                        reg.KWH_TOTALES = workSheet.Cell(iRenglones, 7).GetValue<double>();

                        reg.KW_Base = workSheet.Cell(iRenglones, 8).GetValue<double>();
                        reg.KW_Intermedia = workSheet.Cell(iRenglones, 9).GetValue<double>();
                        reg.KW_Punta = workSheet.Cell(iRenglones, 10).GetValue<double>();
                        reg.KW_SemiPunta = workSheet.Cell(iRenglones, 11).GetValue<double>();

                        reg.KVARH = workSheet.Cell(iRenglones, 12).GetValue<double>();

                        var FP = workSheet.Cell(iRenglones, 13).GetValue<string>();
                        reg.FP = double.Parse("0" + FP);

                        reg.hrs_Base = workSheet.Cell(iRenglones, 26).GetValue<double>();
                        reg.hrs_Intermedia = workSheet.Cell(iRenglones, 27).GetValue<double>();
                        reg.hrs_Punta = workSheet.Cell(iRenglones, 28).GetValue<double>();
                        reg.hrs_SemiPunta = workSheet.Cell(iRenglones, 29).GetValue<double>();

                        reg.FactorCarga_Base = workSheet.Cell(iRenglones, 30).GetValue<double>();
                        reg.FactorCarga_Intermedia = workSheet.Cell(iRenglones, 31).GetValue<double>();
                        reg.FactorCarga_Punta = workSheet.Cell(iRenglones, 32).GetValue<double>();
                        reg.FactorCarga_SemiPunta = workSheet.Cell(iRenglones, 33).GetValue<double>();

                        Boolean bResult = archivoHeader.InsertaRegistroResumen(reg);
                    }
                    else if (iRenglones == 17)
                    {
                        TipoRenglonResumen++;
                    }
                    else if (iRenglones > 17)
                    {
                        if (workSheet.Cell(iRenglones, 3).GetValue<string>() == "")
                        {
                            iRenglones++;
                            PuntoCarga = 0;
                            TipoRenglonResumen++;
                        }
                        if (PuntoCarga > PuntosCarga)
                        {
                            PuntoCarga = 0;
                            TipoRenglonResumen++;
                        }

                        reg.IdArchivo = iKey;
                        reg.IdCentral = archivo.IdCentral;
                        reg.IdTipoRenglonResumen = TipoRenglonResumen;
                        reg.IdTipoEncabezadoResumen = 0;
                        reg.IdPuntoCarga = PuntoCarga + 18;
                        reg.KWH_Base = workSheet.Cell(iRenglones, 3).GetValue<double>();
                        reg.KWH_Intermedia = workSheet.Cell(iRenglones, 4).GetValue<double>();
                        reg.KWH_Punta = workSheet.Cell(iRenglones, 5).GetValue<double>();
                        reg.KWH_SemiPunta = workSheet.Cell(iRenglones, 6).GetValue<double>();
                        reg.KWH_TOTALES = workSheet.Cell(iRenglones, 7).GetValue<double>();
                        reg.KW_Base = workSheet.Cell(iRenglones, 8).GetValue<double>();
                        reg.KW_Intermedia = workSheet.Cell(iRenglones, 9).GetValue<double>();
                        reg.KW_Punta = workSheet.Cell(iRenglones, 10).GetValue<double>();
                        reg.KW_SemiPunta = workSheet.Cell(iRenglones, 11).GetValue<double>();
                        reg.KVARH = workSheet.Cell(iRenglones, 12).GetValue<double>();
                        var FP = workSheet.Cell(iRenglones, 13).GetValue<string>();
                        reg.FP = double.Parse("0" + FP);
                        reg.hrs_Base = workSheet.Cell(iRenglones, 26).GetValue<double>();
                        reg.hrs_Intermedia = workSheet.Cell(iRenglones, 27).GetValue<double>();
                        reg.hrs_Punta = workSheet.Cell(iRenglones, 28).GetValue<double>();
                        reg.hrs_SemiPunta = workSheet.Cell(iRenglones, 29).GetValue<double>();
                        reg.FactorCarga_Base = workSheet.Cell(iRenglones, 30).GetValue<double>();
                        reg.FactorCarga_Intermedia = workSheet.Cell(iRenglones, 31).GetValue<double>();
                        reg.FactorCarga_Punta = workSheet.Cell(iRenglones, 32).GetValue<double>();
                        reg.FactorCarga_SemiPunta = workSheet.Cell(iRenglones, 33).GetValue<double>();

                        Boolean bResult = archivoHeader.InsertaRegistroResumen(reg);

                        PuntoCarga++;

                        if (iRenglones >= ((5 * (PuntosCarga + 1)) + 10 + 6))
                        {
                            break;
                        }
                    }
                }
                archivo.IdArchivo = iKey;
                archivo.Archivo = FileUpload1.PostedFile.FileName;
                archivo.IdCentral = iCentral;
                archivo.año = iAño;
                archivo.mes = iMes;
                archivo.Mensaje = "Exitoso";
                archivo.NoRegistros = iRenglones;
                iKey = archivoHeader.actualizaRegistroResumenHeader(archivo);
                progressBarr.Visible = false;

                lnkExportar.Visible = true;
                LinkButton3.Visible = true;

                buscar();
            }
        }


        protected void ImportExcelCDUII()
        {
            int PuntosCarga = 126;
            int PuntoCarga = 0;
            int TipoRenglonResumen = 1;

            progressBarr.Visible = true;

            ArchivoResumenFacEn archivo = new ArchivoResumenFacEn();
            ResumenFacEn reg = new ResumenFacEn();

            ArchivoResumenFacNe archivoHeader = new ArchivoResumenFacNe();
            //ResumenFacNe archivoHeader = new ResumenFacNe();

            int iAño = int.Parse(ddl_year2.SelectedValue.ToString());
            int iMes = int.Parse(ddl_month2.SelectedValue.ToString());
            int iCentral = int.Parse(ddl_centrales2.SelectedValue.ToString());

            string filePath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(filePath);

            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);

                Int64 iKey = 0;
                int iRenglones = 4;

                for (iRenglones = 4; iRenglones < workSheet.RowCount(); iRenglones++)
                {
                    if (iRenglones < 4)
                    {
                        var cell1 = workSheet.Cell(iRenglones, 1).GetValue<double>();
                        var cell2 = workSheet.Cell(iRenglones, 2).GetValue<double>();
                        var cell3 = workSheet.Cell(iRenglones, 3).GetValue<double>();
                    }
                    else if (iRenglones == 4)
                    {
                        archivo.Archivo = FileUpload1.PostedFile.FileName;
                        archivo.IdCentral = iCentral;
                        archivo.año = iAño;
                        archivo.mes = iMes;
                        iKey = archivoHeader.InsertaRegistroResumenHeaderCFECostosTrans(archivo);
                    }
                    else if ((iRenglones > 6) && (iRenglones < 17))
                    {
                        reg.IdArchivo = iKey;
                        reg.IdCentral = archivo.IdCentral;

                        reg.IdTipoRenglonResumen = 1;
                        reg.IdTipoEncabezadoResumen = (iRenglones - 6);
                        reg.IdPuntoCarga = 0;

                        //string  sPuntoCarga  = workSheet.Cell(iRenglones, 2).GetValue<string>();

                        reg.KWH_Base = workSheet.Cell(iRenglones, 3).GetValue<double>();
                        reg.KWH_Intermedia = workSheet.Cell(iRenglones, 4).GetValue<double>();
                        reg.KWH_Punta = workSheet.Cell(iRenglones, 5).GetValue<double>();
                        reg.KWH_SemiPunta = workSheet.Cell(iRenglones, 6).GetValue<double>();
                        reg.KWH_TOTALES = workSheet.Cell(iRenglones, 7).GetValue<double>();

                        reg.KW_Base = workSheet.Cell(iRenglones, 8).GetValue<double>();
                        reg.KW_Intermedia = workSheet.Cell(iRenglones, 9).GetValue<double>();
                        reg.KW_Punta = workSheet.Cell(iRenglones, 10).GetValue<double>();
                        reg.KW_SemiPunta = workSheet.Cell(iRenglones, 11).GetValue<double>();

                        reg.KVARH = workSheet.Cell(iRenglones, 12).GetValue<double>();

                        var FP = workSheet.Cell(iRenglones, 13).GetValue<string>();
                        reg.FP = double.Parse("0" + FP);

                        reg.hrs_Base = workSheet.Cell(iRenglones, 26).GetValue<double>();
                        reg.hrs_Intermedia = workSheet.Cell(iRenglones, 27).GetValue<double>();
                        reg.hrs_Punta = workSheet.Cell(iRenglones, 28).GetValue<double>();
                        reg.hrs_SemiPunta = workSheet.Cell(iRenglones, 29).GetValue<double>();

                        reg.FactorCarga_Base = workSheet.Cell(iRenglones, 30).GetValue<double>();
                        reg.FactorCarga_Intermedia = workSheet.Cell(iRenglones, 31).GetValue<double>();
                        reg.FactorCarga_Punta = workSheet.Cell(iRenglones, 32).GetValue<double>();
                        reg.FactorCarga_SemiPunta = workSheet.Cell(iRenglones, 33).GetValue<double>();

                        Boolean bResult = archivoHeader.InsertaRegistroResumenCFECostosTrans(reg);
                    }
                    else if (iRenglones == 17)
                    {
                        TipoRenglonResumen++;
                    }
                    else if (iRenglones > 17)
                    {

                        if (workSheet.Cell(iRenglones, 3).GetValue<string>() == "")
                        {
                            iRenglones++;
                            PuntoCarga = 0;
                            TipoRenglonResumen++;
                        }
                        if (PuntoCarga > PuntosCarga)
                        {
                            PuntoCarga = 0;
                            TipoRenglonResumen++;
                        }


                        if (TipoRenglonResumen == 2 && PuntoCarga > 0)
                        {
                            var sPuntoCarga = workSheet.Cell(iRenglones, 2).GetValue<string>();
                            Boolean bResult2 = archivoHeader.InsertaPuntoCargaCFECostosTrans(iKey, PuntoCarga, sPuntoCarga, iAño, iMes, iCentral, "");
                        }

                        reg.IdArchivo = iKey;
                        reg.IdCentral = archivo.IdCentral;
                        reg.IdTipoRenglonResumen = TipoRenglonResumen;
                        reg.IdTipoEncabezadoResumen = 0;
                        reg.IdPuntoCarga = PuntoCarga ;
                        reg.KWH_Base = workSheet.Cell(iRenglones, 3).GetValue<double>();
                        reg.KWH_Intermedia = workSheet.Cell(iRenglones, 4).GetValue<double>();
                        reg.KWH_Punta = workSheet.Cell(iRenglones, 5).GetValue<double>();
                        reg.KWH_SemiPunta = workSheet.Cell(iRenglones, 6).GetValue<double>();
                        reg.KWH_TOTALES = workSheet.Cell(iRenglones, 7).GetValue<double>();
                        reg.KW_Base = workSheet.Cell(iRenglones, 8).GetValue<double>();
                        reg.KW_Intermedia = workSheet.Cell(iRenglones, 9).GetValue<double>();
                        reg.KW_Punta = workSheet.Cell(iRenglones, 10).GetValue<double>();
                        reg.KW_SemiPunta = workSheet.Cell(iRenglones, 11).GetValue<double>();
                        reg.KVARH = workSheet.Cell(iRenglones, 12).GetValue<double>();
                        var FP = workSheet.Cell(iRenglones, 13).GetValue<string>();
                        reg.FP = double.Parse("0" + FP);
                        reg.hrs_Base = workSheet.Cell(iRenglones, 26).GetValue<double>();
                        reg.hrs_Intermedia = workSheet.Cell(iRenglones, 27).GetValue<double>();
                        reg.hrs_Punta = workSheet.Cell(iRenglones, 28).GetValue<double>();
                        reg.hrs_SemiPunta = workSheet.Cell(iRenglones, 29).GetValue<double>();
                        reg.FactorCarga_Base = workSheet.Cell(iRenglones, 30).GetValue<double>();
                        reg.FactorCarga_Intermedia = workSheet.Cell(iRenglones, 31).GetValue<double>();
                        reg.FactorCarga_Punta = workSheet.Cell(iRenglones, 32).GetValue<double>();
                        reg.FactorCarga_SemiPunta = workSheet.Cell(iRenglones, 33).GetValue<double>();

                        Boolean bResult = archivoHeader.InsertaRegistroResumenCFECostosTrans(reg);

                        PuntoCarga++;

                        if (iRenglones >= ((5 * (PuntosCarga + 1)) + 10 + 6))
                        {
                            break;
                        }
                    }
                }
                archivo.IdArchivo = iKey;
                archivo.Archivo = FileUpload1.PostedFile.FileName;
                archivo.IdCentral = iCentral;
                archivo.año = iAño;
                archivo.mes = iMes;
                archivo.Mensaje = "Exitoso";
                archivo.NoRegistros = iRenglones;
                iKey = archivoHeader.actualizaRegistroResumenHeaderCFECostosTrans(archivo);
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
            int strCentral = int.Parse(ddl_centrales.SelectedValue.ToString());

            System.Data.DataTable dtGR = new System.Data.DataTable();
            ArchivoResumenFacNe oclsRpt = new ArchivoResumenFacNe();
            dtGR = oclsRpt.GetArchivoResumenCFECostosTrans(strAño, strMes, strCentral);


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