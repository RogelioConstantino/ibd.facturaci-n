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
    public class ArchivoResumenFacNe
    {
        DataTable dtData;
        DataSet dtSet;

        public Int64 InsertaRegistroResumenHeader(ArchivoResumenFacEn en)
        {
            var oDa = new ArchivoResumenFacDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.insertaRegistroHeader(en);
            return Dt;
        }
        public Int64 InsertaRegistroResumenHeaderCFECostosTrans(ArchivoResumenFacEn en)
        {
            var oDa = new ArchivoResumenFacDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.insertaRegistroHeaderCFECostosTrans(en);
            return Dt;
        }

        public Int64 actualizaRegistroResumenHeader(ArchivoResumenFacEn en)
        {
            var oDa = new ArchivoResumenFacDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.actulaizaRegistroHeader (en);
            return Dt;
        }


        public Int64 actualizaRegistroResumenHeaderCFECostosTrans(ArchivoResumenFacEn en)
        {
            var oDa = new ArchivoResumenFacDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.actulaizaRegistroHeaderCFECostosTrans(en);
            return Dt;
        }


        public Boolean InsertaRegistroResumen(ResumenFacEn en)
        {
            var oDa = new ResumenFacDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.insertaRegistro(en);
            return true;
        }
        
        public Boolean InsertaRegistroResumenCFECostosTrans(ResumenFacEn en)
        {
            var oDa = new ResumenFacDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.insertaRegistroCFECostosTrans(en);
            return true;
        }

        public Boolean InsertaPuntoCargaCFECostosTrans(long idArchivo ,    int Renglon, String PuntoCarga , int Año, int Mes, int Central, String usuario )
        {
            var oDa = new ResumenFacDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.insertaPuntoCargaCostosTrans(idArchivo, Renglon, PuntoCarga, Año, Mes, Central, usuario);
            return true;
        }

        public DataTable GetArchivoResumen(int iAño, int iMes, int iCentral)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_consultaCargasResumen]");
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
        
        public DataTable GetArchivoResumenCFECostosTrans(int iAño, int iMes, int iCentral)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_getCargasResumenCFECostosTrans]");
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
