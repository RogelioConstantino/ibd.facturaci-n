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
    public class ArchivoCFECalificadosNe
    {

        DataTable dtData;
        DataSet dtSet;
        
        public DataTable GetArchivo(int iAño, int iMes)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_getCargasCFECalificados]");
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
        
        public Int64 InsertaHeader(ArchivoCFECalificadosEn en)
        {
            var oDa = new  ArchivoCFECalificadosDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.insertaHeader(en);
            return Dt;
        }

        //public Boolean InsertaRegistro(CFECalificadosEn en)
        //{
        //    var oDa = new ResumenFacDa(Singleton<ConexionMng>.Single.Default());
        //    var Dt = oDa.insertaRegistro(en);
        //    return true;
        //}


        public Int64 actualizaHeader(ArchivoCFECalificadosEn en)
        {
            var oDa = new ArchivoCFECalificadosDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.actulaizaHeader(en);
            return Dt;
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



    }
}