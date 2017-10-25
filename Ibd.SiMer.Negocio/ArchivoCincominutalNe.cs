using Ibd.Framework;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using Ibd.SiMer.Negocio.Managers;
using Ibd.SiMer.Datos;
using System;

using System.Data;

namespace Ibd.SiMer.Negocio
{
    public class ArchivoCincominutalNe
    {
        // Proceso que se encarga de cargar el archivo a la BD
        public void CargarArchivo(ArchivoCincominutalesEn archivo)
        {
            try
            {
                var sCarpetas = archivo.Archivo.Split(@"\".ToCharArray()); //Se toma el ultima "\"
                var sGuionesArchivo = sCarpetas[sCarpetas.Length - 1].Split("_".ToCharArray());

                // ------ Ejemplo de archivo
                // ------ \\10.32.36.115\archivosmercado\archivosC005_22000170208TSO991022017ICL_2017-03-01_2017-03-01.xml

                // Si el archivo no trae los formatos correctos se manda error
                if ((sGuionesArchivo.Length != 4))
                {
                    archivo.IdCargaArchivoEstatus = (int)CargaArchivoEstatus.Fallido;
                    archivo.Mensaje = "Nomenclatura invalida";
                }
                else
                {
                    // Se valida el Punto de carga por RMU
                    var oPuntoCargaDa = new PuntoCargaDa(Singleton<ConexionMng>.Single.Default());
                    var pc = new PuntoCargaEn();
                    pc.RMU = sGuionesArchivo[1];
                    pc = oPuntoCargaDa.PuntoCargaConsultar(pc);

                    if (pc.IdPuntoCarga == 0)
                    {
                        archivo.IdCargaArchivoEstatus = (int)CargaArchivoEstatus.Fallido;
                        archivo.Mensaje = "Punto de carga no registrado";
                        archivo.RMU = sGuionesArchivo[1];
                    }
                    else {
                        archivo.RMU = sGuionesArchivo[1];
                        archivo.FechaArchivo = sGuionesArchivo[2].ToDateTime("yyyy-MM-dd");
                        archivo.IdCargaArchivoEstatus = (int)CargaArchivoEstatus.EnCurso;
                        archivo.IdPuntoCarga = pc.IdPuntoCarga;
                    }
                }

                // Primero se agrega la bitacora del archivo
                var oArchivoCincominutalDa = new ArchivoCincominutalDa(Singleton<ConexionMng>.Single.Default());
                var oCincoMinutalesDa = new CincoMinutalesDa(Singleton<ConexionMng>.Single.Default());
                archivo = oArchivoCincominutalDa.Agregar(archivo);

                // Se procesan los datos del archivo, si este es valido
                if (archivo.IdCargaArchivoEstatus == (int)CargaArchivoEstatus.EnCurso)
                {
                    try
                    {
                        // Solo pueden existir los datos de un archivo por lo que se eliminan al inicio
                        oCincoMinutalesDa.Eliminar(archivo);
                        archivo  = oArchivoCincominutalDa.Cargar(archivo);

                        // Todos los registros correctos
                        if (archivo.NoRegistros == 288)
                        {
                            archivo.IdCargaArchivoEstatus = (int)CargaArchivoEstatus.Exitoso;
                            archivo.Mensaje = "";
                        }
                        else if (archivo.NoRegistros == 0)
                        {
                            archivo.IdCargaArchivoEstatus = (int)CargaArchivoEstatus.Fallido;
                            archivo.Mensaje = "Archivo vacío";
                        }
                        else if (archivo.NoRegistros > 288)
                        {
                            archivo.IdCargaArchivoEstatus = (int)CargaArchivoEstatus.Exitoso;
                            archivo.Mensaje = "Archivo con más de 288 cincominutales";
                        }
                        else
                        {
                            archivo.IdCargaArchivoEstatus = (int)CargaArchivoEstatus.Incompleto;
                            // Lista de cincominutales faltantes
                            archivo.Mensaje = "Archivo con menos de 288 cincominutales, Listar faltantes...";
                        }

                    }
                    catch (Exception ex)
                    {
                        archivo.IdCargaArchivoEstatus = (int)CargaArchivoEstatus.Fallido;
                        archivo.Mensaje = "Error al cargar los datos";
                        archivo.NoRegistros = 0;
//                        ErrorManager.ManejarError(ex);
                    }

                    // Por ultimo se actualiza el estatus del archivo
                    oArchivoCincominutalDa.Actualizar(archivo);
                }

                //oArchivoCincominutalDa.Cargar(archivo, Config.ObtenerValuePorkeyEnSeccion<string>("ConfiguracionServicio", "CarpetaLocal"));
            }
            catch (Exception ex)
            {
                //ErrorManager.ManejarError(ex);
                throw new Exception("No se pudo cargar el archivo.");
            }
        }

        public int insertarHeaderArchivo(ArchivoCincominutalesEn archivo)
        {
            var oDa = new ArchivoCincominutalDa(Singleton<ConexionMng>.Single.Default());
            
            var  key = oDa.insertarHeaderArchivo(archivo);

            return key;
        }
        public Boolean actualizaHeaderArchivo(ArchivoCincominutalesEn archivo)
        {
            var oDa = new ArchivoCincominutalDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.actualizaHeaderArchivo(archivo);
            return true;
        }

        public Boolean actualizaCincominutal(CincoMinutalEn cincoMinutal)
        {
            var oDa = new ArchivoCincominutalDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.actualizaCincominutales(cincoMinutal);
            return true;
        }


    }
}