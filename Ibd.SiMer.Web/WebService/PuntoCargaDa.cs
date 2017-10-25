
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

        public List<PuntoCargaFac> ConsultarPrevio()
        {
            const string sql = "SELECT r.Region, gpo.Grupo, cli.Cliente,pc.Codigo , pc.IdPuntoCarga FROM PuntosCarga pc  inner join regiones r on r.IdRegion = pc.IdRegion  join clientes cli on cli.idCliente = pc.IdCliente join grupos gpo on gpo.IdGrupo = cli.IdGrupo";
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
                Region = reader.ToAppFormat<string>("Region"),
                Grupo = reader.ToAppFormat<string>("Grupo"),
                Cliente = reader.ToAppFormat<string>("Cliente"),                
                Codigo = reader.ToAppFormat<string>("Codigo")                
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