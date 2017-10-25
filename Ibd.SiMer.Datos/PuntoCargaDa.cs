using Ibd.Framework.AccesoDatos;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;

namespace Ibd.SiMer.Datos
{
    public class PuntoCargaDa
    {
        private readonly IBaseDatos _baseDatos;

        //Inyeccion de dependencia
        public PuntoCargaDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public PuntoCargaEn PuntoCargaConsultar(PuntoCargaEn puntoCarga)
        {
            const string sql = "usp_PuntoCargaConsultar";
            try
            {
                _baseDatos.Conectar();
                _baseDatos.CrearComando(sql, CommandType.StoredProcedure);
                _baseDatos.AsignarParametroWhitValue("@RMU", puntoCarga.RMU);

                var datos = _baseDatos.TraerDataReader();
                while (datos.Read())
                {
                    puntoCarga = LeerPc(datos);
                }
                datos.Close();
                return puntoCarga;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener el Punto de Carga " + puntoCarga.RMU + " de la BD.", ex);
            }
            finally
            {
                _baseDatos.Desconectar();
            }
        }

        public PuntoCargaFac PuntoCargaConsultar(PuntoCargaFac puntoCarga)
        {
            const string sql = @"Select	IdPuntoCarga, PuntoCarga, RPU, Codigo from  PuntosCarga
                                where(IdPuntoCarga = @IdPuntoCarga)";
            try
            {
                _baseDatos.Conectar();
                _baseDatos.CrearComando(sql, CommandType.Text);
                _baseDatos.AsignarParametroWhitValue("@IdPuntoCarga", puntoCarga.IdPuntoCarga);

                var datos = _baseDatos.TraerDataReader();
                while (datos.Read())
                {
                    puntoCarga = LeerPcAnexo(datos);
                }
                datos.Close();
                return puntoCarga;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener el Punto de Carga " + puntoCarga.IdPuntoCarga + " de la BD.", ex);
            }
            finally
            {
                _baseDatos.Desconectar();
            }
        }

        public List<PuntoCargaFac> Consultar()
        {
            const string sql = "SELECT pc.IdPuntoCarga,pc.Codigo,pc.PorteoMaximo,pc.DemandaContratada,t.Tarifa ,r.Region FROM PuntosCarga pc inner join regiones r on r.IdRegion = pc.IdRegion inner join Tarifas t on t.IdTarifa = pc.IdTarifa";            
            var list = new List<PuntoCargaFac>();
            ConnectionDB con = new ConnectionDB();
            try
            {
              
                SqlConnection conn = con.dbConnection();
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                //var datos1 = con.executeSelectQuery(sql, null);

                //_baseDatos.Conectar();
                //_baseDatos.CrearComando(sql, CommandType.Text);

                //var datos = _baseDatos.TraerDataReader();
                while (reader.Read())
                {
                    list.Add(LeerPcFac(reader));
                }
                reader.Close();
                
                return list;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener los Puntos de Carga", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();
                
            }
        }

        public List<PuntoCargaFac> ConsultarPrevio(string Año, string Mes, string Tipo)
        {
            //const string sql = "SELECT r.Region, gpo.Grupo, cli.Cliente,pc.Codigo , pc.IdPuntoCarga FROM PuntosCarga pc  inner join regiones r on r.IdRegion = pc.IdRegion  join clientes cli on cli.idCliente = pc.IdCliente join grupos gpo on gpo.IdGrupo = cli.IdGrupo";

            const string sql = "       SELECT r.Region, gpo.Grupo, cli.Cliente, pc.IdPuntoCarga, PuntoCarga, Codigo , a.intento, isnull(a.Archivo,'')Archivo, isnull(a.ArchivoPDF,'') ArchivoPDF, a.UsuarioCreacion Usuario  " +
                                "        FROM PuntosCarga pc " +
                                "       inner join regiones r on r.IdRegion = pc.IdRegion   " +
                                "        join clientes cli on cli.idCliente = pc.IdCliente  " +
                                "        join grupos gpo on gpo.IdGrupo = cli.IdGrupo  " +
                                "        left  join  (   " +
                                "                       SELECT fa.idPuntoCarga, fa.intento, fa.Archivo, fa.ArchivoPDF, fa.UsuarioCreacion   " +
                                "                       FROM FacturacionAnexos fa   " +
                                "                       join  ( " +
                                "                               SELECT idPuntoCarga, max(intento) intento   " +
                                "                               FROM FacturacionAnexos   " +
                                "                               WHERE Año = @intAnio   " +
                                "                                 and Mes = @intMes  " +
                                "                               GROUP BY idPuntoCarga   " +
                                "                             ) ua on fa.idPuntoCarga = ua.idPuntoCarga   " +
                                "                                 and fa.intento = ua.intento  " +
                                "                                 and fa.mes = @intMes   " +
                                "                                 and fa.Año = @intAnio  " +
                                "       ) a on pc.IdPuntoCarga = a.idPuntoCarga " +
                                "  WHERE  pc.idPuntoCarga not in (select IdPuntoCargaDuplicado from simer..PuntosCargaDuplicados)  ";

            //const string sql = "                   SELECT " +
            //                    "        " +
            //                    "                   gpo.vchDesGrupo Grupo, pc.vchDesCliente Cliente, pc.IdPuntoCarga, vchDesCliente PuntoCarga, vchCodCliente Codigo , a.intento, isnull(a.Archivo, '')Archivo, isnull(a.ArchivoPDF, '') ArchivoPDF, a.UsuarioCreacion Usuario         " +
            //                    "               FROM[IBD.Core]..[v_SIGEN_PuntosCarga] pc       " +
            //                    "               join[IBD.Core]..v_SIGEN_CatGrupos gpo on gpo.iCodGrupo = pc.iCodGrupo and pc.iCodGrupo = 8  " +
            //                    "               left join( " +
            //                    "                              SELECT fa.idPuntoCarga, fa.intento, fa.Archivo, fa.ArchivoPDF, fa.UsuarioCreacion " +
            //                    "                              FROM FacturacionAnexos fa " +
            //                    "                              join  ( " +
            //                    "                                      SELECT idPuntoCarga, max(intento) intento " +
            //                    "                                        FROM[IBD.Facturacion]..FacturacionAnexos " +
            //                    "                                       WHERE Año = @intAnio " +
            //                    "                                         and Mes = @intMes " +
            //                    "                                       GROUP BY idPuntoCarga " +
            //                    "                                    ) ua on fa.idPuntoCarga = ua.idPuntoCarga " +
            //                    "                                        and fa.intento = ua.intento " +
            //                    "                                        and fa.mes = @intMes " +
            //                    "                                        and fa.Año = @intAnio " +
            //                    "              ) a on pc.IdPuntoCarga = a.idPuntoCarga ";

            const string sqlAltamira = " SELECT r.DesRegionCFE Region, gpo.Grupo, cli.Cliente " +
                                "             , pcf.IdPuntoCarga, pcC.PuntoCarga, pcF.Codigo " +
                                "		      , a.intento, ISNULL(a.Archivo, '') AS Archivo, isnull(a.ArchivoPDF,'') ArchivoPDF, a.UsuarioCreacion AS Usuario " +
                                "          FROM [IBD.Core]..PuntosCarga AS pcC " +
                                "         INNER JOIN[IBD.Facturacion]..PuntosCarga   AS pcF ON pcC.IdPuntoCarga = pcF.IdPuntoCarga  and pcc.IdPuntoCarga in (19,20,21,22,108,109,110) " +
                                "         INNER JOIN[IBD.Core]..RegionesCFE AS r ON r.IdRegionCFE = pcC.IdRegion " +
                                "         INNER JOIN[IBD.Core]..Clientes AS cli ON cli.idCliente = pcC.IdCliente " +
                                "         INNER JOIN[IBD.Core]..Grupos AS gpo ON gpo.IdGrupo = cli.IdGrupo " +
                                "          LEFT OUTER JOIN " +
                                "                           (SELECT fa.idPuntoCarga, fa.intento, fa.Archivo, fa.ArchivoPDF, fa.UsuarioCreacion " +
                                "                              FROM [IBD.Facturacion]..FacturacionAnexos AS fa " +
                                "                             INNER JOIN ( SELECT idPuntoCarga, MAX(intento) AS intento " +
                                "                                            FROM [IBD.Facturacion]..FacturacionAnexos " +
                                "                                           WHERE (Año = @intAnio) AND (Mes = @intMes) " +
                                "                             GROUP BY idPuntoCarga) AS ua ON fa.idPuntoCarga = ua.idPuntoCarga AND fa.intento = ua.intento   and fa.Mes = @intMes  and fa.Año = @intAnio " +
                                "            ) AS a ON pcC.IdPuntoCarga = a.idPuntoCarga ";

          

            var list = new List<PuntoCargaFac>();
            ConnectionDB con = new ConnectionDB();
            try
            {

                SqlConnection conn = con.dbConnection();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (Tipo == "1")
                {
                    cmd = new SqlCommand(sql, conn);
                }          else
                {
                    cmd = new SqlCommand(sqlAltamira, conn);
                }
                                
                cmd.Parameters.AddWithValue("@intAnio", Año);
                cmd.Parameters.AddWithValue("intMes", Mes);

                conn.Open();
                                
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(LeerPcFacPrevio(reader));
                }
                reader.Close();

                return list;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener los Puntos de Carga", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }


        public List<PuntoCargaFac> ConsultarPrevioCFECalif(string Año, string Mes, string Tipo)
        {
            const string sqlCFECalificados = " " +
                                            "SELECT '' Region, ''Grupo, pc.vchCodCliente cliente,  pc.iCodCliente IdPuntoCarga, pc.vchCodCliente PuntoCarga ,  vchAbrCliente Codigo , isnull(a.intento, 0)intento, isnull(a.ArchivoAnexo, '')Archivo, isnull(a.ArchivoPDF, '') ArchivoPDF, a.UsuarioCreacion Usuario " +
                                            " FROM(select  iCodCliente, vchAbrCliente, vchCodCliente from[10.32.32.66].[SIGEN].[dbo].maeclientes pc    where icodcliente = 782) pc " +
                                            "      left  join( " +
                                            "                     SELECT  fa.intento, fa.ArchivoAnexo, fa.ArchivoPDF, fa.UsuarioCreacion " +
                                            "                     FROM [IBD.Facturacion]..[FacturacionAnexosCFECalificados] fa " +
                                            "                     join( " +
                                            "                             SELECT  max(intento) intento " +
                                            "                             FROM [IBD.Facturacion]..[FacturacionAnexosCFECalificados] " +
                                            "                             WHERE Año = @intAnio " +
                                            "                               and Mes = @intMes " +
                                            "                           ) ua on fa.intento = ua.intento " +
                                            "                               and fa.mes = @intMes " +
                                            "                               and fa.Año = @intAnio " +
                                            "                        ) a on 1 = 1 ";

            
            var list = new List<PuntoCargaFac>();
            ConnectionDB con = new ConnectionDB();
            try
            {

                SqlConnection conn = con.dbConnection();
                SqlCommand cmd = new SqlCommand(sqlCFECalificados, conn);
                
                cmd = new SqlCommand(sqlCFECalificados, conn);
                
                cmd.Parameters.AddWithValue("@intAnio", Año);
                cmd.Parameters.AddWithValue("intMes", Mes);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(LeerPcFacPrevio(reader));
                }
                reader.Close();

                return list;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener los Puntos de Carga CFe Calificados.", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }


        public List<PuntoCargaFac> ConsultarPrevioCFECalificados(string Año, string Mes, string Tipo)
        {
            
            const string sql = "SELECT pc. iCodCliente  IdPuntoCarga,  pc.vchCodCliente Codigo,  isnull(a.intento,0)intento, isnull(a.ArchivoAnexo,'')Archivo, isnull(a.ArchivoPDF,'') ArchivoPDF, a.UsuarioCreacion Usuario " +
                                " FROM (select iCodCliente,  vchCodCliente from[10.32.32.66].[SIGEN].[dbo].maeclientes pc    where icodcliente = 782 ) pc " +
                                "        left  join( " +
                                "                       SELECT  fa.intento, fa.ArchivoAnexo, fa.ArchivoPDF, fa.UsuarioCreacion " +
                                "                 " +
                                "                       FROM[FacturacionAnexosCFECalificados] fa " +
                                "  " +
                                "                       join(" +
                                "                               SELECT  max(intento) intento " +
                                "  " +
                                "                               FROM[FacturacionAnexosCFECalificados] " +
                                "  " +
                                "                               WHERE Año = @intAnio " +
                                "  " +
                                "                                 and Mes = @intMes " +
                                "                             ) ua on fa.intento = ua.intento " +
                                "  " +
                                "                                 and fa.mes = @intMes " +
                                "  " +
                                "                                 and fa.Año = @intAnio " +
                                "       ) a on 1 = 1";

           
            var list = new List<PuntoCargaFac>();
            ConnectionDB con = new ConnectionDB();
            try
            {

                SqlConnection conn = con.dbConnection();
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                    cmd = new SqlCommand(sql, conn);
                

                cmd.Parameters.AddWithValue("@intAnio", Año);
                cmd.Parameters.AddWithValue("intMes", Mes);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(LeerPcFacPrevio(reader));
                }
                reader.Close();

                return list;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener los Puntos de Carga", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }


        public List<PuntoCargaFac> ConsultarById(string Id)
        {
            const string sql = "SELECT p.IdPuntoCarga,IdRegion,c.IdGrupo, p.IdCliente, p.PuntoCarga , codigo, p.RPU, p.RMU, p.DemandaContratada, p.PorteoMaximo, DescuentoEnergia_B, DescuentoEnergia_I, DescuentoEnergia_P, DescuentoDemanda, p.NoCuenta, p.IdTarifa " +
                            "FROM PuntosCarga p " +
                            " join clientes c on c.IdCliente = p.IdCliente " +
                            " WHERE(p.IdPuntoCarga = @Id )"; 

            var list = new List<PuntoCargaFac>();
            ConnectionDB con = new ConnectionDB();
            try
            {

                SqlConnection conn = con.dbConnection();
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd.Parameters.AddWithValue("@Id", Id);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    list.Add(LeerPcFacEdit(reader));
                }
                reader.Close();

                return list;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener los Puntos de Carga", ex);
            }
            finally
            {
                //_baseDatos.Desconectar();

            }
        }
        
        public PuntoCargaEn LeerPc(IDataReader reader)
        {
            var oPuntoCarga = new PuntoCargaEn
            {
                IdPuntoCarga = reader.ToAppFormat<int>("IdPuntoCarga"),
                PuntoCarga = reader.ToAppFormat<string>("PuntoCarga"),
                RPU = reader.ToAppFormat<string>("RPU"),
                Codigo = reader.ToAppFormat<string>("Codigo"),
            };
            return oPuntoCarga;
        }

        public PuntoCargaFac LeerPcFac(IDataReader reader)
        {
            var oPuntoCarga = new PuntoCargaFac
            {
                IdPuntoCarga = reader.ToAppFormat<int>("IdPuntoCarga"),
                Codigo = reader.ToAppFormat<string>("Codigo"),
                PorteoMaximo = reader.ToAppFormat<double>("PorteoMaximo"),
                DemandaContratada = reader.ToAppFormat<double>("DemandaContratada"),
                Tarifa = reader.ToAppFormat<string>("Tarifa"),
                Region = reader.ToAppFormat<string>("Region")
            };
            return oPuntoCarga;
        }
        
        public PuntoCargaFac LeerPcFacPrevio(IDataReader reader)
        {
            var oPuntoCarga = new PuntoCargaFac
            {
                IdPuntoCarga = reader.ToAppFormat<int>("IdPuntoCarga"),
                //Region = reader.ToAppFormat<string>("Region"),
                Grupo = reader.ToAppFormat<string>("Grupo"),
                Cliente = reader.ToAppFormat<string>("Cliente"),                
                Codigo = reader.ToAppFormat<string>("Codigo"),
                Ruta  = reader.ToAppFormat<string>("Archivo"),
                RutaPDF = reader.ToAppFormat<string>("ArchivoPDF")
            };
            return oPuntoCarga;
        }
        public PuntoCargaFac LeerPcFacEdit(IDataReader reader)
        {
            try
            {
                var oPuntoCarga = new PuntoCargaFac
                {
                    IdPuntoCarga = reader.ToAppFormat<int>("IdPuntoCarga"),

                    IdRegion = reader.ToAppFormat<Int16>("IdRegion"),
                    IdGrupo = reader.ToAppFormat<int>("IdGrupo"),
                    IdCliente = reader.ToAppFormat<int>("IdCliente"),

                    Codigo = reader.ToAppFormat<string>("codigo"),
                    PuntoCarga = reader.ToAppFormat<string>("PuntoCarga"),

                    RPU = reader.ToAppFormat<string>("RPU"),
                    RMU = reader.ToAppFormat<string>("RMU"),

                    PorteoMaximo = reader.ToAppFormat<double>("PorteoMaximo"),
                    DemandaContratada = reader.ToAppFormat<double>("DemandaContratada"),                    
                    DescEnergiaBase = reader.ToAppFormat<double>("DescuentoEnergia_B"),
                    DescEnergiaIntermedia = reader.ToAppFormat<double>("DescuentoEnergia_I"),
                    DescEnergiaPunta = reader.ToAppFormat<double>("DescuentoEnergia_P"),
                    DescDemanda = reader.ToAppFormat<double>("DescuentoDemanda"),

                    NoCuenta = reader.ToAppFormat<string>("NoCuenta"),
                    IdTarifa = reader.ToAppFormat<Int16>("IdTarifa")
                };
                return oPuntoCarga;
            }
            catch (BaseDatosException ex)
            {
                return null;
            }            
        }

        public PuntoCargaFac LeerPcAnexo(IDataReader reader)
        {
            var oPuntoCarga = new PuntoCargaFac
            {
                IdPuntoCarga = reader.ToAppFormat<int>("IdPuntoCarga"),
                Codigo = reader.ToAppFormat<string>("Codigo"),
                PuntoCarga = reader.ToAppFormat<string>("PuntoCarga"),
                RPU = reader.ToAppFormat<string>("RPU"),
            };
            return oPuntoCarga;
        }
    }
}