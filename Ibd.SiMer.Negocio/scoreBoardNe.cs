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
   public  class scoreBoardNe
    {
        DataTable dtData;
        public DataTable ScoreBoardCargakvarh(string strAño, string strMes)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                             
                string query = string.Format("[usp_scoreBoardMesCargakvarh]");//***[IBD.Facturacion]..
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


        public DataTable ScoreBoardCargakwhe(string strAño, string strMes)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();

                string query = string.Format("[usp_scoreBoardMesCargakwhe]");//***[IBD.Facturacion]..
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


        public DataTable ScoreBoardNum(string strAño, string strMes)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_scoreBoardMesNum]");//***[IBD.Facturacion]..
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
        public StringBuilder CreateTableHTML(DataTable dtGeneralReport)
        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            int iCol = 0;

            try
            {

                html.Append(" <thead>");
                //Building the Header row.

                html.Append("<tr class='text-uppercase' style='font-size:14px !important;'>");
                html.Append("<th class='text-center'  style='text-align:center;' colspan='2'>");
                html.Append("Punto de Carga");
                html.Append("</th>");
                html.Append("<th class='text-center' style='text-align:center;' colspan='" + (dtGeneralReport.Columns.Count - 2) + "'>");
                html.Append("Días del mes");
                html.Append("</th>");
                html.Append("</tr>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if (iCol++ == 0 )
                        html.Append("<th class='text-uppercase'style='font-size:14px !important;'>");
                    else
                        html.Append("<th class='text-uppercase  text-center'style='font-size:12px !important;'>");

                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'  style=' width: 500px; ' > ");// style=' display: block;  overflow: auto; width: 500px; height: 100%; overflow-x: scroll;overflow-y:hidden; '
                //Building the Data rows.
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr style='line-height: 14px !importan; ' >");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    { 
                        
                        if (column.ColumnName == "Nombre" || column.ColumnName == "RMU"
                            || column.ColumnName == "Punto de carga"|| column.ColumnName == "RPU")
                        {
                            html.Append("<td class='text-center' style='font-size:14px !important;'>");
                            html.Append("" + Convert.ToString(row[column.ColumnName]) + "");
                        }
                        else{
                            html.Append("<td class='text-center' style='font-size:14px !important;'>");
                            if (row[column.ColumnName] != DBNull.Value)
                            {
                                if (double.Parse(row[column.ColumnName].ToString()) != 0)
                                {
                                    string sVal = Convert.ToDouble(row[column.ColumnName]).ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                                    sVal = sVal.Substring(0, sVal.IndexOf("."));
                                    //html.Append("<span class='blue-text text-darken-2 text-right' style='font-size:10px !important;'>" + sVal + "</span>");
                                    html.Append( sVal );
                                }
                                else                                
                                    html.Append("0.00");
                                //    html.Append("<span class='red-text text-right'style='font-size:10px !important;' >  </span>");
                            }
                            else {
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

        public StringBuilder CreateTableHTMLIcons(DataTable dtGeneralReport, string strAño, string strMes)
        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            int iCol = 0;

            string strRMU = string.Empty;

            try
            {

                html.Append(" <thead>");
                //Building the Header row.

                html.Append("<tr class='text-uppercase' style='font-size:14px !important;'>");
                    html.Append("<th class='text-center'  style='text-align:center;' colspan='2'>");
                        html.Append("Punto de Carga");
                    html.Append("</th>");
                    html.Append("<th class='text-center' style='text-align:center;' colspan='" + (dtGeneralReport.Columns.Count -2)  + "'>");
                        html.Append("Días del mes");
                    html.Append("</th>");
                html.Append("</tr>");

                html.Append("<tr class='text-uppercase' style='font-size:14px !important;'>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if (iCol++ == 0)
                        html.Append("<th >");
                    else
                        html.Append("<th class='text-center'>");

                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr style='line-height: 16px;'>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {
                        
                        if (column.ColumnName == "Nombre" || column.ColumnName == "RMU" 
                            || column.ColumnName == "Punto de carga" || column.ColumnName == "RPU")
                        {

                            if (column.ColumnName == "RMU") strRMU = Convert.ToString(row[column.ColumnName]);

                            html.Append("<td  class='text-left' style='line-height: 16px;font-size:14px !important;'>");
                            html.Append("<span style='font-size:14px !important; ' >" + Convert.ToString(row[column.ColumnName]) + "</span>");
                        }
                        else
                        {
                            
                            html.Append("<td  class='text-center' style='line-height: 14px;'> ");
                            if (row[column.ColumnName] != DBNull.Value)
                            {
                                if (double.Parse(row[column.ColumnName].ToString()) > 288)
                                {
                                    html.Append("<i class='tiny material-icons red-text' title='"+ row[column.ColumnName].ToString() + "' style='height:10px !important;' onclick='alert(''" + row[column.ColumnName].ToString() + "'');' >error</i>");
                                }
                                else if (double.Parse(row[column.ColumnName].ToString()) == 288)
                                {

                                    //if (tieneEstimados(strAño, strMes, column.ColumnName, strRMU) > 0)
                                    //{
                                    //    html.Append("<a href='#modal1'><i class='tiny material-icons  light-yellow-text text-darken-3 ' title='" + row[column.ColumnName].ToString() + "' onclick='alert(''" + row[column.ColumnName].ToString() + "'');' >done</i></a>");
                                    //}
                                    //else {
                                        html.Append("<a href='#modal1'><i class='tiny material-icons  light-green-text text-darken-3 ' title='" + row[column.ColumnName].ToString() + "' onclick='alert(''" + row[column.ColumnName].ToString() + "'');' >done</i></a>");
                                    //}
                                }
                                else if (double.Parse(row[column.ColumnName].ToString()) < 288)
                                {
                                    if ((288 -  double.Parse(row[column.ColumnName].ToString())- 288) == 12 )
                                    {
                                        html.Append("<a href='#modal1'><i class='tiny material-icons ' title='" + row[column.ColumnName].ToString() + "' onclick='aler('" + row[column.ColumnName].ToString() + "');' >warning</i></a>");
                                    }
                                    else {
                                        html.Append("<a href='#modal1'><i class='tiny material-icons yellow-text text-accent-4' title='" + row[column.ColumnName].ToString() + "' onclick='aler('" + row[column.ColumnName].ToString() + "');' >report_problem</i></a>");
                                    }                                    
                                }
                                else
                                {
                                    html.Append("<i class='tiny material-icons light-blue accent-4 ' onclick='aler('" + row[column.ColumnName].ToString() + "'); >warning</i>");
                                }                            
                            }
                            else
                            {
                                DateTime thisDay = DateTime.Today;                                
                                if (thisDay.Day> int.Parse (column.ColumnName.ToString() ))
                                    html.Append("<i class='tiny material-icons red-text text-accent-4' title='' onclick='aler('" + "Vacio" + "');' >report_problem</i>");
                                else
                                    html.Append("");
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


        public StringBuilder CreateTableHTMLNum(DataTable dtGeneralReport)
        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            int iCol = 0;

            try
            {

                html.Append(" <thead>");
                //Building the Header row.

                html.Append("<tr class='text-uppercase' style='font-size:14px !important;'>");
                html.Append("<th class='text-center'  style='text-align:center;' colspan='2'>");
                html.Append("Punto de Carga");
                html.Append("</th>");
                html.Append("<th class='text-center' style='text-align:center;' colspan='" + (dtGeneralReport.Columns.Count - 2) + "'>");
                html.Append("Días del mes");
                html.Append("</th>");
                html.Append("</tr>");

                html.Append("<tr class='text-uppercase' style='font-size:14px !important;'>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if (iCol++ == 0)
                        html.Append("<th >");
                    else
                        html.Append("<th class='text-center'>");

                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr style='line-height: 16px;'>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {

                        if (column.ColumnName == "Nombre" || column.ColumnName == "RMU"
                            || column.ColumnName == "Punto de carga" || column.ColumnName == "RPU")
                        {
                            html.Append("<td  class='text-left' style='line-height: 16px;'>");
                            html.Append("<span style='font-size:14px !important; ' >" + Convert.ToString(row[column.ColumnName]) + "</span>");
                        }
                        else
                        {
                            html.Append("<td  class='text-center' style='line-height: 14px;'> ");
                            if (row[column.ColumnName] != DBNull.Value)
                            {
                                if (double.Parse(row[column.ColumnName].ToString()) > 288)
                                {
                                    html.Append("<span class='red-text' >" + row[column.ColumnName].ToString() + "</span>");
                                }
                                else if (double.Parse(row[column.ColumnName].ToString()) == 288)
                                {
                                    html.Append("<span class=' light-green-text text-darken-3 ' >" + row[column.ColumnName].ToString() + "</span>");
                                }
                                else if (double.Parse(row[column.ColumnName].ToString()) < 288)
                                {
                                    if ((288 - double.Parse(row[column.ColumnName].ToString()) - 288) == 12)
                                    {
                                        html.Append("<span class='' >" + row[column.ColumnName].ToString() + "'</span>");
                                    }
                                    else
                                    {
                                        html.Append("<span class=' yellow-text text-accent-4' >" + row[column.ColumnName].ToString() + "</span>");
                                    }
                                }
                                else
                                {
                                    html.Append("</span>");
                                }
                            }
                            else
                            {
                                DateTime thisDay = DateTime.Today;
                                if (thisDay.Day > int.Parse(column.ColumnName.ToString()))
                                    html.Append("<span class='  red-text text-accent-4'>" + "X" + "</span>");
                                else
                                    html.Append("");
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

        public int tieneEstimados(string strAño, string strMes, string strDia, string strRMU)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_tieneEstimados]");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@intAnio", SqlDbType.Int);
                sqlParameters[0].Value = int.Parse(strAño);

                sqlParameters[1] = new SqlParameter("@intMes", SqlDbType.Int);
                sqlParameters[1].Value = int.Parse(strMes);

                sqlParameters[2] = new SqlParameter("@intDia", SqlDbType.Int);
                sqlParameters[2].Value = int.Parse(strDia);

                sqlParameters[3] = new SqlParameter("@strRMU", SqlDbType.VarChar);
                sqlParameters[3].Value = strRMU;

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
            return int.Parse(dtData.Rows[0][0].ToString()) ;
        }


    }
}
