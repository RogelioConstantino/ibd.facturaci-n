
using Ibd.Framework;
using Ibd.Framework.Archivos;
using Ibd.SiMer.Datos;
using Ibd.SiMer.Entidades;
using Ibd.SiMer.Negocio.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using System.Data;
using System.Configuration;
using System.Data.SqlClient;

using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;


namespace Ibd.SiMer.Negocio
{
    public class PuntoCargaNe
    {
        public List<PuntoCargaFac> Consultar()
        {
            var oPcDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
            var list = oPcDa.Consultar();

            return list;
        }
        public List<PuntoCargaFac> ConsultarPrevio()
        {
            var oPcDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
            var list = oPcDa.ConsultarPrevio();

            return list;
        }

        public List<PuntoCargaFac> ConsultarById(string Id)
        {
            var oPcDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
            var list = oPcDa.ConsultarById(Id);

            return list;
        }

        public List<PuntoCargaFac> GenerarAnexos1(string strIds, string strMes, string servidor)
        {
            System.Data.DataSet ds;
            var oPcDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
            var list = oPcDa.Consultar();

            var lista = new List<PuntoCargaFac>();
            var pcDa = new PuntoCargaNe();
            var idsAnexos = strIds.Split(',');
            
            foreach (string id in idsAnexos)
            {
                var oPuntoCargaDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
                var pc = new PuntoCargaFac();

                string strAño = strMes.Substring(0, 4);
                string strMes1 = strMes.Substring(4, 2);

                rptConcentradoNe oclsRpt = new rptConcentradoNe();
                
                ds = oclsRpt.prcFac_get5MinBitHorario(int.Parse(id), strAño, strMes1);

                

                string Archivo = ExporttoExcelClosedXML(ds, int.Parse(strAño), int.Parse(strMes1), servidor);

                pc.IdPuntoCarga = int.Parse(id);
                pc = oPuntoCargaDa.PuntoCargaConsultar(pc);
                pc.Ruta = Archivo;


                lista.Add(pc);
                
                
                
                // Creamos la copia del Anexo
                var rutaPlantilla = servidor+Config.ObtenerValuePorkeyEnSeccion<string>("ConfiguracionAnexos", "Plantilla");
                var rutaArchivos = servidor + Config.ObtenerValuePorkeyEnSeccion<string>("ConfiguracionAnexos", "CarpetaAnexos");
                var plantilla = new ArchivoExcel(rutaPlantilla);

                var anexo = "Anexo_Mercado_" + strMes + "_" + pc.Codigo.Replace(" ","_") + ".xlsx";

                //plantilla.CopiarA(rutaArchivos + anexo, true);
            }

            return lista;
        }


        public List<PuntoCargaFac> GenerarAnexos(string strIds, string strMes, string servidor)
        {
            System.Data.DataSet ds;
            var oPcDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
            var list = oPcDa.Consultar();

            var lista = new List<PuntoCargaFac>();
            var pcDa = new PuntoCargaNe();
            var idsAnexos = strIds.Split(',');


            object sync = new object();
            Parallel.ForEach(idsAnexos, (idAnexo) =>
            {


                string strAño = strMes.Substring(0, 4);
                string strMes1 = strMes.Substring(4, 2);

                rptConcentradoNe oclsRpt = new rptConcentradoNe();

                ds = oclsRpt.prcFac_get5MinBitHorario(int.Parse(idAnexo), strAño, strMes1);
                
                string Archivo = ExporttoExcelClosedXML(ds, int.Parse(strAño), int.Parse(strMes1), servidor);

                var oPuntoCargaDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
                PuntoCargaFac pc = new PuntoCargaFac();
                pc.IdPuntoCarga = int.Parse(idAnexo);
                pc.Codigo= "Descargar";

                //var pc = new PuntoCargaFac()
                //{
                //    IdPuntoCarga = Convert.ToInt32(idAnexo)
                //};

                //{
                //    lock (sync)
                //        pc = oPuntoCargaDa.PuntoCargaConsultar(pc);
                //}

                // Creamos la copia del Anexo
                var rutaPlantilla = servidor + Config.ObtenerValuePorkeyEnSeccion<string>("ConfiguracionAnexos", "Plantilla");
                var rutaArchivos = servidor + Config.ObtenerValuePorkeyEnSeccion<string>("ConfiguracionAnexos", "CarpetaAnexos");
//                var plantilla = new ArchivoExcel(rutaPlantilla);

                var anexo = "Anexo_Mercado_" + strMes + "_" + pc.Codigo+ ".xlsx";
                //pc.Ruta = Config.ObtenerValuePorkeyEnSeccion<string>("ConfiguracionAnexos", "CarpetaAnexos").Replace(@"\", "/") + anexo;
                pc.Ruta = Archivo;// anexo;
                //plantilla.CopiarA(rutaArchivos + anexo, true);

                lista.Add(pc);
            });



            return lista;
        }

        private string ExporttoExcelClosedXML(DataSet ds, int strAño, int strMes, string servidor)
        {
            long k = 0;
            long j = 4;

            string strPathReports="";
            string strFullPath="";
            string strNamefile="";

            try
            {
                var workbook = new XLWorkbook("C:\\Iberdrola\\prjs\\req\\Macro\\template_FacturaMercado.xlsx");
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

                System.Data.DataTable tableTarifa = ds.Tables[2];

                var ws_Subtotales1 = workbook.Worksheet("Subtotales1");
                ws_Subtotales1.Cell("b1").Value = tablePuntocarga.Rows[0]["PorteoMaximo"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                ws_Subtotales1.Cell("b2").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                ws_Subtotales1.Cell("b3").Value = tableTarifa.Rows[0]["mesaño"].ToString() ?? string.Empty;
                ws_Subtotales1.Cell("b4").Value = tableTarifa.Rows[0]["diasdelmes"].ToString() ?? string.Empty;
                ws_Subtotales1.Cell("b5").Value = tableTarifa.Rows[0]["tarifa"].ToString() ?? string.Empty;
                ws_Subtotales1.Cell("b6").Value = tableTarifa.Rows[0]["FRI"].ToString() ?? string.Empty;
                ws_Subtotales1.Cell("b7").Value = tableTarifa.Rows[0]["FRB"].ToString() ?? string.Empty;

                ws_Subtotales1.Cell("B1").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales1.Cell("B2").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales1.Cell("B3").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales1.Cell("B4").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales1.Cell("B5").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales1.Cell("B6").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales1.Cell("B7").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");


                var ws_Subtotales2 = workbook.Worksheet("Subtotales2");
                ws_Subtotales2.Cell("b1").Value = tablePuntocarga.Rows[0]["PorteoMaximo"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                                                                                                                       // ws_Subtotales2.Cell("b2").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                ws_Subtotales2.Cell("b3").Value = tableTarifa.Rows[0]["mesaño"].ToString() ?? string.Empty;
                ws_Subtotales2.Cell("b4").Value = tableTarifa.Rows[0]["diasdelmes"].ToString() ?? string.Empty;
                ws_Subtotales2.Cell("b5").Value = tableTarifa.Rows[0]["tarifa"].ToString() ?? string.Empty;
                ws_Subtotales2.Cell("b6").Value = tableTarifa.Rows[0]["FRI"].ToString() ?? string.Empty;
                ws_Subtotales2.Cell("b7").Value = tableTarifa.Rows[0]["FRB"].ToString() ?? string.Empty;
                ws_Subtotales2.Cell("b8").Value = tableTarifa.Rows[0]["fechacorte"].ToString() ?? string.Empty;

                ws_Subtotales2.Cell("B1").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales2.Cell("B2").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales2.Cell("B3").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales2.Cell("B4").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales2.Cell("B5").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales2.Cell("B6").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales2.Cell("B7").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Subtotales2.Cell("B8").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");



                var ws_Datos = workbook.Worksheet("DATOS");
                ws_Datos.Cell("b28").Value = tableTarifa.Rows[0]["PrecioBase"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                ws_Datos.Cell("b26").Value = tableTarifa.Rows[0]["PrecioIntermedio"].ToString() ?? string.Empty;
                //ws_Datos.Cell("b26").Value = tableTarifa.Rows[0]["PrecioSemipunta"].ToString() ?? string.Empty;
                ws_Datos.Cell("b27").Value = tableTarifa.Rows[0]["PrecioPunta"].ToString() ?? string.Empty;
                ws_Datos.Cell("b30").Value = tableTarifa.Rows[0]["DemandaFacturable"].ToString() ?? string.Empty;
                ws_Datos.Cell("b1").Value = tableTarifa.Rows[0]["Tarifa"].ToString() ?? string.Empty;

                ws_Datos.Cell("b30").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Datos.Cell("b29").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Datos.Cell("b28").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Datos.Cell("b27").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Datos.Cell("b1").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");


                ws_Datos.Cell("b31").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_B"].ToString() ?? string.Empty;
                ws_Datos.Cell("b32").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_I"].ToString() ?? string.Empty;
                ws_Datos.Cell("b33").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_P"].ToString() ?? string.Empty;
                ws_Datos.Cell("b34").Value = tablePuntocarga.Rows[0]["DescuentoDemanda"].ToString() ?? string.Empty;

                ws_Datos.Cell("b31").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Datos.Cell("b32").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Datos.Cell("b33").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");
                ws_Datos.Cell("b34").Style.Fill.BackgroundColor = XLColor.FromHtml("#558b2f");


                var ws_Datos2 = workbook.Worksheet("DATOS2");
                ws_Datos2.Cell("b29").Value = tableTarifa.Rows[0]["PrecioBase"].ToString() ?? string.Empty;  //ItemArray[0].ToString().ToUpper() ?? string.Empty;
                ws_Datos2.Cell("b28").Value = tableTarifa.Rows[0]["PrecioIntermedio"].ToString() ?? string.Empty;
               // ws_Datos2.Cell("b1").Value = tableTarifa.Rows[0]["PrecioSemipunta"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b27").Value = tableTarifa.Rows[0]["PrecioPunta"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b30").Value = tableTarifa.Rows[0]["DemandaFacturable"].ToString() ?? string.Empty;
                ws_Datos2.Cell("b8").Value = tableTarifa.Rows[0]["Tarifa"].ToString() ?? string.Empty;

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

                ws_Datos2.Cell("b9").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;



                strPathReports = GetPathUploadReports();
                //string strFullPath = "Anexo_Mercado_" + strAño.ToString() + strMes.ToString() + "_" + tablePuntocarga.Rows[0]["Codigo"].ToString() ?? string.Empty + ".xlsx";


                strNamefile = "Anexo_Mercado_" + strAño.ToString() + strMes.ToString() + "_" + tablePuntocarga.Rows[0]["Codigo"].ToString() + ".xlsx";
                strFullPath = servidor + "" + "reportes\\" + strNamefile;

                workbook.SaveAs(strFullPath);

                //string strUrl = "Bajarresumengral.aspx?n=" + strNamefile;
                //Response.Redirect(strUrl, true);
                //return strPathReports + strFullPath;
                return "AnexoMercado/reportes/" + strNamefile; ;

            }
            catch (Exception ex)
            {
                
            }
            finally {
                
            }
            return strPathReports + strFullPath;

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



    }
}