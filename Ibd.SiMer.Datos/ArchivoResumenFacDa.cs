using Ibd.Framework.AccesoDatos;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class ArchivoResumenFacDa
    {
        private readonly IBaseDatos _baseDatos;

        public ArchivoResumenFacDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public Int64 insertaRegistroHeader(ArchivoResumenFacEn en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_insertArchivosResumen]");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = en.año;

                sqlParameters[1] = new SqlParameter("@Mes", SqlDbType.Int);
                sqlParameters[1].Value = en.mes;

                sqlParameters[2] = new SqlParameter("@IdCentral", SqlDbType.BigInt);
                sqlParameters[2].Value = en.IdCentral;

                sqlParameters[3] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParameters[3].Value = en.Archivo;

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return int.Parse(dtData.Rows[0][0].ToString());
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al inserta registro segregacion", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }

        public Int64 insertaRegistroHeaderCFECostosTrans(ArchivoResumenFacEn en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_insertFileResumenCFECostosTrans]");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = en.año;

                sqlParameters[1] = new SqlParameter("@Mes", SqlDbType.Int);
                sqlParameters[1].Value = en.mes;

                sqlParameters[2] = new SqlParameter("@IdEmpresa", SqlDbType.BigInt);
                sqlParameters[2].Value = en.IdCentral;

                sqlParameters[3] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParameters[3].Value = en.Archivo;

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return int.Parse(dtData.Rows[0][0].ToString());
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al inserta registro segregacion", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }

        public Int64 actulaizaRegistroHeader(ArchivoResumenFacEn en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_actualizaArchivosResumen]");
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@IdArchivo", SqlDbType.BigInt);
                sqlParameters[0].Value = en.IdArchivo;

                sqlParameters[1] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[1].Value = en.año;

                sqlParameters[2] = new SqlParameter("@Mes", SqlDbType.Int);
                sqlParameters[2].Value = en.mes;

                sqlParameters[3] = new SqlParameter("@IdEmpresa", SqlDbType.BigInt);
                sqlParameters[3].Value = en.IdCentral;

                sqlParameters[4] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParameters[4].Value = en.Archivo;

                sqlParameters[5] = new SqlParameter("@numReg", SqlDbType.Int);
                sqlParameters[5].Value = en.NoRegistros;

                sqlParameters[6] = new SqlParameter("@Mensaje", SqlDbType.NVarChar);
                sqlParameters[6].Value = en.Mensaje;

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return int.Parse(dtData.Rows[0][0].ToString());
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al actualizar registro Resumen", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }


        public Int64 actulaizaRegistroHeaderCFECostosTrans(ArchivoResumenFacEn en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_actualizaFileResumenCFECostosTrans]");
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@IdArchivo", SqlDbType.BigInt);
                sqlParameters[0].Value = en.IdArchivo;

                sqlParameters[1] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[1].Value = en.año;

                sqlParameters[2] = new SqlParameter("@Mes", SqlDbType.Int);
                sqlParameters[2].Value = en.mes;

                sqlParameters[3] = new SqlParameter("@IdEmpresa", SqlDbType.BigInt);
                sqlParameters[3].Value = en.IdCentral;

                sqlParameters[4] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParameters[4].Value = en.Archivo;

                sqlParameters[5] = new SqlParameter("@numReg", SqlDbType.Int);
                sqlParameters[5].Value = en.NoRegistros;

                sqlParameters[6] = new SqlParameter("@Mensaje", SqlDbType.NVarChar);
                sqlParameters[6].Value = en.Mensaje;

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return int.Parse(dtData.Rows[0][0].ToString());
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al actualizar registro Resumen", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }


    }
}
