using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
//using Mercado.Class.ADO;
using System.Data.SqlClient;
using System.Text;
using Ibd.SiMer.Datos;

namespace Ibd.SiMer.Negocio
{
    public class rptConcentradoNe
    {

        DataTable dtData;
        DataSet dtSet;

        public DataTable GetGeneralReport(string strAño, string strMes)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_RptConglomerado]");//***[IBD.Facturacion]..
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@intAnio", SqlDbType.NVarChar);
                sqlParameters[0].Value = strAño;

                sqlParameters[1] = new SqlParameter("@intMes", SqlDbType.Int);
                sqlParameters[1].Value = int.Parse(strMes);
                                
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }
        public DataTable GetGeneralReport( string strFecIni, string strFecFin, string strPuntoCarga)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_RptConglomeradoPuntoCarga]"); //***[IBD.Facturacion]..
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@PuntoCarga", SqlDbType.BigInt);
                sqlParameters[0].Value = strPuntoCarga;

                sqlParameters[1] = new SqlParameter("@FechaInicio", SqlDbType.NVarChar);
                sqlParameters[1].Value = strFecIni;

                sqlParameters[2] = new SqlParameter("@FechaFin", SqlDbType.NVarChar);
                sqlParameters[2].Value = strFecFin;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }

        public StringBuilder CreateTableHTML(DataTable dtGeneralReport)
        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            try
            {

                html.Append(" <thead>");
                //Building the Header row.


                html.Append("<tr class='text-uppercase' style='font-size:14px !important;'>");
                html.Append("<th class='text-center'  style='text-align:center;' colspan='2'>");
                html.Append("");
                html.Append("</th>");
                html.Append("<th class='text-center' style='text-align:center;' colspan='" + (dtGeneralReport.Columns.Count - 2) + "'>");
                html.Append("Puntos de carga");
                html.Append("</th>");
                html.Append("</tr>");


                html.Append("<tr  style='font-size:11px !important;'>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if (column.ColumnName == "Fecha" || column.ColumnName == "Hora")
                    {
                        html.Append("<th class='text-uppercase' style=' background-color:white;  width:80px !importan;'>");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }
                    else
                    {
                        html.Append("<th class='text-uppercase'>");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }
                        
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {

                        if (column.ColumnName == "Fecha" || column.ColumnName == "Hora")
                        {
                            html.Append("<td class='text-center' style='font-size:10px !important; '>");
                            html.Append("" + Convert.ToString(row[column.ColumnName]) + "");
                        }
                        else
                        {
                            html.Append("<td  class='text-right' style='font-size:9px !important; text-align:right; '>");
                            if (row[column.ColumnName] != DBNull.Value)
                            {
                                if (double.Parse(row[column.ColumnName].ToString()) != 0)
                                {
                                    string sVal = Convert.ToDouble(row[column.ColumnName]).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                                      sVal =row[column.ColumnName].ToString();
                                    html.Append(sVal);
                                }
                                else
                                    html.Append("0.00");                                
                            }
                            else
                            {
                                html.Append("<span class='text-center ' style='font-size:10px !important;' >  &nbsp;&nbsp;  </span>");
                            }
                        }
                        html.Append("</td>");
                    }                    
                    html.Append("</tr>");
                }
                html.Append("</tbody>");

            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "CreateTableHTML";
                //clsError.LogWrite();
            }

            return html;
        }
        public DataSet prcFac_get5MinBitHorario(int iPuntoCarga, string strAño, string strMes, string strUsuario, string Minutales)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_prcFacturacionPuntoCarga]");
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@intIdpuntocarga", SqlDbType.BigInt);
                sqlParameters[0].Value = iPuntoCarga;

                sqlParameters[1] = new SqlParameter("@intAnio", SqlDbType.Int);
                sqlParameters[1].Value = int.Parse(strAño);

                sqlParameters[2] = new SqlParameter("@intMes", SqlDbType.Int);
                sqlParameters[2].Value = int.Parse(strMes);

                sqlParameters[3] = new SqlParameter("@sUsuario", SqlDbType.Int);
                sqlParameters[3].Value = int.Parse(strUsuario);

                sqlParameters[4] = new SqlParameter("@intMinutales", SqlDbType.Int);
                sqlParameters[4].Value = int.Parse(Minutales); 


                con.dbConnection();
                dtSet = con.executeStoreProcedureDS(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtSet;
        }

        public DataSet prcFac_get5MinBitHorarioAct(Int64 id)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_prcFacturacionPuntoCarga_Act]");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@intId", SqlDbType.BigInt);
                sqlParameters[0].Value = id;


                con.dbConnection();
                dtSet = con.executeStoreProcedureDS(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtSet;
        }

        public DataTable getCodifoPuntoCarga(int iPuntoCarga)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_obtienePuntosCarga]");
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@idGrupo", SqlDbType.Int);
                sqlParameters[0].Value = int.Parse("0");
                sqlParameters[1] = new SqlParameter("@idCliente", SqlDbType.Int);
                sqlParameters[1].Value = int.Parse("0");
                sqlParameters[2] = new SqlParameter("@idPuntoCarga", SqlDbType.Int);
                sqlParameters[2].Value = iPuntoCarga;

                sqlParameters[3] = new SqlParameter("@PuntoCarga", SqlDbType.NVarChar);
                sqlParameters[3].Value = "";
                sqlParameters[4] = new SqlParameter("@RMU", SqlDbType.NVarChar);
                sqlParameters[4].Value = "";
                sqlParameters[5] = new SqlParameter("@RPU", SqlDbType.NVarChar);
                sqlParameters[5].Value = "";

                //_baseDatos.CrearComando(sql, CommandType.StoredProcedure);

                //_baseDatos.AsignarParametroWhitValue("@idGrupo", 0);
                //_baseDatos.AsignarParametroWhitValue("@idCliente", 0);

                //_baseDatos.AsignarParametroWhitValue("@idPuntoCarga", 0);
                //_baseDatos.AsignarParametroWhitValue("@PuntoCarga", "");
                //_baseDatos.AsignarParametroWhitValue("@RMU", "");
                //_baseDatos.AsignarParametroWhitValue("@RP", "");

                //var datos = _baseDatos.TraerDataTable();
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }


        public DataTable getContratoCarga(int iPuntoCarga, string strAño, string strMes)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_obtieneContratoPuntosCarga]");
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@idPuntoCarga", SqlDbType.Int);
                sqlParameters[0].Value = iPuntoCarga;
                sqlParameters[1] = new SqlParameter("@intAnio", SqlDbType.Int);
                sqlParameters[1].Value = int.Parse(strAño);

                sqlParameters[2] = new SqlParameter("@intMes", SqlDbType.Int);
                sqlParameters[2].Value = int.Parse(strMes);


                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);


            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }

        public DataSet prcFac_getProcesoFacturacionAltamira(int iPuntoCarga,  string strAño, string strMes, string strUsuario)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_prcFacturacionAltamira]");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@intIdpuntocarga", SqlDbType.BigInt);
                sqlParameters[0].Value = iPuntoCarga;

                sqlParameters[1] = new SqlParameter("@intAnio", SqlDbType.Int);
                sqlParameters[1].Value = int.Parse(strAño);

                sqlParameters[2] = new SqlParameter("@intMes", SqlDbType.Int);
                sqlParameters[2].Value = int.Parse(strMes);

                sqlParameters[3] = new SqlParameter("@sUsuario", SqlDbType.Int);
                sqlParameters[3].Value = int.Parse(strUsuario);
                

                con.dbConnection();
                dtSet = con.executeStoreProcedureDS(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtSet;
        }
        public DataTable GetBITHorario(int iAño, int iMes, int iRegion, int iTarifa)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_getBitHorarioTarifa]"); //***[IBD.Facturacion]..
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@intAño", SqlDbType.Int);
                sqlParameters[0].Value = iAño;

                sqlParameters[1] = new SqlParameter("@intMes", SqlDbType.Int);
                sqlParameters[1].Value = iMes;

                sqlParameters[2] = new SqlParameter("@intIdRegion", SqlDbType.BigInt);
                sqlParameters[2].Value = iRegion;

                sqlParameters[3] = new SqlParameter("@intIdTarifa", SqlDbType.BigInt);
                sqlParameters[3].Value = iTarifa;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }

        public StringBuilder CreateTableHTMLBIT(DataTable dtGeneralReport)
        {
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append(" <thead>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    html.Append("<th class='text-uppercase text -enter' >");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }

                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {
                        html.Append("<td>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }
                html.Append("</tbody>");
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "CreateTableHTML";
                //clsError.LogWrite();
            }
            return html;
        }
        
        public DataTable prcFac_Cierre( string strAño, string strMes, int iPuntoCarga, string File, string strUsuario)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_prcFacturacionPuntoCarga_ActCierre]");
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@intAnio", SqlDbType.BigInt);
                sqlParameters[0].Value = strAño;

                sqlParameters[1] = new SqlParameter("@intMes", SqlDbType.BigInt);
                sqlParameters[1].Value = strMes;

                sqlParameters[2] = new SqlParameter("@intIdpuntocarga", SqlDbType.BigInt);
                sqlParameters[2].Value = iPuntoCarga;

                sqlParameters[3] = new SqlParameter("@File", SqlDbType.NVarChar);
                sqlParameters[3].Value = File;

                sqlParameters[4] = new SqlParameter("@sUsuario", SqlDbType.NVarChar);
                sqlParameters[4].Value = strUsuario;
                
                con.dbConnection();
                //dtSet = con.executeStoreProcedureDS(query, sqlParameters);
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();
            }
            return dtData;
        }


        public DataSet prcFac_getProcesoFacturacionCFECalificados( string strAño, string strMes, string strUsuario)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_procesaCFECalificados]");
                SqlParameter[] sqlParameters = new SqlParameter[3];                              

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = int.Parse(strAño);

                sqlParameters[1] = new SqlParameter("@Mes", SqlDbType.Int);
                sqlParameters[1].Value = int.Parse(strMes);

                sqlParameters[2] = new SqlParameter("@sUsuario", SqlDbType.Int);
                sqlParameters[2].Value = int.Parse(strUsuario);
                
                con.dbConnection();
                dtSet = con.executeStoreProcedureDS(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtSet;
        }

        public DataTable getContratoCargaCFECalificados(int iPuntoCarga, string strAño, string strMes)
        {
            try
            {
                ConnectionDB con = new ConnectionDB(); 
                string query = string.Format("[IBD.Facturacion]..[usp_obtieneContratoCFECalificados]");
                SqlParameter[] sqlParameters = new SqlParameter[2];
                 
                sqlParameters[0] = new SqlParameter("@intAnio", SqlDbType.Int);
                sqlParameters[0].Value = int.Parse(strAño);

                sqlParameters[1] = new SqlParameter("@intMes", SqlDbType.Int);
                sqlParameters[1].Value = int.Parse(strMes);

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }


        public DataTable prcFac_CierreCFECalificados(string strAño, string strMes, int iPuntoCarga, string File, string strUsuario)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_prcFacturacionCFECalif_ActCierre]");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@intAnio", SqlDbType.BigInt);
                sqlParameters[0].Value = strAño;

                sqlParameters[1] = new SqlParameter("@intMes", SqlDbType.BigInt);
                sqlParameters[1].Value = strMes;
                               
                sqlParameters[2] = new SqlParameter("@File", SqlDbType.NVarChar);
                sqlParameters[2].Value = File;

                sqlParameters[3] = new SqlParameter("@sUsuario", SqlDbType.NVarChar);
                sqlParameters[3].Value = strUsuario;

                con.dbConnection();
                //dtSet = con.executeStoreProcedureDS(query, sqlParameters);
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();
            }
            return dtData;
        }



    }
}
