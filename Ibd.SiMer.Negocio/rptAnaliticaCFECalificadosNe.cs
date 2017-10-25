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
    public class rptAnaliticaCFECalificadosNe
    {
        DataTable dtData;
        DataSet dtSet;
        public DataSet rptAnalitica(int strAño, int strMes)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_procesaCFECalificados]");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = strAño;

                sqlParameters[1] = new SqlParameter("@Mes", SqlDbType.Int);
                sqlParameters[1].Value = strMes;

                con.dbConnection();
                dtSet = con.executeStoreProcedureDS(query, sqlParameters);
                //dtData = con.executeStoreProcedure(query, sqlParameters);
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


        public StringBuilder CreateTableHTML(DataTable dtGeneralReport)
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

                        if (column.ColumnName == "Punto de Carga"
                            || column.ColumnName == "RMU"
                            || column.ColumnName == "CFE"
                            || column.ColumnName == "Año"
                            || column.ColumnName == "Mes"
                            || column.ColumnName == "RPU")
                        {
                            html.Append("<td class='text-center' style='font-size:14px !important;'>");
                            html.Append("" + Convert.ToString(row[column.ColumnName]) + "");
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


        public StringBuilder CreateTableHTML2(DataTable dtGeneralReport)
        {
            StringBuilder html = new StringBuilder();

            string sParent = "";
            string sChild = "";
            int iRenglon = 0;
            int iParentRenglon = 0;
            try
            {
                html.Append(" <thead>");
                html.Append("<tr style='color: #5c881a; border-top: 1px solid #d0d0d0 !important;  text-align: center !important;'><th  style='text-align: center !important;'  colspan= '2' class='text-uppercase sorting_disabled'>	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>TC	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>SML	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>Precio Gas	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>CTUNG	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>Combustible	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>CVOM	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>Transmisión	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>CENACE	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>Precio Energía	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>PML JOV-230	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>TBFin	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>CFE Calificados</th>	<th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>Precio CFE Calificados</th>    <th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>Facturación Potencia </th>  <th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'> Facturación Energia MXP</th>   </tr>");
                html.Append("<tr style='color: #5c881a;' ><th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>Dia</th><th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>Hora</th>	<th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/USD	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>USD/MMBTU	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>USD/MMBTU	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MMBTU/MWh	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>USD/MWh	</th> <th style='text-align: center !important;'  class='text-uppercase text-center sorting_disabled'>USD/MWh	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/MWh	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/MWh	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/MWh	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/MWh		</th> <th  style='text-align: center !important;' class='text-uppercase text-enter sorting_disabled'>MXN/MWh	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MW	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/MWh</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/MWh</th>  </tr> ");
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");

                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    iRenglon++;
                    html.Append("<tr  data-id='" + iRenglon.ToString() + "' data-parent='" + iParentRenglon.ToString() + "'>"); //(iParentRenglon== 0?"": iParentRenglon.ToString()) 
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {

                        if (column.ColumnName == "Día")
                        {
                            if (row[column.ColumnName].ToString() != sParent)
                            {
                                html.Append("<td  style='display:none;' ></td><td style='font-weight:bold !important;  background-color:rgb(115, 166, 22); color:white; ' padding-left: 19px; colspan='" + (dtGeneralReport.Columns.Count - 1).ToString() + "'>");
                                html.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +   row[column.ColumnName]);
                                html.Append("</td></tr>     ");
                                iParentRenglon = iRenglon;
                                //iRenglon++;
                                html.Append("<tr data-id = '" + iRenglon.ToString() + "' data-parent = '" + iParentRenglon.ToString() + "' >");
                                sParent = row[column.ColumnName].ToString();
                            }
                        }


                        if (column.ColumnName == "IdArchivo")
                        {
                            html.Append("<td style='display:none;'>");
                            html.Append("");
                            html.Append("</td>");
                        }
                        else
                        {
                            html.Append("<td style='text-align: center;' >");
                            html.Append(row[column.ColumnName]);
                            html.Append("</td>");
                        }
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
