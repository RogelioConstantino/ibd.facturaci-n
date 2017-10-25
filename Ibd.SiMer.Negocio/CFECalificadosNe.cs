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
    public class CFECalificadosNe
    {
        DataTable dtData;
        DataSet dtSet;
        public Boolean InsertaRegistro(CFECalificadosEn en)
        {
            var oDa = new CFECalificadosDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.insertaRegistro(en);
            return true;
        }


        public DataTable GetCFECalificados(int iAño, int iMes )
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_consultaCFECalificados]");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = iAño;

                sqlParameters[1] = new SqlParameter("@Mes", SqlDbType.Int);
                sqlParameters[1].Value = iMes;
                
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
                    html.Append("<tr style='color: #5c881a; text-align: center !important;'><th  style='text-align: center !important;'  colspan= '2' class='text-uppercase sorting_disabled'>	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>TC	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>SML	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>Precio Gas	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>CTUNG	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>Combustible	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>CVOM	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>Transmisión	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>CENACE	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>Precio Energía	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>PML JOV-230	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>TBFin	</th><th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>CFE Calificados</th>	<th  style='text-align: center !important;'  class='text-uppercase sorting_disabled'>Precio CFE Calificados</th> </tr>");
                    html.Append("<tr style='color: #5c881a;' ><th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>Dia</th><th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>Hora</th>	<th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/USD	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>USD/MMBTU	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>USD/MMBTU	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MMBTU/MWh	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>USD/MWh	</th> <th style='text-align: center !important;'  class='text-uppercase text-center sorting_disabled'>USD/MWh	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/MWh	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/MWh	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/MWh	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/MWh		</th> <th  style='text-align: center !important;' class='text-uppercase text-enter sorting_disabled'>MXN/MWh	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MW	</th> <th  style='text-align: center !important;' class='text-uppercase text-center sorting_disabled'>MXN/MWh</th>  </tr> ");
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");

                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    iRenglon++;
                    html.Append("<tr  data-id='" + iRenglon.ToString() + "' data-parent='" + iParentRenglon.ToString() + "'>"); //(iParentRenglon== 0?"": iParentRenglon.ToString()) 
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {
                        if (column.ColumnName == "IdArchivo")
                        {
                            html.Append("<td style='display:none;'>");
                            html.Append("");
                            html.Append("</td>");
                        }
                        else
                        {
                                html.Append("<td style='text-align: right;' >");
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
