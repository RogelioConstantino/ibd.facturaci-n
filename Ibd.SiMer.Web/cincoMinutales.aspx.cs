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

namespace Ibd.SiMer.Web
{
    public partial class cincoMinutales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            puntosCargaNe clsNeP = new puntosCargaNe();
            DataTable dtG2;
            dtG2 = clsNeP.Consultar();

            //dtG.Rows.Add(0, "0", "-- TODOS --");
            DataSet ds1 = new DataSet();
            ds1.Tables.Add(dtG2.Copy());
            //ds.Tables[0].DefaultView.Sort = "Punto de Carga";
            ddl_puntos.DataSource = ds1;
            ddl_puntos.DataTextField = "Punto de Carga";
            ddl_puntos.DataValueField = "IdPuntoCarga";
            //ddl_puntos.Items.Add("--Todos--");
            ddl_puntos.DataBind();
            ddl_puntos.Items.Add(new ListItem("Todos", "0"));
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

            ArchivoCincominutalesEn ArchivoCincominutal = new ArchivoCincominutalesEn();
            ArchivoCincominutalNe archivoCincominutalNe = new ArchivoCincominutalNe();

            string strFecIni = fecIni.Value;
            string strFecFin = fecFin.Value;            
            int strPunto = int.Parse(ddl_puntos.SelectedValue.ToString());


            string filePath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(filePath);

         
            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {         
                IXLWorksheet workSheet = workBook.Worksheet(1);

                int iKey = 0;
                int iRenglones = 1;
                bool firstRow = true;

                foreach (IXLRow row in workSheet.Rows())
                {                    
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            ArchivoCincominutal.Archivo = FileUpload1.PostedFile.FileName;
                            ArchivoCincominutal.IdPuntoCarga = strPunto;
                            ArchivoCincominutal.sFechaArchivo =  strFecIni;
                            iKey  = archivoCincominutalNe.insertarHeaderArchivo(ArchivoCincominutal);
                        }
                        firstRow = false;
                    }
                    else
                    {
                        iRenglones++;

                        var cell1 = row.Cell(1); 
                        string value1 = cell1.GetValue<string>();                     
                        var cell2 = row.Cell(2);
                        string value2 = cell2.GetValue<string>();
                        ArchivoCincominutal.sFechaArchivo = value2;
                        var cell3 = row.Cell(3);
                        string value3 = cell3.GetValue<string>();
                        var cell4 = row.Cell(4);
                        string value4 = cell4.GetValue<string>();
                        var cell5 = row.Cell(5);
                        string value5 = cell5.GetValue<string>();
                        var cell6 = row.Cell(6);
                        string value6 = cell6.GetValue<string>();
                        
                        if (strFecIni == value2.ToString().Substring(0, 10)  )
                        {
                            if (value6 == "R")
                            {
                                CincoMinutalEn cincoMinutal = new CincoMinutalEn();
                                
                                cincoMinutal.rmu = value1;
                                cincoMinutal.fecha = value2;
                                cincoMinutal.kwhe = double.Parse( value3);
                                cincoMinutal.kwhr = double.Parse(value4);
                                cincoMinutal.kvarh = double.Parse(value5);
                                cincoMinutal.tipo = value6;
                                
                                ArchivoCincominutal.IdArchivoCincominutales = iKey;
                                
                                Boolean bResult = archivoCincominutalNe.actualizaCincominutal(cincoMinutal);
                            }
                        }

                    }
                    
                }


                ArchivoCincominutal.Archivo = FileUpload1.PostedFile.FileName;
                ArchivoCincominutal.IdPuntoCarga = strPunto;
                ArchivoCincominutal.sFechaArchivo = strFecIni;                
                ArchivoCincominutal.NoRegistros = iRenglones;
                ArchivoCincominutal.Mensaje = "filtrado por " + strFecIni + " " + ddl_puntos.Text ;
                ArchivoCincominutal.IdArchivoCincominutales = iKey;
                archivoCincominutalNe.actualizaHeaderArchivo(ArchivoCincominutal);


            }
        }


    }
}