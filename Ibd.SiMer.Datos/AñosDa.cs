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
    public class AñosDa
    {

        DataTable dtData;
        
        public DataTable obtieneAñosCargados()
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_obtieneAñosCargados]");//***[IBD.Facturacion]..
                SqlParameter[] sqlParameters = new SqlParameter[0];
                
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
                
                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener años cargados", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
                
            }
        }
               

        public DataTable obtieneAñosCargadosSegregacion()
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..usp_obtieneAñosCargadosSegregacion");
                SqlParameter[] sqlParameters = new SqlParameter[0];

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener años cargados Segregacion", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }
        public DataTable obtieneAñosCargadosResumen()
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..usp_obtieneAñosCargadosResumen");
                SqlParameter[] sqlParameters = new SqlParameter[0];

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener años cargados resumen", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }
        public DataTable obtieneAñosResumenCFECostosTrans()
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..usp_getAñosResumenCFECostosTrans");
                SqlParameter[] sqlParameters = new SqlParameter[0];

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener años cargados resumen CFE Costos Trans", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }


        public DataTable obtieneAñosResumenCFECalificados()
        {
            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion].[dbo].[usp_obtieneAñosCFECalificados]");
                SqlParameter[] sqlParameters = new SqlParameter[0];

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener años cargados resumen CFE Calificados", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }

    }
}
