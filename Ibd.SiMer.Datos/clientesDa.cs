using System;
using System.Data;
using System.Collections.Generic;
using Ibd.SiMer.Entidades;
using Ibd.Framework.Extensores;
using Ibd.Framework.AccesoDatos;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class clientesDa
    {
        private readonly IBaseDatos _baseDatos;
        DataTable dtData;
        public clientesDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public DataTable Consultar()
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_obtieneClientes]");
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@idGrupo", SqlDbType.Int);
                sqlParameters[0].Value = int.Parse("0");

                sqlParameters[1] = new SqlParameter("@idCliente", SqlDbType.Int);
                sqlParameters[1].Value = int.Parse("0");
                
                sqlParameters[2] = new SqlParameter("@Cliente", SqlDbType.NVarChar);
                sqlParameters[2].Value = "";
                

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
