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
    public class rptAnaliticaNe
    {
        DataTable dtData;
        DataSet dtSet;
        public DataTable rptAnalitica(int strAño, int strMes)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                //string query = string.Format("[usp_RptDetalleAnalitica]");
                string query = string.Format("[usp_RptDetalleAnalitica_Acum]");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@intAnio", SqlDbType.Int);
                sqlParameters[0].Value = strAño;

                sqlParameters[1] = new SqlParameter("@intMes", SqlDbType.Int);
                sqlParameters[1].Value = strMes;

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

        public DataSet rptFacturacionMercado()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_RptFacAcum]");
                SqlParameter[] sqlParameters = new SqlParameter[0]; 

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

        public DataTable rptAnaliticaAltamira(int strAño, int strMes)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_RptDetalleAnaliticaRefCFEAltamira]");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@intAnio", SqlDbType.Int);
                sqlParameters[0].Value = strAño;

                sqlParameters[1] = new SqlParameter("@intMes", SqlDbType.Int);
                sqlParameters[1].Value = strMes;

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

        public StringBuilder CreateTableHTML(DataTable dtGeneralReport)
        {
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append(" <thead>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {                    
                    if (column.ColumnName == "numeroMes")
                    {
                        html.Append("<th class='text-uppercase text -enter' style='display:none !important ;' >");                        
                    }
                    else {
                        html.Append("<th class='text-uppercase text -enter' >");                        
                    }
                    html.Append(column.ColumnName.Replace("_", " "));
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

                        if (column.ColumnName == "Punto de Carga"
                            || column.ColumnName == "RMU"
                            || column.ColumnName == "Año"
                            || column.ColumnName == "Mes"
                            || column.ColumnName == "numeroMes"
                            || column.ColumnName == "Zonas de Carga"
                            || column.ColumnName == "RPU")
                        {

                            if (column.ColumnName == "numeroMes")
                            {
                                
                                html.Append("<td class='text-center' style='font-size:14px !important; display:none !important; '>");
                                html.Append("" + Convert.ToString(row[column.ColumnName]).Replace("_", " ") + "");
                            }
                            else
                            {
                                html.Append("<td class='text-center' style='font-size:14px !important;'>");
                                html.Append("" + Convert.ToString(row[column.ColumnName]).Replace("_", " ") + "");
                            }

                            
                        }
                        else
                        {                                 
                            html.Append("<td class='text-right' style='font-size:14px !important; text-align:right; '>");
                            if (row[column.ColumnName] != DBNull.Value)
                            {
                                if (double.Parse(row[column.ColumnName].ToString()) != 0)
                                {
                                    string sVal = Convert.ToDouble(row[column.ColumnName]).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                                    //sVal = sVal.Substring(0, sVal.IndexOf("."));
                                    //html.Append("<span class='blue-text text-darken-2 text-right' style='font-size:10px !important;'>" + sVal + "</span>");
                                    html.Append(sVal);
                                }
                                else
                                    html.Append("0.00");
                                //    html.Append("<span class='red-text text-right'style='font-size:10px !important;' >  </span>");
                            }
                            else
                            {
                                html.Append("<span class='text-center ' style='font-size:14px !important;' >  &nbsp;&nbsp;  </span>");
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
    }
}
