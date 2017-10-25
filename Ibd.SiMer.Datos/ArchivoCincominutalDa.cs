using Ibd.Framework.AccesoDatos;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class ArchivoCincominutalDa
    {
        private readonly IBaseDatos _baseDatos;

        //Inyeccion de dependencia
        public ArchivoCincominutalDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public ArchivoCincominutalesEn Agregar(ArchivoCincominutalesEn archivoConcominutal)
        {
            const string sql = "usp_ArchivoCincominutalesAgregar";
            try
            {
                _baseDatos.Conectar();
                _baseDatos.CrearComando(sql, CommandType.StoredProcedure);
                _baseDatos.AsignarParametroWhitValue("@Archivo", archivoConcominutal.Archivo);
                _baseDatos.AsignarParametroWhitValue("@IdPuntoCarga", archivoConcominutal.IdPuntoCarga.ToDBNull());
                _baseDatos.AsignarParametroWhitValue("@FechaArchivo", archivoConcominutal.FechaArchivo.ToDBNull());
                _baseDatos.AsignarParametroWhitValue("@FechaCreacion", archivoConcominutal.FechaCreacion.ToDBNull());
                _baseDatos.AsignarParametroWhitValue("@IdCargaArchivoEstatus", archivoConcominutal.IdCargaArchivoEstatus);
                _baseDatos.AsignarParametroWhitValue("@NoRegistros", archivoConcominutal.NoRegistros.ToDBNull());
                _baseDatos.AsignarParametroWhitValue("@Mensaje", archivoConcominutal.Mensaje.ToDBNull());
                _baseDatos.AsignarParametroWhitValue("@RMU", archivoConcominutal.RMU.ToDBNull());

                archivoConcominutal.IdArchivoCincominutales = _baseDatos.EjecutarEscalar<int>();
            }
            catch (BaseDatosException ex)
            {
                throw new BaseDatosException("Error al agregar Archivo de CincoMinutales.", ex);
            }
            finally
            {
                _baseDatos.Desconectar();
            }

            return archivoConcominutal;
        }

        public void Actualizar(ArchivoCincominutalesEn archivoConcominutal)
        {
            const string sql = "usp_ArchivoCincominutalesActualizar";
            try
            {
                _baseDatos.Conectar();
                _baseDatos.CrearComando(sql, CommandType.StoredProcedure);
                _baseDatos.AsignarParametroWhitValue("@IdCargaArchivoEstatus", archivoConcominutal.IdCargaArchivoEstatus);
                _baseDatos.AsignarParametroWhitValue("@NoRegistros", archivoConcominutal.NoRegistros.ToDBNull());
                _baseDatos.AsignarParametroWhitValue("@Mensaje", archivoConcominutal.Mensaje.ToDBNull());
                _baseDatos.AsignarParametroWhitValue("@IdArchivoCincominutales", archivoConcominutal.IdArchivoCincominutales);

                _baseDatos.EjecutarComando();
            }
            catch (BaseDatosException ex)
            {
                throw new BaseDatosException("Error al actualizar registro de Archivo de CincoMinutales.", ex);
            }
            finally
            {
                _baseDatos.Desconectar();
            }
        }

        public ArchivoCincominutalesEn Cargar(ArchivoCincominutalesEn archivoConcominutal)
        {
            const string sql = "usp_CargaArchivo";
            try
            {
                _baseDatos.Conectar();
                _baseDatos.CrearComando(sql, CommandType.StoredProcedure);
                _baseDatos.AsignarParametroWhitValue("@Archivo", archivoConcominutal.Archivo);
                _baseDatos.AsignarParametroWhitValue("@IdArchivoCincominutales", archivoConcominutal.IdArchivoCincominutales);

                archivoConcominutal.NoRegistros = _baseDatos.EjecutarEscalar<int>();
            }
            finally
            {
                _baseDatos.Desconectar();
            }

            return archivoConcominutal;
        }
                
        public Int32  insertarHeaderArchivo(ArchivoCincominutalesEn archivoConcominutal)
        {
            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_ArchivoCincominutalesInsertar]");
                SqlParameter[] sqlParameters = new SqlParameter[8];

                sqlParameters[0] = new SqlParameter("@Archivo", SqlDbType.Int);
                sqlParameters[0].Value = archivoConcominutal.Archivo ;

                sqlParameters[1] = new SqlParameter("@RMU", SqlDbType.NVarChar);
                sqlParameters[1].Value = "";

                sqlParameters[2] = new SqlParameter("@IdPuntoCarga", SqlDbType.NVarChar);
                sqlParameters[2].Value = archivoConcominutal.IdPuntoCarga;

                sqlParameters[3] = new SqlParameter("@FechaArchivo", SqlDbType.NVarChar);
                sqlParameters[3].Value = archivoConcominutal.sFechaArchivo;
                
                sqlParameters[5] = new SqlParameter("@IdCargaArchivoEstatus", SqlDbType.NVarChar);
                sqlParameters[5].Value = (int)CargaArchivoEstatus.EnCurso;

                sqlParameters[6] = new SqlParameter("@NoRegistros", SqlDbType.NVarChar);
                sqlParameters[6].Value = "0";

                sqlParameters[7] = new SqlParameter("@Mensaje", SqlDbType.NVarChar);
                sqlParameters[7].Value = "";
                                
                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);
                                
                return Int32.Parse(dtData.Rows[0][0].ToString());

            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al insertarHeaderArchivo", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }


        public DataTable actualizaHeaderArchivo(ArchivoCincominutalesEn archivoConcominutal)
        {
            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_ArchivoCincominutalesActualizar]");
                SqlParameter[] sqlParameters = new SqlParameter[4];
                
                sqlParameters[0] = new SqlParameter("@IdCargaArchivoEstatus", SqlDbType.Int);
                sqlParameters[0].Value = (int)CargaArchivoEstatus.EnCurso;

                sqlParameters[1] = new SqlParameter("@NoRegistros", SqlDbType.Int);
                sqlParameters[1].Value = archivoConcominutal.NoRegistros ;

                sqlParameters[2] = new SqlParameter("@Mensaje", SqlDbType.NVarChar);
                sqlParameters[2].Value = archivoConcominutal.Mensaje ;
                
                sqlParameters[3] = new SqlParameter("@IdArchivoCincominutales", SqlDbType.Int);
                sqlParameters[3].Value = archivoConcominutal.IdArchivoCincominutales;


                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al actualizaHeaderArchivo", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }


        public Boolean actualizaCincominutales(CincoMinutalEn CincoMinutal)
        {
            try
            {                
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_ActualizaCincominutales]");
                SqlParameter[] sqlParameters = new SqlParameter[8];

                sqlParameters[0] = new SqlParameter("@rmu", SqlDbType.Int);
                sqlParameters[0].Value = CincoMinutal.rmu;

                sqlParameters[1] = new SqlParameter("@fecha", SqlDbType.NVarChar);
                sqlParameters[1].Value = CincoMinutal.fecha;

                sqlParameters[2] = new SqlParameter("@ahora", SqlDbType.NVarChar);
                sqlParameters[2].Value = CincoMinutal.ahora;
                
                sqlParameters[3] = new SqlParameter("@kvarh", SqlDbType.Decimal);
                sqlParameters[3].Value = CincoMinutal.kvarh;
                
                sqlParameters[4] = new SqlParameter("@kwhe", SqlDbType.Decimal);
                sqlParameters[4].Value = CincoMinutal.kwhe;
                
                sqlParameters[5] = new SqlParameter("@kwhr", SqlDbType.Decimal);
                sqlParameters[5].Value = CincoMinutal.kwhr;

                sqlParameters[6] = new SqlParameter("@tipo", SqlDbType.NVarChar);
                sqlParameters[6].Value = CincoMinutal.tipo;

                sqlParameters[7] = new SqlParameter("@IdArchivoCincominutales", SqlDbType.BigInt);
                sqlParameters[7].Value = CincoMinutal.IdArchivoCincominutales;
                
                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return true;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al actualizaHeaderArchivo", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }



    }
}