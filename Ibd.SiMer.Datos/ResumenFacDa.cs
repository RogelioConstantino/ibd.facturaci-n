using Ibd.Framework.AccesoDatos;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class ResumenFacDa
    {


        private readonly IBaseDatos _baseDatos;

        public ResumenFacDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }


        public Boolean insertaRegistro(ResumenFacEn en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_insertaRegistroResumen]");
                SqlParameter[] sqlParameters = new SqlParameter[24];

                sqlParameters[0] = new SqlParameter("@IdArchivo", SqlDbType.BigInt);                
                sqlParameters[1] = new SqlParameter("@IdTipoRenglonResumen", SqlDbType.BigInt);
                sqlParameters[2] = new SqlParameter("@IdTipoEncabezadoResumen", SqlDbType.BigInt);
                sqlParameters[3] = new SqlParameter("@IdPuntoCarga", SqlDbType.BigInt);

                sqlParameters[4] = new SqlParameter("@KWH_Base", SqlDbType.Float);
                sqlParameters[5] = new SqlParameter("@KWH_Intermedia", SqlDbType.Float);
                sqlParameters[6] = new SqlParameter("@KWH_Punta", SqlDbType.Float);
                sqlParameters[7] = new SqlParameter("@KWH_SemiPunta", SqlDbType.Float);
                sqlParameters[8] = new SqlParameter("@KWH_TOTALES", SqlDbType.Float);
                sqlParameters[9] = new SqlParameter("@KW_Base", SqlDbType.Float);
                sqlParameters[10] = new SqlParameter("@KW_Intermedia", SqlDbType.Float);
                sqlParameters[11] = new SqlParameter("@KW_Punta", SqlDbType.Float);
                sqlParameters[12] = new SqlParameter("@KW_SemiPunta", SqlDbType.Float);
                sqlParameters[13] = new SqlParameter("@KVARH", SqlDbType.Float);
                sqlParameters[14] = new SqlParameter("@FP", SqlDbType.Float);
                sqlParameters[15] = new SqlParameter("@hrs_Base", SqlDbType.Float);
                sqlParameters[16] = new SqlParameter("@hrs_Intermedia", SqlDbType.Float);
                sqlParameters[17] = new SqlParameter("@hrs_Punta", SqlDbType.Float);
                sqlParameters[18] = new SqlParameter("@hrs_SemiPunta", SqlDbType.Float);
                sqlParameters[19] = new SqlParameter("@FactorCarga_Base", SqlDbType.Float);
                sqlParameters[20] = new SqlParameter("@FactorCarga_Intermedia", SqlDbType.Float);
                sqlParameters[21] = new SqlParameter("@FactorCarga_Punta", SqlDbType.Float);
                sqlParameters[22] = new SqlParameter("@FactorCarga_SemiPunta", SqlDbType.Float);
                sqlParameters[23] = new SqlParameter("@Usuario", SqlDbType.VarChar);


                sqlParameters[0].Value = en.IdArchivo;
                sqlParameters[1].Value = en.IdTipoRenglonResumen;                
                sqlParameters[2].Value = en.IdTipoEncabezadoResumen;
                sqlParameters[3].Value = en.IdPuntoCarga;

                sqlParameters[4].Value = en.KWH_Base;
                sqlParameters[5].Value = en.KWH_Intermedia;
                sqlParameters[6].Value = en.KWH_Punta;
                sqlParameters[7].Value = en.KWH_SemiPunta;
                sqlParameters[8].Value = en.KWH_TOTALES;
                sqlParameters[9].Value = en.KW_Base;
                sqlParameters[10].Value = en.KW_Intermedia;
                sqlParameters[11].Value = en.KW_Punta;
                sqlParameters[12].Value = en.KW_SemiPunta;
                sqlParameters[13].Value = en.KVARH;
                sqlParameters[14].Value = en.FP;
                sqlParameters[15].Value = en.hrs_Base;
                sqlParameters[16].Value = en.hrs_Intermedia;
                sqlParameters[17].Value = en.hrs_Punta;
                sqlParameters[18].Value = en.hrs_SemiPunta;
                sqlParameters[19].Value = en.FactorCarga_Base;
                sqlParameters[20].Value = en.FactorCarga_Intermedia;
                sqlParameters[21].Value = en.FactorCarga_Punta;
                sqlParameters[22].Value = en.FactorCarga_SemiPunta;
                sqlParameters[23].Value = "";

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return true;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al inserta registro resumen", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }


        public Boolean insertaRegistroCFECostosTrans(ResumenFacEn en)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_insertaRecordResumenCFECostosTrans]");
                SqlParameter[] sqlParameters = new SqlParameter[24];

                sqlParameters[0] = new SqlParameter("@IdArchivo", SqlDbType.BigInt);
                sqlParameters[1] = new SqlParameter("@IdTipoRenglonResumen", SqlDbType.BigInt);
                sqlParameters[2] = new SqlParameter("@IdTipoEncabezadoResumen", SqlDbType.BigInt);
                sqlParameters[3] = new SqlParameter("@IdPuntoCarga", SqlDbType.BigInt);

                sqlParameters[4] = new SqlParameter("@KWH_Base", SqlDbType.Float);
                sqlParameters[5] = new SqlParameter("@KWH_Intermedia", SqlDbType.Float);
                sqlParameters[6] = new SqlParameter("@KWH_Punta", SqlDbType.Float);
                sqlParameters[7] = new SqlParameter("@KWH_SemiPunta", SqlDbType.Float);
                sqlParameters[8] = new SqlParameter("@KWH_TOTALES", SqlDbType.Float);
                sqlParameters[9] = new SqlParameter("@KW_Base", SqlDbType.Float);
                sqlParameters[10] = new SqlParameter("@KW_Intermedia", SqlDbType.Float);
                sqlParameters[11] = new SqlParameter("@KW_Punta", SqlDbType.Float);
                sqlParameters[12] = new SqlParameter("@KW_SemiPunta", SqlDbType.Float);
                sqlParameters[13] = new SqlParameter("@KVARH", SqlDbType.Float);
                sqlParameters[14] = new SqlParameter("@FP", SqlDbType.Float);
                sqlParameters[15] = new SqlParameter("@hrs_Base", SqlDbType.Float);
                sqlParameters[16] = new SqlParameter("@hrs_Intermedia", SqlDbType.Float);
                sqlParameters[17] = new SqlParameter("@hrs_Punta", SqlDbType.Float);
                sqlParameters[18] = new SqlParameter("@hrs_SemiPunta", SqlDbType.Float);
                sqlParameters[19] = new SqlParameter("@FactorCarga_Base", SqlDbType.Float);
                sqlParameters[20] = new SqlParameter("@FactorCarga_Intermedia", SqlDbType.Float);
                sqlParameters[21] = new SqlParameter("@FactorCarga_Punta", SqlDbType.Float);
                sqlParameters[22] = new SqlParameter("@FactorCarga_SemiPunta", SqlDbType.Float);
                sqlParameters[23] = new SqlParameter("@Usuario", SqlDbType.VarChar);
                
                sqlParameters[0].Value = en.IdArchivo;
                sqlParameters[1].Value = en.IdTipoRenglonResumen;
                sqlParameters[2].Value = en.IdTipoEncabezadoResumen;
                sqlParameters[3].Value = en.IdPuntoCarga;

                sqlParameters[4].Value = en.KWH_Base;
                sqlParameters[5].Value = en.KWH_Intermedia;
                sqlParameters[6].Value = en.KWH_Punta;
                sqlParameters[7].Value = en.KWH_SemiPunta;
                sqlParameters[8].Value = en.KWH_TOTALES;
                sqlParameters[9].Value = en.KW_Base;
                sqlParameters[10].Value = en.KW_Intermedia;
                sqlParameters[11].Value = en.KW_Punta;
                sqlParameters[12].Value = en.KW_SemiPunta;
                sqlParameters[13].Value = en.KVARH;
                sqlParameters[14].Value = en.FP;
                sqlParameters[15].Value = en.hrs_Base;
                sqlParameters[16].Value = en.hrs_Intermedia;
                sqlParameters[17].Value = en.hrs_Punta;
                sqlParameters[18].Value = en.hrs_SemiPunta;
                sqlParameters[19].Value = en.FactorCarga_Base;
                sqlParameters[20].Value = en.FactorCarga_Intermedia;
                sqlParameters[21].Value = en.FactorCarga_Punta;
                sqlParameters[22].Value = en.FactorCarga_SemiPunta;
                sqlParameters[23].Value = "";

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return true;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al inserta registro resumen", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }


        public Boolean insertaPuntoCargaCostosTrans(long idArchivo, int Renglon, String PuntoCarga, int Año, int Mes, int Central, String usuario)                       
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Facturacion]..[usp_insertaPuntoCargaCFECostosTrans]");
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@IdArchivo", SqlDbType.BigInt);
                sqlParameters[1] = new SqlParameter("@Renglon", SqlDbType.BigInt);                
                sqlParameters[2] = new SqlParameter("@PuntoCarga", SqlDbType.VarChar);

                sqlParameters[3] = new SqlParameter("@Año", SqlDbType.BigInt);
                sqlParameters[4] = new SqlParameter("@Mes", SqlDbType.BigInt);
                sqlParameters[5] = new SqlParameter("@IdEmpresa", SqlDbType.BigInt);
                
                sqlParameters[6] = new SqlParameter("@Usuario", SqlDbType.VarChar);

                sqlParameters[0].Value = idArchivo;
                sqlParameters[1].Value = Renglon;
                sqlParameters[2].Value = PuntoCarga;
                sqlParameters[3].Value = Año;

                sqlParameters[4].Value = Mes;
                sqlParameters[5].Value = Central;
                sqlParameters[6].Value = usuario;

                con.dbConnection();
                DataTable dtData = con.executeStoreProcedure(query, sqlParameters);

                return true;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al inserta punto de carga  resumen cfe ", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
            }
        }


    }


}
