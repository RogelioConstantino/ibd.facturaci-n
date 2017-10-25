
using System;
using System.Data;
using System.Collections.Generic;
using Ibd.SiMer.Entidades;
using Ibd.Framework.Extensores;
using Ibd.Framework.AccesoDatos;


namespace Ibd.SiMer.Datos
{
    public class logCargaMinutalDa
    {
        
        private readonly IBaseDatos _baseDatos;

        //Inyeccion de dependencia
        public logCargaMinutalDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public List<logCargaMinutalEn> Consultar()
        {
            const string sql = " select  [FechaArchivo] [Fecha de Carga],  RMU, [Archivo] [Nombre del Archivo], [NoRegistros] [Número de registros], Mensaje [Observaciones]FROM log_XMLUploadFile l";
            var list = new List<logCargaMinutalEn>();
            try
            {
                _baseDatos.Conectar();
                _baseDatos.CrearComando(sql, CommandType.Text);

                var datos = _baseDatos.TraerDataReader();
                while (datos.Read())
                {
                    list.Add(Leer(datos));
                }
                datos.Close();
                return list;
            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener el log de cargas de minutales", ex);
            }
            finally
            {
                _baseDatos.Desconectar();
            }
        }


        public EntidadFiltrable<logCargaMinutalEn> Consultar(EntidadFiltrable<logCargaMinutalEn> filtros)
        {
            const string sql = "usp_consultaBitacoraCargas";
            var list = new List<logCargaMinutalEn>();
            try
            {
                _baseDatos.Conectar();
                _baseDatos.CrearComando(sql, CommandType.StoredProcedure);

                //_baseDatos.AsignarParametroWhitValue("@Fecha", filtros.fecha.ToDBNull());
                _baseDatos.AsignarParametroWhitValue("@PuntoCarga", filtros.rmu.ToDBNull());

                _baseDatos.AsignarParametroWhitValue("@FechaInicio", filtros.fechaInicio.ToDBNull());
                _baseDatos.AsignarParametroWhitValue("@FechaFin", filtros.fechaFin.ToDBNull());

                _baseDatos.AsignarParametroWhitValue("@Pagina", filtros.Pagina);
                _baseDatos.AsignarParametroWhitValue("@NoRegistros", filtros.RegistrosPorPagina);
                _baseDatos.AsignarParametroWhitValue("@OrdenarPor", filtros.OrdenarPor.ToDBNull());
                _baseDatos.AsignarParametroWhitValue("@Direccion", filtros.Direccion.ToDBNull());

                var datos = _baseDatos.TraerDataReader();
                while (datos.Read())
                {
                    list.Add(Leer(datos));
                }

                filtros.Datos = list;

                datos.NextResult();
                while (datos.Read())
                {
                    filtros.TotalRegistros = datos.ToAppFormat<int>("NoRegistros");
                }

                datos.NextResult();
                while (datos.Read())
                {
                    filtros.RegistrosFiltrados = datos.ToAppFormat<int>("NoFiltrados");
                }

                datos.Close();
                return filtros;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los archivos cargados!", ex);
            }
            finally
            {
                _baseDatos.Desconectar();
            }
        }

        private logCargaMinutalEn Leer(IDataReader reader)
        {
            var Log = new logCargaMinutalEn
            {    
                //Id  = reader.ToAppFormat<int>("Id"),
                fechaCarga = reader.ToAppFormat<string>("FechaArchivo"),
                rmu = reader.ToAppFormat<string>("RMU"),
                archivo = reader.ToAppFormat<string>("Archivo"),
                numRegisttros= reader.ToAppFormat<int>("NoRegistros"),
                observaciones= reader.ToAppFormat<string>("Mensaje"),                
            };
            return Log;
        }


    }
}
