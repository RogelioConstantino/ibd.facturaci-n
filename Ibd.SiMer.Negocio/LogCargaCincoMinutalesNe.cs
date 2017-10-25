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
    public class LogCargaCincoMinutalesNe
    {

        DataTable dtData;
        public DataTable get( string strPuntoCarga, string strFecIni, string strFecFin)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_consultaBitacoraCargasV1]"); //***[IBD.Facturacion]..
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@IdPuntoCarga", SqlDbType.NVarChar);
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
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    html.Append("<th class='text-uppercase'>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
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
                        //html.Append("<td>");
                        //html.Append(row[column.ColumnName]);
                        html.Append("<td>");

                        //if ((Convert.ToString(row[column.ColumnName]) == "1") || (Convert.ToString(row[column.ColumnName]) == "True"))
                        //{
                        //    html.Append("<span class='glyphicon glyphicon-ok text-success text-center' aria-hidden='true'></span>");
                        //}
                        //else if ((Convert.ToString(row[column.ColumnName]) == "0") || (Convert.ToString(row[column.ColumnName]) == "False"))
                        //{
                        //    html.Append("<span class='glyphicon glyphicon-remove text-danger text-center' aria-hidden='true'></span>");
                        //}
                        //else
                        //{
                            html.Append(row[column.ColumnName]);
                        //}

                        html.Append("</td>");
                        //html.Append("</td>");

                    }
                    //html.Append("<td><a href='#' class='btn btn-primary btn-xs' data-toggle='modal' data-target='#edit' contenteditable='false'><span class='glyphicon glyphicon-pencil'></span></a></td>");
                    //html.Append("<td><a href='#' class='btn btn-danger btn-xs' data-toggle='modal' data-target='#delete' contenteditable='false'><span class='glyphicon glyphicon-trash'></span></a></td>");
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
