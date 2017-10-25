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
    public  class BasesContratoNe
    {
        DataTable dtData;
        
        public DataTable GetArchivoBasesContrato(int iAño, int iMes, int iCentral)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[SIMER].[dbo].[usp_consultaCargasBasesContrato]");
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = iAño;

                sqlParameters[1] = new SqlParameter("@Mes", SqlDbType.Int);
                sqlParameters[1].Value = iMes;

                sqlParameters[2] = new SqlParameter("@IdPuntoCarga", SqlDbType.Int);
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
            try
            {
                html.Append(" <thead>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    html.Append("<th class='text-uppercase text -enter' >");
                    html.Append(column.ColumnName.Replace("|", "<br/>"));
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
                    
        public Int64 InsertaArchivoBasesContrato(ArchivoBasesContratoEN en)
        {
            var oDa = new ArchivoBasesContratoDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.insertaArchivoBaseContrato(en);
            return Dt;
        }
        public Int64 actualizaArchivoBasesContrato(ArchivoBasesContratoEN en)
        {
            var oDa = new ArchivoBasesContratoDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.actulaizaArchivoBaseContrato(en);
            return Dt;
        }
        public Boolean InsertaRegistroBasesContrato(BasesContratoEN en)
        {
            var oDa = new BasesContratoDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.insertaRegistro(en);
            return true;
        }

    }
}
