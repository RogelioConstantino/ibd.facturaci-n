using Ibd.Framework;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using Ibd.SiMer.Negocio.Managers;
using System.Data;
using Ibd.SiMer.Datos;
using System.Data.SqlClient;
using System;
using System.Text;

namespace Ibd.SiMer.Negocio
{
    public class ResumenFacNe
    {

        DataTable dtData;
        DataSet dtSet;
              

        public DataTable GetResumenFac(int iAño, int iMes, int iCentral)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_consultaResumen]");
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = iAño;

                sqlParameters[1] = new SqlParameter("@Mes", SqlDbType.Int);
                sqlParameters[1].Value = iMes;

                sqlParameters[2] = new SqlParameter("@IdCentral", SqlDbType.Int);
                sqlParameters[2].Value = iCentral;

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


        public DataTable GetResumenCFECostosTrans(int iAño, int iMes, int iCentral)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_getResumenCFECostosTrans]");
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = iAño;

                sqlParameters[1] = new SqlParameter("@Mes", SqlDbType.Int);
                sqlParameters[1].Value = iMes;

                sqlParameters[2] = new SqlParameter("@IdEmpresa", SqlDbType.Int);
                sqlParameters[2].Value = iCentral;

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
            StringBuilder html = new StringBuilder();

            string sParent = "";
            string sChild = "";
            int iRenglon = 0;
            int iParentRenglon = 0;
            try
            {
                html.Append(" <thead>");
                //html.Append("<tr style='color: #5c881a;'>");
                //foreach (DataColumn column in dtGeneralReport.Columns)
                //{
                //    if (column.ColumnName == "TipoRenglonResumen")
                //    {
                //        html.Append("<td style='display:none;'>");
                //        html.Append("");
                //        html.Append("</td>");
                //    }
                //    else if (column.ColumnName == "-")
                //    {
                //        html.Append("<td >");
                //        html.Append("");
                //        html.Append("</td>");
                //    }
                //    else
                //    {
                //        html.Append("<th class='text-uppercase text -enter' >");
                //        html.Append(column.ColumnName.Replace("|", "<br/>"));
                //        html.Append("</th>");
                //    }                    
                //}

                html.Append("<tr style='color: #5c881a; text-align: center !important; '> <td  style='border-bottom: 0px solid;' ></td ><th colspan='5' style='text-align: center !important;' class='text-uppercase sorting_disabled'>KWH</th> <th  style='text-align: center !important;' colspan= '4' class='text-uppercase sorting_disabled'>KW</th>	 <td></td> <td></td> <th  style='text-align: center !important;'  colspan = '4' class='text-uppercase sorting_disabled'>Horas pedido</th> <th colspan = '4' style='text-align: center !important;'   class='text-uppercase sorting_disabled'>Factor de carga</th> </tr>");                
                html.Append("<tr style='color: #5c881a;' > <td style='display:none;' class='sorting_disabled'></td> <th style='border-right: 1px solid;' class='text-uppercase text-enter sorting_disabled'></th> <th class='text-uppercase text-enter sorting_disabled'> Base</th> <th class='text-uppercase text-enter sorting_disabled'> Intermedia</th> <th class='text-uppercase text-enter sorting_disabled'> Punta</th> <th class='text-uppercase text-enter sorting_disabled'> SemiPunta</th> <th style='border-right: 1px solid;'  class='text-uppercase text-enter sorting_disabled'> TOTALES</th> <th class='text-uppercase text-enter sorting_disabled'> Base</th> <th class='text-uppercase text-enter sorting_disabled'> Intermedia</th> <th class='text-uppercase text-enter sorting_disabled'> Punta</th> <th  style='border-right: 1px solid;'  class='text-uppercase text-enter sorting_disabled'> SemiPunta</th> <th style='border-right: 1px solid;'  class='text-uppercase text-enter sorting_disabled'>KVARH</th> <th  style='border-right: 1px solid;' class='text-uppercase text-enter sorting_disabled'>%FP</th> <th class='text-uppercase text-enter sorting_disabled'>Base</th> <th class='text-uppercase text-enter sorting_disabled'> Intermedia</th> <th class='text-uppercase text-enter sorting_disabled'> Punta</th> <th  style='border-right: 1px solid;' class='text-uppercase text-enter sorting_disabled'> SemiPunta</th> <th class='text-uppercase text-enter sorting_disabled'> Base</th> <th class='text-uppercase text-enter sorting_disabled'>FacCar Intermedia</th> <th class='text-uppercase text-enter sorting_disabled'> Punta</th> <th  style='border-right: 1px solid;' class='text-uppercase text-enter sorting_disabled'> SemiPunta</th> </tr> ");

                sParent = "";
                sChild = "";

                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");

                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    iRenglon++;
                    html.Append("<tr  data-id='"+ iRenglon.ToString() + "' data-parent='"+ iParentRenglon.ToString() + "'>"); //(iParentRenglon== 0?"": iParentRenglon.ToString()) 
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {
                        if (column.ColumnName == "TipoRenglonResumen")
                        {
                            if (row[column.ColumnName].ToString() != sParent) {
                                html.Append("<td style='display:none;' ></td><td style='font-weight:bold !important;  background-color:rgb(115, 166, 22); color:white; ' colspan='" + (dtGeneralReport.Columns.Count-1).ToString() +  "'>");
                                html.Append((row[column.ColumnName].ToString()=="ENCABEZADO"? "Resultados del Permisionario" : row[column.ColumnName]));
                                html.Append("</td></tr>     ");
                                iParentRenglon = iRenglon;
                                iRenglon++;
                                html.Append("<tr data-id = '" + iRenglon.ToString() + "' data-parent = '" + iParentRenglon.ToString() + "' >");                                
                                sParent = row[column.ColumnName].ToString();                                
                            }
                        }

                        if (column.ColumnName == "TipoRenglonResumen")
                        {
                            html.Append("<td style='display:none;'>");
                            html.Append("");
                            html.Append("</td>");
                        }
                        else {

                            if (column.ColumnName == "_")
                            {
                                html.Append("<td style='font-weight:bold !important;' >");  // style='background-color: " + ((iRenglon%2==0)? "rgb(164,221,80)" : "rgb(195,233,138)") +" ; color: rgb(92, 136, 26); '
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }
                            else
                            {
                                html.Append("<td style='text-align: right;' >");
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }
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
