using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ibd.Framework.AccesoDatos;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class gruposDa
    {
        private readonly IBaseDatos _baseDatos;
        DataTable dtData;
        public gruposDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public DataTable Consultar()
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_obtieneGrupos]");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@id", SqlDbType.Int);
                sqlParameters[0].Value = int.Parse("0");

                sqlParameters[1] = new SqlParameter("@grupo", SqlDbType.NVarChar);
                sqlParameters[1].Value = "";
                                
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener catalogo de Grupos", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }


    }
}
