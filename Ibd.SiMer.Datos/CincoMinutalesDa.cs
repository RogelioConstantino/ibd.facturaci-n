using Ibd.Framework.AccesoDatos;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using System;
using System.Collections.Generic;
using System.Data;

namespace Ibd.SiMer.Datos
{
    public class CincoMinutalesDa
    {
        private readonly IBaseDatos _baseDatos;

        //Inyeccion de dependencia
        public CincoMinutalesDa(IBaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public void Eliminar(ArchivoCincominutalesEn archivo)
        {
            const string sql = @"DELETE cm
                                FROM CincoMinutales cm
                                INNER JOIN ArchivosCincominutales ac
                                  ON cm.IdArchivoCincominutales= ac.IdArchivoCincominutales 
                                WHERE ac.IdPuntoCarga = @IdPuntoCarga and FechaArchivo = @FechaArchivo";
            try
            {
                _baseDatos.Conectar();
                _baseDatos.CrearComando(sql, CommandType.Text);
                _baseDatos.AsignarParametroWhitValue("@IdPuntoCarga", archivo.IdPuntoCarga);
                _baseDatos.AsignarParametroWhitValue("@FechaArchivo", archivo.FechaArchivo);

                _baseDatos.EjecutarComando();

            }
            catch (BaseDatosException ex)
            {
                throw new Exception("Error al obtener el Punto de Carga " + archivo.RMU + " de la BD.", ex);
            }
            finally
            {
                _baseDatos.Desconectar();
            }
        }
    }
}