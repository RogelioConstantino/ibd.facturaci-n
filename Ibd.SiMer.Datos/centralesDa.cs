using System;
using System.Data;
using System.Collections.Generic;
using Ibd.SiMer.Entidades;
using Ibd.Framework.Extensores;
using Ibd.Framework.AccesoDatos;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class centralesDa
    {
        private readonly IBaseDatos _baseDatos;
        DataTable dtData;
        public centralesDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public DataTable Consultar()
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_obtieneCentrales]");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@idCentral", SqlDbType.Int);
                sqlParameters[0].Value = int.Parse("0");

                sqlParameters[1] = new SqlParameter("@Central", SqlDbType.VarChar);
                sqlParameters[1].Value = "";
                                
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener catalogo de Centrales", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }
    }
}
