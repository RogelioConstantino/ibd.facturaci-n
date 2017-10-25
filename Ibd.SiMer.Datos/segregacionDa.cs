using Ibd.Framework.AccesoDatos;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class segregacionDa
    {

        private readonly IBaseDatos _baseDatos;

        public segregacionDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }
        
        public Boolean insertaRegistro(segregacionEn en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_insertaRegistroSegregacion]");
                SqlParameter[] sqlParameters = new SqlParameter[9];

                sqlParameters[0] = new SqlParameter("@IdArchivo", SqlDbType.BigInt);
                sqlParameters[0].Value = en.idArchivo;

                sqlParameters[1] = new SqlParameter("@fecha", SqlDbType.NVarChar);
                sqlParameters[1].Value = en.fecha;

                //sqlParameters[1] = new SqlParameter("@hora", SqlDbType.NVarChar);
                //sqlParameters[1].Value = en.hora;

                sqlParameters[2] = new SqlParameter("@capacidad", SqlDbType.Float);
                sqlParameters[2].Value = en.CAPACIDAD;

                sqlParameters[3] = new SqlParameter("@interconexion", SqlDbType.Float);
                sqlParameters[3].Value = en.INTERCONEXION;

                sqlParameters[4] = new SqlParameter("@dentro", SqlDbType.Float);
                sqlParameters[4].Value = en.DENTRO;

                sqlParameters[5] = new SqlParameter("@tolerancia", SqlDbType.Float);
                sqlParameters[5].Value = en.TOLERANCIA;

                sqlParameters[6] = new SqlParameter("@fuera", SqlDbType.Float);
                sqlParameters[6].Value = en.FUERA;

                sqlParameters[7] = new SqlParameter("@pruebas", SqlDbType.Float);
                sqlParameters[7].Value = en.PRUEBAS;

                sqlParameters[8] = new SqlParameter("@declaradas", SqlDbType.Float);
                sqlParameters[8].Value = en.DECLARADAS;

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return true;
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
        
        public Int64 insertaRegistroHeader(ArchivoSegregacionEn en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_insertArchivosSegregacion]");
                SqlParameter[] sqlParameters = new SqlParameter[4];
                                
                sqlParameters[0] = new SqlParameter("@Año", SqlDbType.NVarChar);
                sqlParameters[0].Value = en.año;

                sqlParameters[1] = new SqlParameter("@Mes", SqlDbType.Float);
                sqlParameters[1].Value = en.mes;

                sqlParameters[2] = new SqlParameter("@IdCentral", SqlDbType.Float);
                sqlParameters[2].Value = en.IdCentral;

                sqlParameters[3] = new SqlParameter("@FileName", SqlDbType.Float);
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


    }
}
