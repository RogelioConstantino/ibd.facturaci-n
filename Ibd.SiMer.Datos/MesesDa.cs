using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Ibd.Framework.AccesoDatos;


namespace Ibd.SiMer.Datos
{
    public class MesesDa
    {
        DataTable dtData;



        public DataTable obtieneMesesCargados(int iAño)
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_obtieneMesesCargados]");//***[IBD.Facturacion]..
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = iAño;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener meses cargados", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }


        public DataTable obtieneMesesCargadosSegregacion(int iAño)
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..usp_obtieneMesesCargadosSegregacion");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = iAño;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener meses cargados Segregacion", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }


        public DataTable obtieneMesesCargadosResumen(int iAño)
        {
            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..usp_obtieneMesesCargadosResumen");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = iAño;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener meses cargados Resumen", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }
        
        public DataTable obtieneMesesResumenCFECostosTrans(int iAño)
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..usp_getMesesResumenCFECostosTrans");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = iAño;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener meses cargados Resumen CFE Costos Trans", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }


        public DataTable obtieneMesesCFECalificados(int iAño)
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..usp_getMesesCFECalificados");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = iAño;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener meses cargados Resumen CFE Calificados", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }


    }
}
