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
    public class rptCostosTransmisionNe
    {

        DataTable dtTable;
        DataSet dtSet;
        public DataTable GetGeneralReport(string strAño, string strMes)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..usp_RptCostosTrans");//***[IBD.Facturacion]..
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@intAnio", SqlDbType.NVarChar);
                sqlParameters[0].Value = strAño;

                sqlParameters[1] = new SqlParameter("@intMes", SqlDbType.Int);
                sqlParameters[1].Value = int.Parse(strMes);

                con.dbConnection();
                dtTable = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtTable;
        }

    }
}
