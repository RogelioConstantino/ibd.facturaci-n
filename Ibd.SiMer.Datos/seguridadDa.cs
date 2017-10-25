using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ibd.Framework.AccesoDatos;
using System.Data.SqlClient;

using Ibd.SiMer.Entidades;

namespace Ibd.SiMer.Datos
{
   public class seguridadDa:usuariosEn
    {
        private readonly IBaseDatos _baseDatos;
        DataTable dtData;

        public seguridadDa(IBaseDatos baseDatos)
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



        public ConnectionDB con = new ConnectionDB();
        DataTable Usr;

        public DataTable GetUser()
        {
            String FullName = string.Empty;
            try
            {
                string query = string.Format("select IdUsuario, Nombre FirstName,ApellidoPaterno LastName , Email from [IBD.Seguridad]..Usuarios where (usuario = @Email )  AND Password =@Password "); // or Email like @Email) "); //
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Email", SqlDbType.VarChar);
                sqlParameters[0].Value = Convert.ToString(UserName);
                sqlParameters[1] = new SqlParameter("@Password", SqlDbType.VarChar);
                sqlParameters[1].Value = Convert.ToString(Password);

                con.dbConnection();
                Usr = con.executeSelectQuery(query, sqlParameters);              

            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "CreateTableHTML";
                //clsError.LogWrite();
            }

            return Usr;
        }


    }


}
