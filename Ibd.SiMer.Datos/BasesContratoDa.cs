using Ibd.Framework.AccesoDatos;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class BasesContratoDa
    {
        private readonly IBaseDatos _baseDatos;

        public BasesContratoDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public Boolean insertaRegistro(BasesContratoEN en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[SIMER]..[usp_insertaRegistroBaseContrato]");
                SqlParameter[] sqlParameters = new SqlParameter[12];

                sqlParameters[0] = new SqlParameter("@IdArchivo", SqlDbType.BigInt);
                
                sqlParameters[1] = new SqlParameter("@rpu", SqlDbType.VarChar);
                sqlParameters[2] = new SqlParameter("@rmu", SqlDbType.VarChar);
                
                sqlParameters[3] = new SqlParameter("@DescuentoEnergia_B", SqlDbType.Float);
                sqlParameters[4] = new SqlParameter("@DescuentoEnergia_I", SqlDbType.Float);
                sqlParameters[5] = new SqlParameter("@DescuentoEnergia_P", SqlDbType.Float);

                sqlParameters[6] = new SqlParameter("@DescuentoDemanda", SqlDbType.Float);
                
                sqlParameters[7] = new SqlParameter("@CFPm", SqlDbType.Float);
                sqlParameters[8] = new SqlParameter("@CVPm", SqlDbType.Float);

                sqlParameters[9] = new SqlParameter("@FechaInicial", SqlDbType.VarChar);
                sqlParameters[10] = new SqlParameter("@Duracion", SqlDbType.Float); 
                
                sqlParameters[11] = new SqlParameter("@Usuario", SqlDbType.VarChar);


                sqlParameters[0].Value = en.IdArchivo;
                
                sqlParameters[1].Value = en.RPU;
                sqlParameters[2].Value = en.RMU;

                sqlParameters[3].Value = en.DescuentoEnergia_B;
                sqlParameters[4].Value = en.DescuentoEnergia_I;
                sqlParameters[5].Value = en.DescuentoEnergia_P;
                sqlParameters[6].Value = en.DescuentoDemanda;

                sqlParameters[7].Value = en.CFPm;
                sqlParameters[8].Value = en.CVPm;

                sqlParameters[9].Value = en.FechaInicio;
                sqlParameters[10].Value = en.Duracion;
                                
                sqlParameters[11].Value = "";

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return true;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al inserta registro bases contrato", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }

    }


}
