using System;
using System.Data;
using System.Collections.Generic;
using Ibd.SiMer.Entidades;
using Ibd.Framework.Extensores;
using Ibd.Framework.AccesoDatos;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class puntosCargaDa
    {
        private readonly IBaseDatos _baseDatos;
        DataTable dtData;
        public puntosCargaDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public DataTable Consultar()
        {
                        
            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_obtienePuntosCarga]");//***[IBD.Facturacion]..
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@idGrupo", SqlDbType.Int);
                sqlParameters[0].Value = int.Parse("0");
                sqlParameters[1] = new SqlParameter("@idCliente", SqlDbType.Int);
                sqlParameters[1].Value = int.Parse("0");
                sqlParameters[2] = new SqlParameter("@idPuntoCarga", SqlDbType.Int);
                sqlParameters[2].Value = int.Parse("0"); 

                sqlParameters[3] = new SqlParameter("@PuntoCarga", SqlDbType.NVarChar);
                sqlParameters[3].Value = "";
                sqlParameters[4] = new SqlParameter("@RMU", SqlDbType.NVarChar);
                sqlParameters[4].Value = "";
                sqlParameters[5] = new SqlParameter("@RPU", SqlDbType.NVarChar);
                sqlParameters[5].Value = "";

                //_baseDatos.CrearComando(sql, CommandType.StoredProcedure);

                //_baseDatos.AsignarParametroWhitValue("@idGrupo", 0);
                //_baseDatos.AsignarParametroWhitValue("@idCliente", 0);

                //_baseDatos.AsignarParametroWhitValue("@idPuntoCarga", 0);
                //_baseDatos.AsignarParametroWhitValue("@PuntoCarga", "");
                //_baseDatos.AsignarParametroWhitValue("@RMU", "");
                //_baseDatos.AsignarParametroWhitValue("@RP", "");

                //var datos = _baseDatos.TraerDataTable();
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData ;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener catalogo de Puntos de carga", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }

        public DataTable getPuntosCarga()
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_obtienePuntosCarga]");
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@idGrupo", SqlDbType.Int);
                sqlParameters[0].Value = int.Parse("0");
                sqlParameters[1] = new SqlParameter("@idCliente", SqlDbType.Int);
                sqlParameters[1].Value = int.Parse("0");
                sqlParameters[2] = new SqlParameter("@idPuntoCarga", SqlDbType.Int);
                sqlParameters[2].Value = int.Parse("0");

                sqlParameters[3] = new SqlParameter("@PuntoCarga", SqlDbType.NVarChar);
                sqlParameters[3].Value = "";
                sqlParameters[4] = new SqlParameter("@RMU", SqlDbType.NVarChar);
                sqlParameters[4].Value = "";
                sqlParameters[5] = new SqlParameter("@RPU", SqlDbType.NVarChar);
                sqlParameters[5].Value = "";

                //_baseDatos.CrearComando(sql, CommandType.StoredProcedure);

                //_baseDatos.AsignarParametroWhitValue("@idGrupo", 0);
                //_baseDatos.AsignarParametroWhitValue("@idCliente", 0);

                //_baseDatos.AsignarParametroWhitValue("@idPuntoCarga", 0);
                //_baseDatos.AsignarParametroWhitValue("@PuntoCarga", "");
                //_baseDatos.AsignarParametroWhitValue("@RMU", "");
                //_baseDatos.AsignarParametroWhitValue("@RP", "");

                //var datos = _baseDatos.TraerDataTable();
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener catalogo de Puntos de carga", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }

    }
}
