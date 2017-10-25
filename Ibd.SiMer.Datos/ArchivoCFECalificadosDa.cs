using Ibd.Framework.AccesoDatos;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class ArchivoCFECalificadosDa
    {
        private readonly IBaseDatos _baseDatos;

        public ArchivoCFECalificadosDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public Int64 insertaHeader(ArchivoCFECalificadosEn en)
        {
            try
            {

                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_insertFileCFECalificados]");
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[0].Value = en.año;

                sqlParameters[1] = new SqlParameter("@Mes", SqlDbType.Int);
                sqlParameters[1].Value = en.mes;
                
                sqlParameters[2] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParameters[2].Value = en.Archivo;

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return int.Parse(dtData.Rows[0][0].ToString());

            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al inserta registro cfe calificados", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }


        public Int64 actulaizaHeader(ArchivoCFECalificadosEn en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_actualizaFileCFECalificados]");
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@IdArchivo", SqlDbType.BigInt);
                sqlParameters[0].Value = en.IdArchivo;

                sqlParameters[1] = new SqlParameter("@Año", SqlDbType.Int);
                sqlParameters[1].Value = en.año;

                sqlParameters[2] = new SqlParameter("@Mes", SqlDbType.Int);
                sqlParameters[2].Value = en.mes;                

                sqlParameters[3] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParameters[3].Value = en.Archivo;

                sqlParameters[4] = new SqlParameter("@numReg", SqlDbType.Int);
                sqlParameters[4].Value = en.NoRegistros;

                sqlParameters[5] = new SqlParameter("@Mensaje", SqlDbType.NVarChar);
                sqlParameters[5].Value = en.Mensaje;

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return int.Parse(dtData.Rows[0][0].ToString());
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al actualizar registro CFE Calificados", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }

    }
}
