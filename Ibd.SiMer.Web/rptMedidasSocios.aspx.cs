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
using X14 = DocumentFormat.OpenXml.Office2010.Excel;

namespace Ibd.SiMer.Web
{
    public partial class rptMedidasSocios : System.Web.UI.Page
    {
        StringBuilder strHTMLElectric = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //buscar();


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


            }
        }
        
        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void buscar()
        {
            int strAño = int.Parse(ddl_year.SelectedValue.ToString());
            int strMes = int.Parse(ddl_month.SelectedValue.ToString());
            
            System.Data.DataTable dtGR = new System.Data.DataTable();
            rptMedidasSociosNe oclsRpt = new rptMedidasSociosNe();
            dtGR = oclsRpt.GetMedidasSocios(strAño, strMes);

            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                //    Session["dtGR"] = dtGR;
                strHTMLElectric = oclsRpt.CreateTableHTML(dtGR);
                DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
            }

        }
        
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            export();
        }
        
        private void export()
        {

            DataSet dsGR = new DataSet();
            try
            {
                int strAño = int.Parse(ddl_year.SelectedValue.ToString());
                int strMes = int.Parse(ddl_month.SelectedValue.ToString());
                
                System.Data.DataTable dtGR = new System.Data.DataTable();
                rptMedidasSociosNe oclsRpt = new rptMedidasSociosNe();
                dtGR = oclsRpt.GetMedidasSocios(strAño, strMes);

                if (dtGR != null && (dtGR.Rows.Count > 0))

                    if (dtGR.Rows.Count > 0)
                    {
                        dsGR.Tables.Add(dtGR);
                        ExporttoExcelClosedXML(dsGR);
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
        
        private void ExporttoExcelClosedXML(DataSet ds)
        {
            var wb = new XLWorkbook();

            var ws = wb.Worksheets.Add("Concentrado");
            var cellIdex = 0;
            //int intCount = 0;
            string strX = string.Empty;
            string strY = string.Empty;

            string strRPU = string.Empty;
            string strRMU = string.Empty;
            string strCode = string.Empty;
            string strCol = string.Empty;
            string[] aStrCol;

            UInt32 rowIdex = 3;

            foreach (System.Data.DataTable table in ds.Tables)
            {
                // headers
                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    if (i <9)
                    {
                        ws.Cell(ColumnLetter(cellIdex++) + rowIdex).Value = table.Columns[i - 1].ColumnName.ToString().ToUpper() ?? string.Empty;
                    }
                    else
                    {
                        strCol = table.Columns[i - 1].ColumnName.ToString().ToUpper() ?? string.Empty;
                        aStrCol = strCol.Split('|');
                        ws.Cell(ColumnLetter(cellIdex) + (rowIdex - 2)).Value = aStrCol[0];
                        ws.Cell(ColumnLetter(cellIdex) + (rowIdex - 1)).Value = aStrCol[1];
                        ws.Cell(ColumnLetter(cellIdex) + (rowIdex)).SetDataType(XLCellValues.Text);
                        ws.Cell(ColumnLetter(cellIdex++) + (rowIdex)).SetValue<string>(Convert.ToString(aStrCol[2]));
                        //ws.Cell(ColumnLetter(cellIdex++) + (rowIdex)).Value = aStrCol[2];
                    }

                }

                //// From worksheet
                var rngTable = ws.Range("A1:" + ColumnLetter(cellIdex - 1) + "3");
                // var rngTable = rngTable.Range("A1:J1"); // The address is relative to rngTable (NOT the worksheet)
                rngTable.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngTable.Style.Font.Bold = true;
                rngTable.Style.Fill.BackgroundColor = XLColor.FromArgb(0x558b2f);//  .FromArgb(155, 187, 89);

                var rngHeaders2 = rngTable.Range(ColumnLetter(2) + "1:" + ColumnLetter(cellIdex - 1) + "3"); // The address is relative to rngTable (NOT the worksheet)
                rngHeaders2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngHeaders2.Style.Font.Bold = true;
                rngHeaders2.Style.Fill.BackgroundColor = XLColor.FromArgb(0x8bc34a);

                rowIdex = 2;

                // datos
                int k = 0;
                int j = 0;
                for (j = 2; j < table.Rows.Count + 2; j++)
                {
                    for (k = 0; k < table.Columns.Count; k++)
                    {
                        ws.Cell(ColumnLetter(k) + (j + 2)).Value = table.Rows[j - 2].ItemArray[k].ToString().ToUpper() ?? string.Empty;
                    }
                }

                var rngTableAll = ws.Range("A1:" + ColumnLetter(cellIdex - 1) + (j + 1));
                //Add a thick outside border
                rngTableAll.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                var rngTableNum = ws.Range("C3:" + ColumnLetter(cellIdex - 1) + (j + 1));
                rngTableNum.Style.NumberFormat.Format = "#,##0.000000";

                var col2 = ws.Column(1);
                col2.Width = 5;

                var col3 = ws.Column("B");
                col3.Width = 5;

                //rngTableAll.SetAutoFilter();


                //You can also specify the border for each side with:
                rngTableAll.FirstColumn().Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                rngTableAll.LastColumn().Style.Border.RightBorder = XLBorderStyleValues.Thick;
                rngTableAll.FirstRow().Style.Border.TopBorder = XLBorderStyleValues.Thick;
                rngTableAll.LastRow().Style.Border.BottomBorder = XLBorderStyleValues.Thick;

                rngTableAll.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                /*
                var rngCapacidad = rngTableAll.Range("E2:E" + (j + 1)); // The address is relative to rngTable (NOT the worksheet)
                rngCapacidad.Style.NumberFormat.Format = "#,##0.00";
                */

                ws.Columns(1, j + 1).AdjustToContents();


            }

            ws.SheetView.FreezeRows(3);
            ws.SheetView.FreezeColumns(2);

            string strPathReports = GetPathUploadReports();
            string strNamefile = "MedidasSocios_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
            string strFullPath = Server.MapPath(strPathReports) + strNamefile;

            wb.SaveAs(strFullPath);

            string strUrl = "Bajarresumengral.aspx?n=" + strNamefile;
            Response.Redirect(strUrl, true);

        }
        
    }
}