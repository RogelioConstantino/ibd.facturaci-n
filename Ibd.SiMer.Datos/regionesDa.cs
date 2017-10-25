using System;
using System.Data;
using System.Collections.Generic;
using Ibd.SiMer.Entidades;
using Ibd.Framework.Extensores;
using Ibd.Framework.AccesoDatos;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class regionesDa
    { 
        private readonly IBaseDatos _baseDatos;
        DataTable dtData;

        public regionesDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public DataTable Consultar()
        {

            try
            {
                //_baseDatos.Conectar();
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[usp_obtieneRegiones]");//***[IBD.Facturacion]..
                SqlParameter[] sqlParameters = new SqlParameter[0];
                                
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                return dtData;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener catalogo de Regiones", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }

        }
    }

}

