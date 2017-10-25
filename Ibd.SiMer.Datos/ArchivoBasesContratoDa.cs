using Ibd.Framework.AccesoDatos;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class ArchivoBasesContratoDa
    {
        private readonly IBaseDatos _baseDatos;

        public ArchivoBasesContratoDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public Int64 insertaArchivoBaseContrato(ArchivoBasesContratoEN en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[SIMER]..[usp_insertArchivoBaseContrato]");
                SqlParameter[] sqlParameters = new SqlParameter[1];                

                sqlParameters[0] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParameters[0].Value = en.Archivo;

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return int.Parse(dtData.Rows[0][0].ToString());
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al inserta Archivo Bases de Contratos", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }

        public Int64 actulaizaArchivoBaseContrato(ArchivoBasesContratoEN en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[SIMER]..[usp_actualizaArchivoBaseContrato]");
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@IdArchivo", SqlDbType.BigInt);
                sqlParameters[0].Value = en.IdArchivo;

                

                sqlParameters[1] = new SqlParameter("@numReg", SqlDbType.Int);
                sqlParameters[1].Value = en.NoRegistros;

                sqlParameters[2] = new SqlParameter("@Mensaje", SqlDbType.NVarChar);
                sqlParameters[2].Value = en.Mensaje;

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return int.Parse(dtData.Rows[0][0].ToString());
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al actualizar Archivo Bases de contratos", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }


    }
}
