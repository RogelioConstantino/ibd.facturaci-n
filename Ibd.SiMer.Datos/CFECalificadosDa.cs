using Ibd.Framework.AccesoDatos;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{

    public class CFECalificadosDa
    {


        private readonly IBaseDatos _baseDatos;

        public CFECalificadosDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }


        public Boolean insertaRegistro(CFECalificadosEn en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_insertaRegistroCFECalificados]");
                SqlParameter[] sqlParameters = new SqlParameter[17];

                sqlParameters[0] = new SqlParameter("@IdArchivo", SqlDbType.BigInt);                
                sqlParameters[1] = new SqlParameter("@Dia", SqlDbType.Int);
                sqlParameters[2] = new SqlParameter("@Hora", SqlDbType.Int);
                sqlParameters[3] = new SqlParameter("@TC", SqlDbType.Float);
                sqlParameters[4] = new SqlParameter("@SML", SqlDbType.Float);
                sqlParameters[5] = new SqlParameter("@PrecioGas", SqlDbType.Float);
                sqlParameters[6] = new SqlParameter("@CTUNG", SqlDbType.Float);
                sqlParameters[7] = new SqlParameter("@Combustible", SqlDbType.Float);
                sqlParameters[8] = new SqlParameter("@CVOM", SqlDbType.Float);
                sqlParameters[9] = new SqlParameter("@Transmision", SqlDbType.Float);
                sqlParameters[10] = new SqlParameter("@CENACE", SqlDbType.Float);
                sqlParameters[11] = new SqlParameter("@PrecioEnergia", SqlDbType.Float);
                sqlParameters[12] = new SqlParameter("@PML_JOV_230", SqlDbType.Float);
                sqlParameters[13] = new SqlParameter("@TBFin", SqlDbType.Float);
                sqlParameters[14] = new SqlParameter("@CFECalificados", SqlDbType.Float);
                sqlParameters[15] = new SqlParameter("@PrecioCFECalificados", SqlDbType.Float);
                sqlParameters[16] = new SqlParameter("@Usuario", SqlDbType.VarChar);
                                
                sqlParameters[0].Value = en.IdArchivo;
                sqlParameters[1].Value = en.dia;
                sqlParameters[2].Value = en.hora;
                sqlParameters[3].Value = en.TC;
                sqlParameters[4].Value = en.SML;
                sqlParameters[5].Value = en.PrecioGas;
                sqlParameters[6].Value = en.CTUNG;
                sqlParameters[7].Value = en.Combustible;
                sqlParameters[8].Value = en.CVOM;
                sqlParameters[9].Value = en.Transmision;
                sqlParameters[10].Value = en.CENACE;
                sqlParameters[11].Value = en.PrecioEnergia;
                sqlParameters[12].Value = en.PML_JOV_230;
                sqlParameters[13].Value = en.TBFin;
                sqlParameters[14].Value = en.CFECalificados;
                sqlParameters[15].Value = en.PrecioCFECalificados;
                sqlParameters[16].Value = en.usuario;

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return true;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al inserta registro CFE Calificados ", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }

    }


}
