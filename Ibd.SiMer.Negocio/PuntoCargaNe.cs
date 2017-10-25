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

using System.Drawing;
using GemBox.Spreadsheet;



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
        public List<PuntoCargaFac> ConsultarPrevio(string strAño, string strMes, string  Tipo)
        {
            var oPcDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
            var list = oPcDa.ConsultarPrevio(strAño, strMes,  Tipo);

            return list;
        }


        public List<PuntoCargaFac> ConsultarPrevioCFECalificados(string strAño, string strMes, string Tipo)
        {
            var oPcDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
            var list = oPcDa.ConsultarPrevioCFECalif(strAño, strMes, Tipo);

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
                
                ds = oclsRpt.prcFac_get5MinBitHorario(int.Parse(id), strAño, strMes1,"","1");
                
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


        public List<PuntoCargaFac> GenerarAnexos(string strIds, string strMes, string servidor, string strUsuario, string strMinutales)
        {
            System.Data.DataSet ds;
            var oPcDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
            var list = oPcDa.Consultar();

            var lista = new List<PuntoCargaFac>(); 
            var pcDa = new PuntoCargaNe();  
            var idsAnexos = strIds.Split(',');
            Int64 id;

            //object sync = new object();
            //Parallel.ForEach(idsAnexos, (idAnexo) =>
            //{

            int idAnexo = 0;
            for (int i = 0; i <= idsAnexos.Length; i++)
            {
                idAnexo = int.Parse(idsAnexos[i]);

                string strAño = strMes.Substring(0, 4);
                string strMes1 = strMes.Substring(4, 2);

                rptConcentradoNe oclsRpt = new rptConcentradoNe();

                ds = oclsRpt.prcFac_get5MinBitHorario(idAnexo, strAño, strMes1, strUsuario, strMinutales);

                System.Data.DataTable tablePuntocarga = ds.Tables[1];
                id = Int64.Parse( tablePuntocarga.Rows[0][0].ToString());

                string Archivo = "";
                if (strMinutales == "1")
                {
                    Archivo = ExporttoExcelClosedXMLMinutales(ds, int.Parse(strAño), int.Parse(strMes1), servidor);
                }
                else
                {
                    Archivo = ExporttoExcelClosedXML(ds, int.Parse(strAño), int.Parse(strMes1), servidor);
                }
                

                ds = null;
                
                var oPuntoCargaDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
                PuntoCargaFac pc = new PuntoCargaFac();
                pc.IdPuntoCarga = idAnexo;
                pc.Codigo= "xls";

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

                ds = oclsRpt.prcFac_get5MinBitHorarioAct(id) ;
                ds.Dispose();

                lista.Add(pc);
            }
            //});



            return lista;
        }


        public List<PuntoCargaFac> GenerarPDF(string strIds, string strMes, string servidor, string strUsuario)
        {
            System.Data.DataTable dt;
            var oPcDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
            var list = oPcDa.Consultar();

            var lista = new List<PuntoCargaFac>();
            var pcDa = new PuntoCargaNe();
            var idsAnexos = strIds.Split(',');
            Int64 id;

            //object sync = new object();
            //Parallel.ForEach(idsAnexos, (idAnexo) =>
            //{
            int idAnexo = 0;
            for (int i = 0; i <= idsAnexos.Length; i++)
            {
                idAnexo = int.Parse(idsAnexos[i]);

                string strAño = strMes.Substring(0, 4);
                string strMes1 = strMes.Substring(4, 2);

                rptConcentradoNe oclsRpt = new rptConcentradoNe();
                
                dt = oclsRpt.getContratoCarga(idAnexo, strAño, strMes1);

                //System.Data.DataTable tablePuntocarga = ds.Tables[1];
                //id = Int64.Parse(tablePuntocarga.Rows[0][0].ToString());

                string Archivo = ExporttoExcelToPDF(dt, idAnexo, int.Parse(strAño), int.Parse(strMes1), servidor);

                dt = oclsRpt.prcFac_Cierre(strAño, strMes1,idAnexo, Archivo, strUsuario);
                
                var oPuntoCargaDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
                PuntoCargaFac pc = new PuntoCargaFac();
                pc.IdPuntoCarga = idAnexo;
                pc.Codigo = "pdf";
                pc.Ruta = Archivo;// anexo;
                
                lista.Add(pc);
                //});
            }
            return lista;
        }

        
        public List<PuntoCargaFac> GenerarAnexosAltamira(string strIds, string strMes, string servidor, string strUsuario)
        {
            System.Data.DataSet ds;
            var oPcDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
            
            var lista = new List<PuntoCargaFac>();
            var pcDa = new PuntoCargaNe();
            var idsAnexos = strIds.Split(',');

            object sync = new object();
            //Parallel.ForEach(idsAnexos, (idAnexo) =>
            //{
            int idAnexo =0;
            for (int i = 0; i <= idsAnexos.Length; i++)
            {
                idAnexo = int.Parse( idsAnexos[i]);

                string strAño = strMes.Substring(0, 4);
                string strMes1 = strMes.Substring(4, 2);

                rptConcentradoNe oclsRpt = new rptConcentradoNe();

                ds = oclsRpt.prcFac_getProcesoFacturacionAltamira(idAnexo, strAño, strMes1, strUsuario);

                string Archivo = ExporttoExcelClosedXMLAltamira(ds, int.Parse(strAño), int.Parse(strMes1), servidor);

                var oPuntoCargaDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
                PuntoCargaFac pc = new PuntoCargaFac();
                pc.IdPuntoCarga = idAnexo;
                pc.Codigo = "xls";
                                
                var rutaPlantilla = servidor + Config.ObtenerValuePorkeyEnSeccion<string>("ConfiguracionAnexos", "Plantilla");
                var rutaArchivos = servidor + Config.ObtenerValuePorkeyEnSeccion<string>("ConfiguracionAnexos", "CarpetaAnexos");
                
                var anexo = "Anexo_Altamira_" + strMes + "_" + pc.Codigo + ".xlsx";
                pc.Ruta = Archivo;
                
                lista.Add(pc);
            }
            //});

            return lista;
        }

        public List<PuntoCargaFac> GenerarAnexoCFECalificados(string strIds, string strMes, string servidor, string strUsuario)
        {
            System.Data.DataSet ds;
            var oPcDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());

            var lista = new List<PuntoCargaFac>();
            var pcDa = new PuntoCargaNe();
            var idsAnexos = strIds.Split(',');

            object sync = new object();
            //Parallel.ForEach(idsAnexos, (idAnexo) =>
            //{
            int idAnexo = 0;
            for (int i = 0; i <= idsAnexos.Length; i++)
            {
                idAnexo = int.Parse(idsAnexos[i]);

                string strAño = strMes.Substring(0, 4);
                string strMes1 = strMes.Substring(4, 2);

                rptConcentradoNe oclsRpt = new rptConcentradoNe();

                ds = oclsRpt.prcFac_getProcesoFacturacionCFECalificados( strAño, strMes1, strUsuario);

                string Archivo = ExporttoExcelClosedXMLCFECalificados(ds, int.Parse(strAño), int.Parse(strMes1), servidor);

                var oPuntoCargaDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
                PuntoCargaFac pc = new PuntoCargaFac();
                pc.IdPuntoCarga = idAnexo;
                pc.Codigo = "xls";

                var rutaPlantilla = servidor + Config.ObtenerValuePorkeyEnSeccion<string>("ConfiguracionAnexos", "Plantilla");
                var rutaArchivos = servidor + Config.ObtenerValuePorkeyEnSeccion<string>("ConfiguracionAnexos", "CarpetaAnexos");

                var anexo = "Anexo_Altamira_" + strMes + "_" + pc.Codigo + ".xlsx";
                pc.Ruta = Archivo;

                lista.Add(pc);
            }
            //});

            return lista;
        }


        private string ExporttoExcelClosedXML(DataSet ds, int strAño, int strMes, string servidor)
        {
            long k = 0;
            long j = 4;

            string strPathReports="";
            string strFullPath="";
            string strNamefile="";
            string strContrato = "";

            string strNamefileTemplate = "";

            try
            {
                //System.Data.DataTable tableTarifa = ds.Tables[2];
                System.Data.DataTable tablePuntocarga = ds.Tables[1];
                //System.Data.DataTable tableCliente = ds.Tables[3];
                                                                    
                if (
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0,10) == "01/01/1900")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "1900/01/01")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "1900-01-01")
                    || 
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 14) == "01 / 01 / 1900")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString() == "01 / 01 / 1900 12:00:00 a.m.")
                    )
                {
                    //strNamefileTemplate = "C:\\Iberdrola\\prjs\\req\\Macro\\template_FacturaMercado_1.xlsx";                    
                    strNamefileTemplate = "C:\\Iberdrola\\prjs\\req\\Macro\\template_FacturaMercado_1_sinMin.xlsx"; 
                }
                else
                {
                    strNamefileTemplate = "C:\\Iberdrola\\prjs\\req\\Macro\\template_FacturaMercado.xlsx";                    
                }

                var workbook = new XLWorkbook(strNamefileTemplate, XLEventTracking.Disabled);
                      



                if (
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "01/01/1900")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "1900/01/01")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "1900-01-01")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 14) == "01 / 01 / 1900")
                    )
                {

                    //var ws = workbook.Worksheet("5MinBit");
                    var ws_Subtotales1 = workbook.Worksheet("Subtotales1");
                    System.Data.DataTable table = ds.Tables[0];
                    for (j = 0; j < table.Rows.Count; j++)
                    {                        
                        for (k = 0; k < table.Columns.Count; k++)
                        {
                            ws_Subtotales1.Cell(ColumnLetter(k) + (j +23)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[k].ToString().ToUpper() ?? string.Empty;
                        }
                    }

                    ws_Subtotales1.Cell("C1").Value = tablePuntocarga.Rows[0].ItemArray[0].ToString().ToUpper() ?? string.Empty;
                    

                    ws_Subtotales1.Cell("b1").Value = tablePuntocarga.Rows[0]["PorteoMaximo"].ToString() ?? string.Empty;
                    //|ws_Subtotales1.Cell("b2").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;
                    ws_Subtotales1.Cell("b3").Value = tablePuntocarga.Rows[0]["mesaño"].ToString() ?? string.Empty;
                    ws_Subtotales1.Cell("b4").Value = tablePuntocarga.Rows[0]["diasdelmes"].ToString() ?? string.Empty;
                    ws_Subtotales1.Cell("b5").Value = tablePuntocarga.Rows[0]["tarifa"].ToString() ?? string.Empty;
                    ws_Subtotales1.Cell("b6").Value = tablePuntocarga.Rows[0]["FRI"].ToString() ?? string.Empty;
                    ws_Subtotales1.Cell("b7").Value = tablePuntocarga.Rows[0]["FRB"].ToString() ?? string.Empty;

                    var ws_Datos = workbook.Worksheet("DATOS");
                                        
                    ws_Datos.Cell("b1").Value = tablePuntocarga.Rows[0]["mesaño"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b3").Value = tablePuntocarga.Rows[0]["cliente"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b4").Value = tablePuntocarga.Rows[0]["Direccion1"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b5").Value = tablePuntocarga.Rows[0]["Direccion2"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b6").Value = tablePuntocarga.Rows[0]["RFC"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b8").Value = tablePuntocarga.Rows[0]["Tarifa"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b9").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b27").Value = tablePuntocarga.Rows[0]["PrecioPunta"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b28").Value = tablePuntocarga.Rows[0]["PrecioIntermedio"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b29").Value = tablePuntocarga.Rows[0]["PrecioBase"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b30").Value = tablePuntocarga.Rows[0]["DemandaFacturable"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b31").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_B"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b32").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_I"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b33").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_P"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b34").Value = tablePuntocarga.Rows[0]["DescuentoDemanda"].ToString() ?? string.Empty;

                    var ws_anexo = workbook.Worksheet("Anexo");
                    ws_anexo.Cell("D126").Value = "'"+tablePuntocarga.Rows[0]["RMU"].ToString() ?? string.Empty;
                    ws_anexo.Cell("I126").Value = "'" + tablePuntocarga.Rows[0]["RPU"].ToString() ?? string.Empty;

                    ws_Subtotales1.Cell("b40").Value = tablePuntocarga.Rows[0]["fechacorte"].ToString() ?? string.Empty;

                    System.Data.DataTable Contrato = ds.Tables[4];
                    strContrato = Contrato.Rows[0]["Contrato"].ToString() ?? string.Empty;


                    System.Data.DataTable tableTotalesEnergia = ds.Tables[3];
                    System.Data.DataTable tableTotalesDemanda = ds.Tables[2];
                    for (int l = 0; l < 11; l++)
                    {
                        // Energia Totales
                        ws_Datos.Cell(ColumnLetter(5 + l) + (12)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[15].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (13)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[14].ToString().ToUpper() ?? string.Empty;

                        //energia CD
                        ws_Datos.Cell(ColumnLetter(5 + l) + (17)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[7].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (18)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[6].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (19)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[5].ToString().ToUpper() ?? string.Empty;
                        
                        //energia SD
                        ws_Datos.Cell(ColumnLetter(5 + l) + (23)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[11].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (24)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[10].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (25)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[9].ToString().ToUpper() ?? string.Empty;
                        
                        //demanda CD
                        ws_Datos.Cell(ColumnLetter(5 + l) + (29)).Value = tableTotalesDemanda.Rows[int.Parse(l.ToString())].ItemArray[8].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (30)).Value = tableTotalesDemanda.Rows[int.Parse(l.ToString())].ItemArray[7].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (31)).Value = tableTotalesDemanda.Rows[int.Parse(l.ToString())].ItemArray[6].ToString().ToUpper() ?? string.Empty;
                        
                        //demanda CD
                        ws_Datos.Cell(ColumnLetter(5 + l) + (35)).Value = tableTotalesDemanda.Rows[int.Parse(l.ToString())].ItemArray[13].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (36)).Value = tableTotalesDemanda.Rows[int.Parse(l.ToString())].ItemArray[12].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (37)).Value = tableTotalesDemanda.Rows[int.Parse(l.ToString())].ItemArray[11].ToString().ToUpper() ?? string.Empty;
                    }
                }
                else
                {
                    var ws_Subtotales2 = workbook.Worksheet("Subtotales2");
                    ws_Subtotales2.Cell("b1").Value = tablePuntocarga.Rows[0]["PorteoMaximo"].ToString() ?? string.Empty;                                                                                                                         
                    ws_Subtotales2.Cell("b3").Value = tablePuntocarga.Rows[0]["mesaño"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b4").Value = tablePuntocarga.Rows[0]["diasdelmes"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b5").Value = tablePuntocarga.Rows[0]["tarifa"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b6").Value = tablePuntocarga.Rows[0]["FRI"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b7").Value = tablePuntocarga.Rows[0]["FRB"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b8").Value = tablePuntocarga.Rows[0]["fechacorte"].ToString() ?? string.Empty;
                    
                    var ws_Datos2 = workbook.Worksheet("DATOS2");

                    ws_Datos2.Cell("b3").Value = tablePuntocarga.Rows[0]["cliente"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b4").Value = tablePuntocarga.Rows[0]["Direccion1"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b5").Value = tablePuntocarga.Rows[0]["Direccion2"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b6").Value = tablePuntocarga.Rows[0]["RFC"].ToString() ?? string.Empty;

                    ws_Datos2.Cell("b8").Value = tablePuntocarga.Rows[0]["Tarifa"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b9").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;

                    ws_Datos2.Cell("b27").Value = tablePuntocarga.Rows[0]["PrecioPunta"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b28").Value = tablePuntocarga.Rows[0]["PrecioIntermedio"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b29").Value = tablePuntocarga.Rows[0]["PrecioBase"].ToString() ?? string.Empty;                      
                    // ws_Datos2.Cell("b1").Value = tableTarifa.Rows[0]["PrecioSemipunta"].ToString() ?? string.Empty;                    
                    ws_Datos2.Cell("b30").Value = tablePuntocarga.Rows[0]["DemandaFacturable"].ToString() ?? string.Empty;
                    
                    ws_Datos2.Cell("b31").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_B"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b32").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_I"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b33").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_P"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b34").Value = tablePuntocarga.Rows[0]["DescuentoDemanda"].ToString() ?? string.Empty;
                    
                    var ws_anexo = workbook.Worksheet("Anexo2");
                    ws_anexo.Cell("D126").Value = tablePuntocarga.Rows[0]["RMU"].ToString() ?? string.Empty;  
                    ws_anexo.Cell("I126").Value = tablePuntocarga.Rows[0]["RPU"].ToString() ?? string.Empty;

                    ws_Subtotales2.Cell("b40").Value = tablePuntocarga.Rows[0]["fechacorte"].ToString() ?? string.Empty;
                }                              

                strPathReports = GetPathUploadReports();                
                strNamefile = "Anexo_Mercado_" + strAño.ToString() + strMes.ToString() + "_" + tablePuntocarga.Rows[0]["Codigo"].ToString().Replace(" ","" ) + "_" + tablePuntocarga.Rows[0]["Intentos"].ToString() +  ".xlsx";
                strNamefile = strContrato;

                strFullPath = servidor + "" + "reportes\\" + strNamefile;
                //workbook.SaveAs(strFullPath);

                strNamefile = "Anexo_Mercado_" + strAño.ToString() + strMes.ToString() + "_" + tablePuntocarga.Rows[0]["Codigo"].ToString().Replace(" ", "") + ".xlsx";
                strNamefile = strContrato + ".xlsx";

                strFullPath = servidor + "" + "reportes\\Anexos\\Mercado\\" + strAño + "\\" +  strMes + "\\" + strNamefile;
                workbook.SaveAs(strFullPath);

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                return "AnexoMercado/reportes/" + strAño + "\\" + strMes + "\\" + strNamefile;
                
            }
            catch (Exception ex)
            {                
            }
            finally {                
            }
            return "AnexoMercado/reportes/" + strNamefile;
        }


        private string ExporttoExcelClosedXMLMinutales(DataSet ds, int strAño, int strMes, string servidor)
        {
            long k = 0;
            long j = 4;

            string strPathReports = "";
            string strFullPath = "";
            string strNamefile = "";
            string strContrato = "";

            string strNamefileTemplate = "";

            try
            {
                //System.Data.DataTable tableTarifa = ds.Tables[2];
                System.Data.DataTable tablePuntocarga = ds.Tables[1];
                //System.Data.DataTable tableCliente = ds.Tables[3];

                if (
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "01/01/1900")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "1900/01/01")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "1900-01-01")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 14) == "01 / 01 / 1900")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString() == "01 / 01 / 1900 12:00:00 a.m.")
                    )
                {
                    strNamefileTemplate = "C:\\Iberdrola\\prjs\\req\\Macro\\template_FacturaMercado_1.xlsx";                    
                    //strNamefileTemplate = "C:\\Iberdrola\\prjs\\req\\Macro\\template_FacturaMercado_1_sinMin.xlsx";
                }
                else
                {
                    strNamefileTemplate = "C:\\Iberdrola\\prjs\\req\\Macro\\template_FacturaMercado.xlsx";
                }

                var workbook = new XLWorkbook(strNamefileTemplate, XLEventTracking.Disabled);

                var ws = workbook.Worksheet("5MinBit");

                System.Data.DataTable table = ds.Tables[0];
                for (j = 0; j < table.Rows.Count; j++)
                {
                    for (k = 0; k < table.Columns.Count; k++)
                    {
                        ws.Cell(ColumnLetter(k) + (j + 4)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[k].ToString().ToUpper() ?? string.Empty;
                    }
                }
                ws.Cell("C1").Value = tablePuntocarga.Rows[0].ItemArray[0].ToString().ToUpper() ?? string.Empty;




                if (
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "01/01/1900")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "1900/01/01")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "1900-01-01")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 14) == "01 / 01 / 1900")
                    )
                {

                    //var ws = workbook.Worksheet("5MinBit");
                    var ws_Subtotales1 = workbook.Worksheet("Subtotales1");
                    //System.Data.DataTable table = ds.Tables[0];
                    //for (j = 0; j < table.Rows.Count; j++)
                    //{
                    //    for (k = 0; k < table.Columns.Count; k++)
                    //    {
                    //        ws_Subtotales1.Cell(ColumnLetter(k) + (j + 23)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[k].ToString().ToUpper() ?? string.Empty;
                    //    }
                    //}

                    //ws_Subtotales1.Cell("C1").Value = tablePuntocarga.Rows[0].ItemArray[0].ToString().ToUpper() ?? string.Empty;


                    ws_Subtotales1.Cell("b1").Value = tablePuntocarga.Rows[0]["PorteoMaximo"].ToString() ?? string.Empty;
                    //|ws_Subtotales1.Cell("b2").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;
                    ws_Subtotales1.Cell("b3").Value = tablePuntocarga.Rows[0]["mesaño"].ToString() ?? string.Empty;
                    ws_Subtotales1.Cell("b4").Value = tablePuntocarga.Rows[0]["diasdelmes"].ToString() ?? string.Empty;
                    ws_Subtotales1.Cell("b5").Value = tablePuntocarga.Rows[0]["tarifa"].ToString() ?? string.Empty;
                    ws_Subtotales1.Cell("b6").Value = tablePuntocarga.Rows[0]["FRI"].ToString() ?? string.Empty;
                    ws_Subtotales1.Cell("b7").Value = tablePuntocarga.Rows[0]["FRB"].ToString() ?? string.Empty;

                    var ws_Datos = workbook.Worksheet("DATOS");

                    ws_Datos.Cell("b1").Value = tablePuntocarga.Rows[0]["mesaño"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b3").Value = tablePuntocarga.Rows[0]["cliente"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b4").Value = tablePuntocarga.Rows[0]["Direccion1"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b5").Value = tablePuntocarga.Rows[0]["Direccion2"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b6").Value = tablePuntocarga.Rows[0]["RFC"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b8").Value = tablePuntocarga.Rows[0]["Tarifa"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b9").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b27").Value = tablePuntocarga.Rows[0]["PrecioPunta"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b28").Value = tablePuntocarga.Rows[0]["PrecioIntermedio"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b29").Value = tablePuntocarga.Rows[0]["PrecioBase"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b30").Value = tablePuntocarga.Rows[0]["DemandaFacturable"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b31").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_B"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b32").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_I"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b33").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_P"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b34").Value = tablePuntocarga.Rows[0]["DescuentoDemanda"].ToString() ?? string.Empty;

                    var ws_anexo = workbook.Worksheet("Anexo");
                    ws_anexo.Cell("D126").Value = "'" + tablePuntocarga.Rows[0]["RMU"].ToString() ?? string.Empty;
                    ws_anexo.Cell("I126").Value = "'" + tablePuntocarga.Rows[0]["RPU"].ToString() ?? string.Empty;

                    ws_Subtotales1.Cell("b40").Value = tablePuntocarga.Rows[0]["fechacorte"].ToString() ?? string.Empty;

                    System.Data.DataTable Contrato = ds.Tables[4];
                    strContrato = Contrato.Rows[0]["Contrato"].ToString() ?? string.Empty;


                    System.Data.DataTable tableTotalesEnergia = ds.Tables[3];
                    System.Data.DataTable tableTotalesDemanda = ds.Tables[2];
                    for (int l = 0; l < 11; l++)
                    {
                        // Energia Totales
                        ws_Datos.Cell(ColumnLetter(5 + l) + (12)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[15].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (13)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[14].ToString().ToUpper() ?? string.Empty;

                        //energia CD
                        ws_Datos.Cell(ColumnLetter(5 + l) + (17)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[7].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (18)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[6].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (19)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[5].ToString().ToUpper() ?? string.Empty;

                        //energia SD
                        ws_Datos.Cell(ColumnLetter(5 + l) + (23)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[11].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (24)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[10].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (25)).Value = tableTotalesEnergia.Rows[int.Parse(l.ToString())].ItemArray[9].ToString().ToUpper() ?? string.Empty;

                        //demanda CD
                        ws_Datos.Cell(ColumnLetter(5 + l) + (29)).Value = tableTotalesDemanda.Rows[int.Parse(l.ToString())].ItemArray[8].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (30)).Value = tableTotalesDemanda.Rows[int.Parse(l.ToString())].ItemArray[7].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (31)).Value = tableTotalesDemanda.Rows[int.Parse(l.ToString())].ItemArray[6].ToString().ToUpper() ?? string.Empty;

                        //demanda CD
                        ws_Datos.Cell(ColumnLetter(5 + l) + (35)).Value = tableTotalesDemanda.Rows[int.Parse(l.ToString())].ItemArray[13].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (36)).Value = tableTotalesDemanda.Rows[int.Parse(l.ToString())].ItemArray[12].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (37)).Value = tableTotalesDemanda.Rows[int.Parse(l.ToString())].ItemArray[11].ToString().ToUpper() ?? string.Empty;
                    }
                }
                else
                {
                    var ws_Subtotales2 = workbook.Worksheet("Subtotales2");
                    ws_Subtotales2.Cell("b1").Value = tablePuntocarga.Rows[0]["PorteoMaximo"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b3").Value = tablePuntocarga.Rows[0]["mesaño"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b4").Value = tablePuntocarga.Rows[0]["diasdelmes"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b5").Value = tablePuntocarga.Rows[0]["tarifa"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b6").Value = tablePuntocarga.Rows[0]["FRI"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b7").Value = tablePuntocarga.Rows[0]["FRB"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b8").Value = tablePuntocarga.Rows[0]["fechacorte"].ToString() ?? string.Empty;

                    var ws_Datos2 = workbook.Worksheet("DATOS2");

                    ws_Datos2.Cell("b3").Value = tablePuntocarga.Rows[0]["cliente"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b4").Value = tablePuntocarga.Rows[0]["Direccion1"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b5").Value = tablePuntocarga.Rows[0]["Direccion2"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b6").Value = tablePuntocarga.Rows[0]["RFC"].ToString() ?? string.Empty;

                    ws_Datos2.Cell("b8").Value = tablePuntocarga.Rows[0]["Tarifa"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b9").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;

                    ws_Datos2.Cell("b27").Value = tablePuntocarga.Rows[0]["PrecioPunta"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b28").Value = tablePuntocarga.Rows[0]["PrecioIntermedio"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b29").Value = tablePuntocarga.Rows[0]["PrecioBase"].ToString() ?? string.Empty;
                    // ws_Datos2.Cell("b1").Value = tableTarifa.Rows[0]["PrecioSemipunta"].ToString() ?? string.Empty;                    
                    ws_Datos2.Cell("b30").Value = tablePuntocarga.Rows[0]["DemandaFacturable"].ToString() ?? string.Empty;

                    ws_Datos2.Cell("b31").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_B"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b32").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_I"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b33").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_P"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b34").Value = tablePuntocarga.Rows[0]["DescuentoDemanda"].ToString() ?? string.Empty;

                    var ws_anexo = workbook.Worksheet("Anexo2");
                    ws_anexo.Cell("D126").Value = tablePuntocarga.Rows[0]["RMU"].ToString() ?? string.Empty;
                    ws_anexo.Cell("I126").Value = tablePuntocarga.Rows[0]["RPU"].ToString() ?? string.Empty;

                    ws_Subtotales2.Cell("b40").Value = tablePuntocarga.Rows[0]["fechacorte"].ToString() ?? string.Empty;
                }

                strPathReports = GetPathUploadReports();
                strNamefile = "Anexo_Mercado_" + strAño.ToString() + strMes.ToString() + "_" + tablePuntocarga.Rows[0]["Codigo"].ToString().Replace(" ", "") + "_" + tablePuntocarga.Rows[0]["Intentos"].ToString() + ".xlsx";
                strNamefile = strContrato;

                strFullPath = servidor + "" + "reportes\\" + strNamefile;
                //workbook.SaveAs(strFullPath);

                strNamefile = "Anexo_Mercado_" + strAño.ToString() + strMes.ToString() + "_" + tablePuntocarga.Rows[0]["Codigo"].ToString().Replace(" ", "") + ".xlsx";
                strNamefile = strContrato + ".xlsx";

                //strFullPath = servidor + "" + "reportes\\" + strNamefile;
                strFullPath = servidor + "" + "reportes\\Anexos\\Mercado\\" + strAño + "\\" + strMes + "\\" + strNamefile;
                workbook.SaveAs(strFullPath);

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                //return "AnexoMercado/reportes/" + strNamefile;
                return "AnexoMercado/reportes/" + strAño + "\\" + strMes + "\\" + strNamefile;
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return "AnexoMercado/reportes/" + strNamefile;
        }



        private string ExporttoExcelClosedXMLAltamira(DataSet ds, int strAño, int strMes, string servidor)
        {
            long k = 0;
            long j = 4;

            string strPathReports = "";
            string strFullPath = "";
            string strNamefile = "";

            string strNamefileTemplate = "";

            try
            {
                //System.Data.DataTable tableTarifa = ds.Tables[2];
                System.Data.DataTable tablePuntocarga = ds.Tables[0];
                //System.Data.DataTable tableCliente = ds.Tables[3];

                if (
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "01/01/1900")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "1900/01/01")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "1900-01-01")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 14) == "01 / 01 / 1900")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString() == "01 / 01 / 1900 12:00:00 a.m.")
                    )
                {
                    strNamefileTemplate = "C:\\Iberdrola\\prjs\\req\\Macro\\plantilla_AnexoFac_Altamira.xlsx";
                    //strNamefileTemplate = "C:\\Iberdrola\\prjs\\req\\Macro\\plantilla_AnexoFac_Ramos.xlsx";
                }
                else
                {
                    strNamefileTemplate = "C:\\Iberdrola\\prjs\\req\\Macro\\plantilla_AnexoFac_Altamira.xlsx";
                }

                var workbook = new XLWorkbook(strNamefileTemplate, XLEventTracking.Disabled);


                if (
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "01/01/1900")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "1900/01/01")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 10) == "1900-01-01")
                    ||
                    (tablePuntocarga.Rows[0]["fechacorte"].ToString().Substring(0, 14) == "01 / 01 / 1900")
                    )
                {
                    //var ws_Subtotales1 = workbook.Worksheet("Subtotales1");
                    //ws_Subtotales1.Cell("b1").Value = tablePuntocarga.Rows[0]["PorteoMaximo"].ToString() ?? string.Empty;
                    ////ws_Subtotales1.Cell("b2").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;
                    //ws_Subtotales1.Cell("b3").Value = tablePuntocarga.Rows[0]["mesaño"].ToString() ?? string.Empty;
                    //ws_Subtotales1.Cell("b4").Value = tablePuntocarga.Rows[0]["diasdelmes"].ToString() ?? string.Empty;
                    //ws_Subtotales1.Cell("b5").Value = tablePuntocarga.Rows[0]["tarifa"].ToString() ?? string.Empty;
                    //ws_Subtotales1.Cell("b6").Value = tablePuntocarga.Rows[0]["FRI"].ToString() ?? string.Empty;
                    //ws_Subtotales1.Cell("b7").Value = tablePuntocarga.Rows[0]["FRB"].ToString() ?? string.Empty;

                    var ws_Datos = workbook.Worksheet("Datos");

                    ws_Datos.Cell("b1").Value = tablePuntocarga.Rows[0]["mesaño"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b3").Value = tablePuntocarga.Rows[0]["cliente"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b4").Value = tablePuntocarga.Rows[0]["Direccion1"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b5").Value = tablePuntocarga.Rows[0]["Direccion2"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b6").Value = tablePuntocarga.Rows[0]["RFC"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b8").Value = tablePuntocarga.Rows[0]["Tarifa"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b9").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;
                    
                    ws_Datos.Cell("b39").Value = tablePuntocarga.Rows[0]["PrecioPunta"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b40").Value = tablePuntocarga.Rows[0]["PrecioIntermedio"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b41").Value = tablePuntocarga.Rows[0]["PrecioBase"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b42").Value = tablePuntocarga.Rows[0]["DemandaFacturable"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b45").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_B"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b46").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_I"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b47").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_P"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b48").Value = tablePuntocarga.Rows[0]["DescuentoDemanda"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b59").Value = tablePuntocarga.Rows[0]["FRI"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b60").Value = tablePuntocarga.Rows[0]["FRB"].ToString() ?? string.Empty;

                    ws_Datos.Cell("b43").Value = tablePuntocarga.Rows[0]["CVPm"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b44").Value = "'"+ tablePuntocarga.Rows[0]["CFPm"].ToString() ?? string.Empty;

                    ws_Datos.Cell("B19").Value =  tablePuntocarga.Rows[0]["FactorPotencia"].ToString() ?? string.Empty;


                    var ws_anexo = workbook.Worksheet("Anexo Cargos");
                    ws_anexo.Cell("D133").Value = "'" + tablePuntocarga.Rows[0]["RMU"].ToString() ?? string.Empty;
                    ws_anexo.Cell("I133").Value = "'" + tablePuntocarga.Rows[0]["RPU"].ToString() ?? string.Empty;

                    

                    //ws_Subtotales1.Cell("b40").Value = tablePuntocarga.Rows[0]["fechacorte"].ToString() ?? string.Empty;

                    System.Data.DataTable tableTotalesSocios = ds.Tables[1];
                    
                    ws_Datos.Cell("b10").Value = tableTotalesSocios.Rows[0]["KWH_Punta"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b11").Value = tableTotalesSocios.Rows[0]["KWH_Intermedia"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b12").Value = tableTotalesSocios.Rows[0]["KWH_Base"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b16").Value = tableTotalesSocios.Rows[0]["KW_Punta"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b17").Value = tableTotalesSocios.Rows[0]["KW_Intermedia"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b18").Value = tableTotalesSocios.Rows[0]["KW_Base"].ToString() ?? string.Empty;
                    //ws_Datos.Cell("b19").Value = tableTotalesSocios.Rows[0]["FP"].ToString() ?? string.Empty;
//                    ws_Datos.Cell("b20").Value = tableTotalesSocios.Rows[0]["Facturable"].ToString() ?? string.Empty;
                    //ws_Datos.Cell("b21").Value = tableTotalesSocios.Rows[0]["Facturable"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b22").Value = tableTotalesSocios.Rows[0]["hrs_Punta"].ToString() ?? string.Empty;

                    System.Data.DataTable tableTotalesCFE = ds.Tables[2];

                    ws_Datos.Cell("b23").Value = tableTotalesCFE.Rows[0]["KWH_Punta"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b24").Value = tableTotalesCFE.Rows[0]["KWH_Intermedia"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b25").Value = tableTotalesCFE.Rows[0]["KWH_Base"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b28").Value = tableTotalesCFE.Rows[0]["KW_Punta"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b29").Value = tableTotalesCFE.Rows[0]["KW_Intermedia"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b30").Value = tableTotalesCFE.Rows[0]["KW_Base"].ToString() ?? string.Empty;
                    //ws_Datos.Cell("b31").Value = tableTotalesCFE.Rows[0]["Facturable"].ToString() ?? string.Empty;
                    //ws_Datos.Cell("b32").Value = tableTotalesCFE.Rows[0]["Facturable"].ToString() ?? string.Empty;
                    //ws_Datos.Cell("b33").Value = tableTotalesCFE.Rows[0]["FacBon"].ToString() ?? string.Empty;

                    System.Data.DataTable tableTotalesCentral = ds.Tables[3];
                    ws_Datos.Cell("b34").Value = tableTotalesCentral.Rows[0]["KWH_Punta"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b35").Value = tableTotalesCentral.Rows[0]["KWH_Intermedia"].ToString() ?? string.Empty;
                    ws_Datos.Cell("b36").Value = tableTotalesCentral.Rows[0]["KWH_Base"].ToString() ?? string.Empty;

                    //var ws2 = workbook.Worksheets.Add("Page Breaks");
                    /*ws_anexo.PageSetup.PrintAreas.Add("A1:N132");
                    ws_anexo.PageSetup.AddHorizontalPageBreak(11);
                    ws_anexo.PageSetup.AddVerticalPageBreak(2);
                    */

                      System.Data.DataTable tableTotalesMes = ds.Tables[4];

                    //System.Data.DataTable tableTotalesDemanda = ds.Tables[3];
                    for (int l = 5; l < 9; l++)
                    {
                        ws_Datos.Cell(ColumnLetter(5 + l) + (18)).Value = tableTotalesMes.Rows[int.Parse((l).ToString())].ItemArray[0].ToString().ToUpper() ?? string.Empty;

                        //Totales energia 
                        ws_Datos.Cell(ColumnLetter(5 + l) + (21)).Value = tableTotalesMes.Rows[int.Parse((l).ToString())].ItemArray[1].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (22)).Value = tableTotalesMes.Rows[int.Parse((l).ToString())].ItemArray[2].ToString().ToUpper() ?? string.Empty;                        
                        //energia central
                        ws_Datos.Cell(ColumnLetter(5 + l) + (27)).Value = tableTotalesMes.Rows[int.Parse((l).ToString())].ItemArray[3].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (28)).Value = tableTotalesMes.Rows[int.Parse(l.ToString())].ItemArray[4].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (29)).Value = tableTotalesMes.Rows[int.Parse(l.ToString())].ItemArray[5].ToString().ToUpper() ?? string.Empty;
                        //energia cfe
                        ws_Datos.Cell(ColumnLetter(5 + l) + (33)).Value = tableTotalesMes.Rows[int.Parse(l.ToString())].ItemArray[6].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (34)).Value = tableTotalesMes.Rows[int.Parse(l.ToString())].ItemArray[7].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (35)).Value = tableTotalesMes.Rows[int.Parse(l.ToString())].ItemArray[8].ToString().ToUpper() ?? string.Empty;
                        //demanda central
                        ws_Datos.Cell(ColumnLetter(5 + l) + (39)).Value = tableTotalesMes.Rows[int.Parse(l.ToString())].ItemArray[9].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (40)).Value = tableTotalesMes.Rows[int.Parse(l.ToString())].ItemArray[10].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (41)).Value = tableTotalesMes.Rows[int.Parse(l.ToString())].ItemArray[11].ToString().ToUpper() ?? string.Empty;
                        //demanda cfe
                        ws_Datos.Cell(ColumnLetter(5 + l) + (45)).Value = tableTotalesMes.Rows[int.Parse(l.ToString())].ItemArray[12].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (46)).Value = tableTotalesMes.Rows[int.Parse(l.ToString())].ItemArray[13].ToString().ToUpper() ?? string.Empty;
                        ws_Datos.Cell(ColumnLetter(5 + l) + (47)).Value = tableTotalesMes.Rows[int.Parse(l.ToString())].ItemArray[14].ToString().ToUpper() ?? string.Empty;
                    }

                }
                else
                {
                    var ws_Subtotales2 = workbook.Worksheet("Subtotales2");
                    ws_Subtotales2.Cell("b1").Value = tablePuntocarga.Rows[0]["PorteoMaximo"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b3").Value = tablePuntocarga.Rows[0]["mesaño"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b4").Value = tablePuntocarga.Rows[0]["diasdelmes"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b5").Value = tablePuntocarga.Rows[0]["tarifa"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b59").Value = tablePuntocarga.Rows[0]["FRI"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b60").Value = tablePuntocarga.Rows[0]["FRB"].ToString() ?? string.Empty;
                    ws_Subtotales2.Cell("b8").Value = tablePuntocarga.Rows[0]["fechacorte"].ToString() ?? string.Empty;

                    var ws_Datos2 = workbook.Worksheet("DATOS2");

                    ws_Datos2.Cell("b3").Value = tablePuntocarga.Rows[0]["cliente"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b4").Value = tablePuntocarga.Rows[0]["Direccion1"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b5").Value = tablePuntocarga.Rows[0]["Direccion2"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b6").Value = tablePuntocarga.Rows[0]["RFC"].ToString() ?? string.Empty;

                    ws_Datos2.Cell("b8").Value = tablePuntocarga.Rows[0]["Tarifa"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b9").Value = tablePuntocarga.Rows[0]["DemandaContratada"].ToString() ?? string.Empty;

                    ws_Datos2.Cell("b27").Value = tablePuntocarga.Rows[0]["PrecioPunta"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b28").Value = tablePuntocarga.Rows[0]["PrecioIntermedio"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b29").Value = tablePuntocarga.Rows[0]["PrecioBase"].ToString() ?? string.Empty;
                    // ws_Datos2.Cell("b1").Value = tableTarifa.Rows[0]["PrecioSemipunta"].ToString() ?? string.Empty;                    
                    ws_Datos2.Cell("b30").Value = tablePuntocarga.Rows[0]["DemandaFacturable"].ToString() ?? string.Empty;

                    ws_Datos2.Cell("b31").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_B"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b32").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_I"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b33").Value = tablePuntocarga.Rows[0]["DescuentoEnergia_P"].ToString() ?? string.Empty;
                    ws_Datos2.Cell("b34").Value = tablePuntocarga.Rows[0]["DescuentoDemanda"].ToString() ?? string.Empty;

                    var ws_anexo = workbook.Worksheet("Anexo2");
                    ws_anexo.Cell("D126").Value = "'" + tablePuntocarga.Rows[0]["RMU"].ToString() ?? string.Empty;
                    ws_anexo.Cell("I126").Value = "'" + tablePuntocarga.Rows[0]["RPU"].ToString() ?? string.Empty;

                    ws_Subtotales2.Cell("b40").Value = tablePuntocarga.Rows[0]["fechacorte"].ToString() ?? string.Empty;

                }

                workbook.CalculateMode = XLCalculateMode.Auto; // .CalculateFormula();
                

                strPathReports = GetPathUploadReports();
                strNamefile = "Anexo_Altamira_" + strAño.ToString() + strMes.ToString() + "_" + tablePuntocarga.Rows[0]["Codigo"].ToString().Replace(" ", "") + "_" + tablePuntocarga.Rows[0]["Intentos"].ToString() + ".xlsx";
                strFullPath = servidor + "" + "reportes\\Anexo\\LiquidacionPrivados\\" + strNamefile;

                workbook.SaveAs(strFullPath);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                var workbook2 = new XLWorkbook(strFullPath, XLEventTracking.Disabled);
                workbook2.Save();

                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                ExcelFile ef = ExcelFile.Load(strFullPath);

                ef.Save(servidor + "" + "reportes\\Anexo\\LiquidacionPrivados\\" + "Anexo_Altamira_" + strAño.ToString() + strMes.ToString() + "_" + tablePuntocarga.Rows[0]["Codigo"].ToString().Replace(" ", "") + "_" + tablePuntocarga.Rows[0]["Intentos"].ToString() +  ".pdf");
                //ef.Save(servidor + "" + "reportes\\" + "Anexo_Altamira_" + strAño.ToString() + strMes.ToString() + "_" + tablePuntocarga.Rows[0]["Codigo"].ToString().Replace(" ", "") + "_66" +  ".pdf"); ///Anexo_Altamira_20175_TRIARA_66


                return "AnexoMercado/reportes/" + strNamefile;
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return "AnexoMercado/reportes/" + strNamefile;
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


        private string ExporttoExcelToPDF( DataTable dt,  int id, int strAño, int strMes, string servidor)
        {
            string strPathReports = "";
            string strFullPath = "";
            string strNamefile = "";
            
            try
            {               
                strPathReports = GetPathUploadReports();
                                                                                                                    //Anexo_Mercado_20176_Acueducto_toPDF.xlsx
                strNamefile = dt.Rows[0]["Contrato"].ToString();
                strFullPath = servidor + "" + "reportes\\Anexos\\Mercado\\"+ strAño + '\\'+ strMes+"\\" + strNamefile + ".xlsx";               

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                ExcelFile ef1 = ExcelFile.Load(strFullPath);

                //ef1.Save(servidor + "" + "reportes\\" + "Anexo_Mercado_" + strAño.ToString() + strMes.ToString() + "_" + dt.Rows[0]["Código"].ToString().Replace(" ", "")  + ".pdf");
                ///----ef1.Save(servidor + "" + "reportes\\" + "Soriana_" +  dt.Rows[0]["Código"].ToString().Replace(" ", "") + "_ICL_Junio_2017.pdf");
                ef1.Save(servidor + "" + "reportes\\Anexos\\Mercado\\" + strAño + '\\' + strMes + "\\" + strNamefile + ".pdf");

                //Soriana_Bahía_ICL_Junio_2017

                //--return "" + "Soriana_" + dt.Rows[0]["Código"].ToString().Replace(" ", "")  + "_ICL_Junio_2017" + ".pdf"; //strAño.ToString() + strMes.ToString() + "_" + ".pdf";
                return strNamefile + ".pdf";
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return "AnexoMercado/reportes/" + strNamefile;
        }


        private string ExporttoExcelClosedXMLCFECalificados(DataSet ds, int strAño, int strMes, string servidor)
        {
            long k = 0;
            long j = 4;

            string strPathReports = "";
            string strFullPath = "";
            string strNamefile = "";

            string strNamefileTemplate = "";

            try
            {                
                System.Data.DataTable tablePuntocarga = ds.Tables[0];
               
                strNamefileTemplate = "C:\\Iberdrola\\prjs\\req\\Macro\\Plantilla_CFE_CALIFICADOS_IG.xlsx";

                var workbook = new XLWorkbook(strNamefileTemplate, XLEventTracking.Disabled);
                
                var ws = workbook.Worksheet("Datos");

                System.Data.DataTable table = ds.Tables[1];
                for (j = 0; j < table.Rows.Count; j++)
                {
                    //for (k = 0; k < table.Columns.Count; k++)
                    //{
                        ws.Cell("A" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[1].ToString().ToUpper() ?? string.Empty;// dia'
                        ws.Cell("B" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[2].ToString().ToUpper() ?? string.Empty;// hora
                        ws.Cell("C" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[3].ToString().ToUpper() ?? string.Empty;// tc
                        ws.Cell("D" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[4].ToString().ToUpper() ?? string.Empty;// sml
                               //E
                        ws.Cell("F" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[6].ToString().ToUpper() ?? string.Empty;
                               //G
                        ws.Cell("H" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[8].ToString().ToUpper() ?? string.Empty;
                        ws.Cell("I" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[9].ToString().ToUpper() ?? string.Empty;
                        ws.Cell("J" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[10].ToString().ToUpper() ?? string.Empty;
                               //K
                        ws.Cell("L" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[12].ToString().ToUpper() ?? string.Empty;
                               //m
                               //n
                        ws.Cell("O" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[14].ToString().ToUpper() ?? string.Empty;
                        ws.Cell("P" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[15].ToString().ToUpper() ?? string.Empty;

                        ws.Cell("Q" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[16].ToString().ToUpper() ?? string.Empty;
                        ws.Cell("R" + (j + 3)).Value = table.Rows[int.Parse(j.ToString())].ItemArray[17].ToString().ToUpper() ?? string.Empty;

                    //}
                    ws.Cell("B1").Value = table.Rows[int.Parse(j.ToString())].ItemArray[18].ToString().ToUpper();
                }                

                workbook.CalculateMode = XLCalculateMode.Auto; 

                strPathReports = GetPathUploadReports();
                strNamefile = "Anexo_CFECalificados_" + strAño.ToString() + strMes.ToString() +  ".xlsx";
                strFullPath = servidor + "" + "reportes\\Anexos\\CFECalificados\\" + strNamefile;

                workbook.SaveAs(strFullPath);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                var workbook2 = new XLWorkbook(strFullPath, XLEventTracking.Disabled);
                workbook2.Save();

                //SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                //ExcelFile ef = ExcelFile.Load(strFullPath);

                //ef.Save(servidor + "" + "reportes\\Anexos\\CFECalificados\\" + "Anexo_CFECalificados_" + strAño.ToString() + strMes.ToString() + "_" + tablePuntocarga.Rows[0]["Codigo"].ToString().Replace(" ", "") + "_" + tablePuntocarga.Rows[0]["Intentos"].ToString() + ".pdf");
                
                return "AnexoMercado/reportes/Anexos/CFECalificados/" + strNamefile;
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return "AnexoMercado/reportes/Anexos/CFECalificados/" + strNamefile;
        }




        public List<PuntoCargaFac> GenerarPDFCfeCalificados(string strIds, string strMes, string servidor, string strUsuario)
        {
            System.Data.DataTable dt;
            var oPcDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
            var list = oPcDa.Consultar();

            var lista = new List<PuntoCargaFac>();
            var pcDa = new PuntoCargaNe();
            var idsAnexos = strIds.Split(',');
            Int64 id;

            //object sync = new object();
            //Parallel.ForEach(idsAnexos, (idAnexo) =>
            //{
            int idAnexo = 0;
            for (int i = 0; i <= idsAnexos.Length; i++)
            {
                idAnexo = int.Parse(idsAnexos[i]);

                string strAño = strMes.Substring(0, 4);
                string strMes1 = strMes.Substring(4, 2);

                rptConcentradoNe oclsRpt = new rptConcentradoNe();

                dt = oclsRpt.getContratoCargaCFECalificados(idAnexo, strAño, strMes1);
                                
                                  
                string Archivo = ExporttoExcelToPDFCFECalificados(dt, idAnexo, int.Parse(strAño), int.Parse(strMes1), servidor);

                dt = oclsRpt.prcFac_Cierre(strAño, strMes1, idAnexo, Archivo, strUsuario);

                var oPuntoCargaDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
                PuntoCargaFac pc = new PuntoCargaFac();
                pc.IdPuntoCarga = idAnexo;
                pc.Codigo = "pdf";
                pc.Ruta = Archivo;// anexo;

                lista.Add(pc);
                //});
            }
            return lista;
        }

        private string ExporttoExcelToPDFCFECalificados(DataTable dt, int id, int strAño, int strMes, string servidor)
        {
            string strPathReports = "";
            string strFullPath = "";
            string strNamefile = "";

            try
            {
                strPathReports = GetPathUploadReports();
                //Anexo_Mercado_20176_Acueducto_toPDF.xlsx
                strNamefile = dt.Rows[0]["Contrato"].ToString();
                strFullPath = servidor + "" + "reportes\\Anexos\\CFECalificados\\Anexo_CFECalificados_" + strAño.ToString() + strMes.ToString() +  "_.xlsx";

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                ExcelFile ef1 = ExcelFile.Load(strFullPath);

                //ef1.Save(servidor + "" + "reportes\\" + "Anexo_Mercado_" + strAño.ToString() + strMes.ToString() + "_" + dt.Rows[0]["Código"].ToString().Replace(" ", "")  + ".pdf");
                ///----ef1.Save(servidor + "" + "reportes\\" + "Soriana_" +  dt.Rows[0]["Código"].ToString().Replace(" ", "") + "_ICL_Junio_2017.pdf");
                ef1.Save(servidor + "" + "reportes\\Anexos\\CFECalificados\\" + strNamefile + ".pdf");

                //Soriana_Bahía_ICL_Junio_2017

                //--return "" + "Soriana_" + dt.Rows[0]["Código"].ToString().Replace(" ", "")  + "_ICL_Junio_2017" + ".pdf"; //strAño.ToString() + strMes.ToString() + "_" + ".pdf";
                return strNamefile + ".pdf";
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            
            return "AnexoMercado/reportes/Anexos/CFECalificados" + strNamefile;
        }



    }
}